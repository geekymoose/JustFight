using UnityEngine;
using UnityEngine.Assertions;

/// Shot that has an initial speed, then decrease continuously.
[RequireComponent(typeof(Rigidbody2D))]
public class ShotMovementProjectile : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Each second, the speed is affected by this modificator (In Unity force)")]
    private float speedModificator = 1;

    private float startTime; // Time when the movement started
    private Rigidbody2D rg;

    private void Awake()
    {
        this.rg = this.GetComponent<Rigidbody2D>();
        Assert.IsNotNull(this.rg, "Missing asset (Rigidbody2D)");
    }

    private void Start()
    {
        this.startTime = Time.time;
    }

    private void FixedUpdate()
    {
        // This is a hacky why to recreate a pseudo gravity
        float elapsedtime = Time.time - this.startTime;
        elapsedtime = Mathf.Exp(elapsedtime);
        float modifX = - (this.rg.velocity.x * this.speedModificator * Time.deltaTime * elapsedtime);
        float modifY = - (this.rg.velocity.y * this.speedModificator * Time.deltaTime * elapsedtime);
        Vector2 modificator = new Vector2(modifX, modifY);
        Debug.DrawRay(this.transform.position, this.rg.velocity, Color.red, 0.2f);
    }

    public void SetCurrentSpeed(float speed)
    {
        Vector2 forward = new Vector2(this.transform.up.x, this.transform.up.y);
        this.rg.velocity = forward * speed;
    }
}
