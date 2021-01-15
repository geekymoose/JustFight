using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ShotController : MonoBehaviour
{
    private ShotData shotData;

    private ShotMovementMissile movementMissile;
    private ShotMovementProjectile movementProjectile;

    private WeaponController weaponControllerOwner; // The one who did the shot

    private void Awake()
    {
        this.movementMissile = this.gameObject.AddComponent<ShotMovementMissile>();
        this.movementProjectile = this.gameObject.AddComponent<ShotMovementProjectile>();

        this.movementMissile.enabled = false;
        this.movementProjectile.enabled = false;

        Assert.IsNotNull(this.movementMissile, "Missing asset");
        Assert.IsNotNull(this.movementProjectile, "Missing asset");
    }

    private void Start()
    {
        Assert.IsNotNull(this.shotData, "Missing asset");
        this.SetCurrentShotSpeed(this.shotData.GetShotMovementSpeed());
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
                if(targetController.gameObject == this.weaponControllerOwner.gameObject)
                {
                    // Shot don't affect the one how sent it
                }
                else if(targetData.IsAffectedByShot(this.shotData))
                {
                    targetController.TakeDamage(this.shotData.GetDamageAmount());
                }
            }
        }

        GameObject.Destroy(this.gameObject); // TODO update with a better "Impact" system
    }

    public void SetCurrentShotSpeed(float speed)
    {
        this.movementMissile.SetCurrentSpeed(speed);
        this.movementProjectile.SetCurrentSpeed(speed);
    }

    public void SetShotOwner(WeaponController owner)
    {
        this.weaponControllerOwner = owner;
    }

    public WeaponController GetShotOwner()
    {
        return this.weaponControllerOwner;
    }

    public void SetShowData(ShotData data)
    {
        this.shotData = data;

        if(this.shotData.GetShotMovementType() == ShotMovementType.HITSCAN)
        {
            this.movementMissile.enabled = false;
            this.movementProjectile.enabled = false;
        }
        else if(this.shotData.GetShotMovementType() == ShotMovementType.MISSILE)
        {
            this.movementMissile.enabled = true;
            this.movementProjectile.enabled = false;
        }
        else if(this.shotData.GetShotMovementType() == ShotMovementType.PROJECTILE)
        {
            this.movementMissile.enabled = false;
            this.movementProjectile.enabled = true;
        }
    }
}
