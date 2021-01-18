using UnityEngine;
using UnityEngine.Assertions;

/// Shot that goes forward with the same pace
[RequireComponent(typeof(Rigidbody2D))]
public class ShotMovementMissile : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The missile speed (Unity force to apply)")]
    private float speedInUnityForce;

    private Rigidbody2D rg;

    private void Awake()
    {
        this.rg = this.GetComponent<Rigidbody2D>();
        Assert.IsNotNull(this.rg, "Missing asset (Rigidbody2D)");
    }

    private void FixedUpdate()
    {
        Vector2 forwardForce = new Vector2(this.transform.up.x, this.transform.up.y);
        forwardForce *= this.speedInUnityForce * Time.deltaTime;
        this.rg.AddForce(forwardForce);
        Debug.DrawRay(this.transform.position, this.rg.velocity, Color.green, 0.2f);
    }

    public void SetCurrentSpeed(float speed)
    {
        Vector2 forward = new Vector2(this.transform.up.x, this.transform.up.y);
        this.rg.velocity = forward * speed;
        this.speedInUnityForce = speed;
    }
}
