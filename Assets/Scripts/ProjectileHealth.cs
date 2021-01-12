using UnityEngine;
using UnityEngine.Assertions;

public class ProjectileHealth : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Lifetime of the projectile")]
    private float lifetimeInSec = 0;

    private float effectiveLifetime;

    private void Awake()
    {
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
}
