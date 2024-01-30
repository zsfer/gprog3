using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DraggableItem : Interactable
{

    [SerializeField] float _dragForce = 10000;
    Transform _hand;
    bool _isDragging;

    Rigidbody _rb;

    private void Start()
    {
        _hand = PlayerInteraction.Instance.PlayerHand;
        _rb = GetComponent<Rigidbody>();
    }

    public override void Interact(bool isInteracted)
    {
        _isDragging = !_isDragging;
        _rb.drag = _isDragging ? 10 : 1;
        _rb.useGravity = !_isDragging;

        if (!_isDragging) PlayerInteraction.Instance.SetInteraction(null);
    }

    void Update()
    {
        if (!_isDragging) return;

        if (Vector3.Distance(transform.position, _hand.position) > 0.1f)
        {
            var dir = _hand.position - transform.position;
            _rb.AddForce(_dragForce * Time.deltaTime * dir);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Interact(false);
            _rb.AddForce(_dragForce * 50 * Time.deltaTime * Camera.main.transform.forward);
        }
    }


}