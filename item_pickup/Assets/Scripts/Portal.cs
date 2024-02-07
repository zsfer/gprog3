using System.Collections;
using System.Collections.Generic;
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
            var objPos = transform.InverseTransformPoint(obj.transform.position);

            if (objPos.z > 0.0f)
            {
                obj.Warp();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsPlaced) return;
        if (other.TryGetComponent<PortalObject>(out var obj))
        {
            _objectsInPortal.Add(obj);
            obj.InPortal(this, LinkedPortal.GetComponent<Portal>(), _wall);
        }
    }
}
