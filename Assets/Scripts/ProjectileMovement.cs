using UnityEngine;
using UnityEngine.Assertions;

public class ProjectileMovement : MonoBehaviour
{
    public float speedInUnityUnitPerSec;

    private Rigidbody2D rg;

    // Start is called before the first frame update
    void Start()
    {
        this.rg = this.GetComponent<Rigidbody2D>();
        Assert.IsNotNull(this.rg, "Missing asset (Rigidbody2D)");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 forward = new Vector2(this.transform.up.x, this.transform.up.y);
        //this.rg.AddForce(forward * speedInUnityUnitPerSec * Time.fixedDeltaTime);
        this.rg.velocity = forward * speedInUnityUnitPerSec;
    }
}