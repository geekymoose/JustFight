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
        TargetController targetController = collision.gameObject.GetComponent<TargetController>();

        if(targetController)
        {
            TargetData targetData = targetController.GetTargetData();
            Assert.IsNotNull(targetData, "Unexpected TargetController without a targetData");
            if(targetData)
            {
                if(targetController.gameObject == this.WeaponControllerOwner.gameObject)
                {
                    // Shot don't affect the one how sent it
                }
                else if(targetData.IsAffectedByShot(this.WeaponData))
                {
                    targetController.TakeDamage(this.WeaponData.GetShotDamageAmount());
                }
            }
        }

        GameObject.Destroy(this.gameObject); // TODO update with a better "Impact" system
    }
}
