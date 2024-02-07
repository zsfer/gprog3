using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    [SerializeField] GameObject _inPortalPrefab, _outPortalPrefab;
    [SerializeField] LayerMask _layerMask;

    GameObject _inPortal, _outPortal;
    void Update()
    {
        if (PlayerInteraction.Instance.CurrentInteraction != null) return;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 1000f, _layerMask))
        {
            if (Input.GetMouseButtonDown(0))
                CreatePortal(hit, _inPortalPrefab, ref _inPortal, PortalType.IN);
            if (Input.GetMouseButtonDown(1))
                CreatePortal(hit, _outPortalPrefab, ref _outPortal, PortalType.OUT);
        }
    }

    void CreatePortal(RaycastHit hit, GameObject portalPrefab, ref GameObject portal, PortalType type)
    {
        if (portal == null) portal = Instantiate(portalPrefab, hit.point, Quaternion.identity);
        portal.transform.SetPositionAndRotation(hit.point, Quaternion.FromToRotation(portal.transform.up, hit.normal) * portal.transform.rotation);
        portal.GetComponent<Portal>().SpawnPortal(hit.collider);

        var behaviour = portal.GetComponent<Portal>();
        behaviour.LinkedPortal = type == PortalType.IN ? _outPortal : _inPortal;
    }

}
