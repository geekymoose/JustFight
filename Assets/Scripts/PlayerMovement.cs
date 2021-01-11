using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    private Vector2 m_movementDirection;
    public float m_movementSpeedInUnitPerSec;

    void Start()
    {
        this.m_Rigidbody2D = this.GetComponent<Rigidbody2D>();
        Assert.IsNotNull(this.m_Rigidbody2D, "Missing component (rigidbody2D)");
    }

    void FixedUpdate()
    {

        this.m_Rigidbody2D.velocity = m_movementDirection * m_movementSpeedInUnitPerSec;
    }

    public void OnInputMove(InputAction.CallbackContext context)
    {
        m_movementDirection= context.ReadValue<Vector2>();
    }
}
