using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Speed in Unity units per seconds")]
    private float maxMovementSpeedInUnitPerSec;

    private Rigidbody2D rg2D = null;
    private Vector2 movementDirection = Vector2.zero;
    private float effectiveSpeed = 0.0f;
    private bool isIdle = true;

    void Start()
    {
        this.rg2D= this.GetComponent<Rigidbody2D>();
        Assert.IsNotNull(this.rg2D, "Missing asset (rigidbody2D)");
    }

    void FixedUpdate()
    {
        this.rg2D.velocity = this.movementDirection * this.effectiveSpeed;
    }

    public void OnInputMove(InputAction.CallbackContext context)
    {
        Vector2 newDirection = context.ReadValue<Vector2>();
        if(newDirection == Vector2.zero)
        {
            this.isIdle = true;
        }
        else
        {
            this.isIdle = false;
            this.movementDirection = newDirection;
            this.effectiveSpeed = this.movementDirection.magnitude * this.maxMovementSpeedInUnitPerSec;
            float angle = Mathf.Atan2(this.movementDirection.y, this.movementDirection.x);
            this.rg2D.SetRotation((angle * Mathf.Rad2Deg) - 90); // In the scene, 0 is up, in atan, 0 at right
        }
    }

}
