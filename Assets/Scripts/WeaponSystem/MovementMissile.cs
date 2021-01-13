using UnityEngine;
using UnityEngine.Assertions;

public class MovementMissile : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The missile speed (Unity force to apply)")]
    private float speed;

    private Rigidbody2D rg;

    private void Awake()
    {
        this.rg = this.GetComponent<Rigidbody2D>();
        Assert.IsNotNull(this.rg, "Missing asset (Rigidbody2D)");
        Assert.IsTrue(this.speed > 0, "Invalid asset (speed should not be zero)");
    }

    private void FixedUpdate()
    {
        Vector2 forward = new Vector2(this.transform.up.x, this.transform.up.y);
        forward *= this.speed * Time.deltaTime;
        this.rg.AddForce(forward);
        Debug.DrawRay(this.transform.position, this.rg.velocity, Color.red, 0.2f);
    }
}
