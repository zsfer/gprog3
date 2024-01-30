using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

    [field: SerializeField] public Transform PlayerHand { get; private set; }
    [SerializeField] float _pickupRange = 5; // 5 meters
    [SerializeField] LayerMask _pickupMask;
    public bool CanPickup { get; private set; } = false;

    public Interactable CurrentInteraction { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        CanPickup = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, _pickupRange, _pickupMask);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CanPickup && hit.collider.CompareTag("Interactable"))
            {
                SetInteraction(hit.collider.GetComponent<Interactable>());
                return;
            }

            if (CurrentInteraction != null)
            {
                SetInteraction(CurrentInteraction);
            }
        }
    }

    public void SetInteraction(Interactable target = null)
    {
        CurrentInteraction = target;
        if (target != null)
            target.Interact(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * _pickupRange);
    }

}