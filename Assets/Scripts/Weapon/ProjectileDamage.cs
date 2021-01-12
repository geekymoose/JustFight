using UnityEngine;
using UnityEngine.Assertions;

public class ProjectileDamage : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Amount of damage the projectile does")]
    private float damageHitPoint;

    private GameObject projectileShooter; // The one who did the shot

    private void Awake()
    {
        Assert.IsTrue(this.damageHitPoint > 0, "Invalid asset (damage value should be positive)");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
        if(health)
        {
            health.TakeDamage(this.damageHitPoint);
        }
    }
}
