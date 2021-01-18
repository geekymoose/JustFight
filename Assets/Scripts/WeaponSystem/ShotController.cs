using UnityEngine;
using UnityEngine.Assertions;

public class ShotController : MonoBehaviour
{
    public WeaponData WeaponData { get; set; }
    public WeaponController WeaponControllerOwner { get; set;} // The one who did the shot

    private void Start()
    {
        Assert.IsNotNull(this.WeaponData, "Missing asset");
        Assert.IsNotNull(this.WeaponControllerOwner, "Missing asset");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destructible destructible = collision.gameObject.GetComponent<Destructible>();

        if(destructible)
        {
            DestructibleData destructibleData = destructible.GetDestructibleData();
            Assert.IsNotNull(destructibleData, "Missing asset");
            if(destructibleData)
            {
                if(destructible.gameObject == this.WeaponControllerOwner.gameObject)
                {
                    // Shot don't affect the one how sent it
                }
                else if(destructibleData.IsAffectedByWeapon(this.WeaponData))
                {
                    destructible.TakeDamage(this.WeaponData.GetShotDamageAmount());
                }
            }
        }

        GameObject.Destroy(this.gameObject); // TODO update with a better "Impact" system
    }
}
