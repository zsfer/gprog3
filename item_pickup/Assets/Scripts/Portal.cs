using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum PortalType
{
    IN, OUT
}
public class Portal : MonoBehaviour
{
    public PortalType Type;
    public GameObject LinkedPortal { get; set; }

    public bool IsPlaced { get; private set; }
    readonly List<PortalObject> _objectsInPortal = new();

    Collider _wall;

    public void SpawnPortal(Collider wall)
    {
        IsPlaced = true;
        _wall = wall;
    }

    void Update()
    {
        foreach (var obj in _objectsInPortal)
        {
            var posDiff = obj.transform.position - transform.position;
            var dot = Vector3.Dot(transform.up, posDiff);

            if (dot < 0)
                obj.Warp(posDiff);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsPlaced) return;
        if (other.TryGetComponent<PortalObject>(out var obj))
        {
            _objectsInPortal.Add(obj);

            if (LinkedPortal == null) return;
            obj.InPortal(this, LinkedPortal.GetComponent<Portal>(), _wall);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var obj = other.GetComponent<PortalObject>();

        if (_objectsInPortal.Contains(obj))
        {
            _objectsInPortal.Remove(obj);
            obj.ExitPortal(_wall);
        }
    }
}
