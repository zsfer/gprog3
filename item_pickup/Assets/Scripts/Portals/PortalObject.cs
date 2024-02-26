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

    int _portalEnterCount = 0;
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
        else
        {
            _clone.transform.position = Vector3.one * 1000;
        }
    }

    public void InPortal(Portal inPortal, Portal outPortal, Collider wall)
    {
        _inPortal = inPortal;
        _outPortal = outPortal;
        Physics.IgnoreCollision(_col, wall);

        _portalEnterCount++;
        _clone.SetActive(true);
    }

    public void ExitPortal(Collider wall)
    {
        Physics.IgnoreCollision(_col, wall, false);
        _portalEnterCount--;
        if (_portalEnterCount == 0)
            _clone.SetActive(false);
    }

    public void Warp(Vector3 positionDifference)
    {
        if (_inPortal == null || _outPortal == null) return;

        // var pos = _inPortal.transform.InverseTransformPoint(transform.position);
        // pos = flip * pos;
        // _rb.position = _outPortal.transform.TransformPoint(pos);
        var rotationDiff = -Quaternion.Angle(transform.rotation, _outPortal.transform.rotation);
        rotationDiff += 180;
        transform.Rotate(Vector3.up, rotationDiff);
        // _rb.(Vector3.up, rotationDiff);

        Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * positionDifference;
        _rb.position = _outPortal.transform.position + positionOffset;

        // var rot = Quaternion.Inverse(_inPortal.transform.rotation) * transform.rotation;
        // rot = flip * rot;
        // _rb.rotation = _outPortal.transform.rotation * rot;

        var vel = _inPortal.transform.InverseTransformDirection(_rb.velocity);
        vel = flip * vel;
        _rb.velocity = _outPortal.transform.TransformDirection(vel);

        (_outPortal, _inPortal) = (_inPortal, _outPortal);
    }
}
