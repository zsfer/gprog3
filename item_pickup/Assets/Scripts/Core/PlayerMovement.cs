using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 7;
    [SerializeField] float _mouseSensitivity = 3;
    CharacterController _cc;

    Vector2 _mouseVec;

    void Start()
    {
        _cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        var moveVec = (transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical")).normalized * _moveSpeed;
        _cc.SimpleMove(moveVec);

        _mouseVec.x = Input.GetAxisRaw("Mouse X") * _mouseSensitivity;
        _mouseVec.y -= Input.GetAxisRaw("Mouse Y") * _mouseSensitivity;
        _mouseVec.y = Mathf.Clamp(_mouseVec.y, -90, 90);

        transform.Rotate(0, _mouseVec.x, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(_mouseVec.y, 0, 0);
    }
}
