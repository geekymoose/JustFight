using UnityEngine;
using UnityEngine.Assertions;

public class ProjectileMovement : MonoBehaviour
{
    private Rigidbody2D rg;

    void Awake()
    {
        this.rg = this.GetComponent<Rigidbody2D>();
        Assert.IsNotNull(this.rg, "Missing asset (Rigidbody2D)");
    }

    public void ApplySpeed(float speedInUnityUnitsPerSec)
    {
        Vector2 forward = new Vector2(this.transform.up.x, this.transform.up.y);
        this.rg.velocity = forward * speedInUnityUnitsPerSec;
    }
}