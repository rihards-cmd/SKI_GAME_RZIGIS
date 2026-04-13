using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private InputAction move;
    [SerializeField] private float rotSpeed = 30;
    private Rigidbody rb;
    [SerializeField] private float forwardForce = 50;

    private void Awake()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Vector2 moveInput = move.ReadValue<Vector2>();
        Debug.Log("x:"+ moveInput.x + "y:"+ moveInput.y);

        transform.Rotate(0, -moveInput.x * rotSpeed* Time.deltaTime, 0);

        float turnAngle = Math.Abs(180- transform.localEulerAngles.y);
        float speedMult = Mathf.Cos(turnAngle * Mathf.Deg2Rad);
        rb.AddForce(transform.forward * forwardForce * speedMult * Time.fixedDeltaTime);

    }
}
