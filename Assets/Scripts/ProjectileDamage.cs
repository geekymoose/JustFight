using UnityEngine;
using UnityEngine.Assertions;

public class ProjectileDamage : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Amount of damage the projectile does")]
    private float damageHitPoint;

    [SerializeField]
    [Tooltip("Lifetime of the projectile")]
    private float lifetimeInSec = 0;

    private float effectiveLifetime;
    private GameObject projectileShooter; // The one who did the shot

    private void Awake()
    {
        Assert.IsTrue(this.damageHitPoint > 0, "Invalid asset (damage value should be positive)");
        Assert.IsTrue(this.lifetimeInSec > 0, "Invalid Asset (Invalid lifetime)");
    }

    private void Update()
    {
        this.effectiveLifetime += Time.deltaTime;
        if(this.effectiveLifetime >= this.lifetimeInSec)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
        if(health)
        {
            health.takeDamage(this.damageHitPoint);
        }
    }
}
