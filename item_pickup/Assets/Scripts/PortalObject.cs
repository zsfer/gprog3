using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PortalObject : MonoBehaviour
{
    GameObject _clone;

    Portal _inPortal, _outPortal;

    Rigidbody _rb;
    Collider _col;

    Quaternion flip = Quaternion.Euler(0, 180, 0);
    void Awake()
    {
        _clone = new GameObject();
        _clone.SetActive(false);
        var filter = _clone.AddComponent<MeshFilter>();
        var rend = _clone.AddComponent<MeshRenderer>();

        filter.mesh = GetComponent<MeshFilter>().mesh;
        rend.materials = GetComponent<MeshRenderer>().materials;
        _clone.transform.localScale = transform.localScale;

        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
    }

    void LateUpdate()
    {
        if (_inPortal == null || _outPortal == null) return;

        if (_clone.activeSelf && _inPortal.IsPlaced && _outPortal.IsPlaced)
        {
            var pos = _inPortal.transform.InverseTransformPoint(transform.position);
            pos = flip * pos;

            var rot = Quaternion.Inverse(_inPortal.transform.rotation) * transform.rotation;
            rot *= flip;

            _clone.transform.SetPositionAndRotation(_outPortal.transform.TransformPoint(pos), _outPortal.transform.rotation * rot);
        }
    }

    public void InPortal(Portal inPortal, Portal outPortal, Collider wall)
    {
        _inPortal = inPortal;
        _outPortal = outPortal;
        Physics.IgnoreCollision(_col, wall);

        _clone.SetActive(false);
    }

    public void ExitPortal(Collider wall)
    {
        Physics.IgnoreCollision(_col, wall, false);
        // _clone.SetActive(false);
    }

    public void Warp()
    {
        var pos = _inPortal.transform.InverseTransformPoint(transform.position);
        pos = flip * pos;

        var rot = Quaternion.Inverse(_inPortal.transform.rotation) * transform.rotation;
        rot *= flip;

        var vel = _inPortal.transform.InverseTransformDirection(_rb.velocity);
        vel = flip * vel;
        _rb.velocity = _outPortal.transform.TransformDirection(vel);

        (_outPortal, _inPortal) = (_inPortal, _outPortal);
    }
}
