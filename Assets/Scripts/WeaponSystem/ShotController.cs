using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ShotController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The data to use for this shot")]
    private ShotData shotData;

    private ShotMovementMissile movementMissile;
    private ShotMovementProjectile movementProjectile;

    private void Awake()
    {
        this.movementMissile = this.gameObject.AddComponent<ShotMovementMissile>();
        this.movementProjectile = this.gameObject.AddComponent<ShotMovementProjectile>();

        this.movementMissile.enabled = false;
        this.movementProjectile.enabled = false;

        this.SetCurrentShotSpeed(this.shotData.GetShotMovementSpeed());

        Assert.IsNotNull(this.shotData, "Missing asset");
        Assert.IsNotNull(this.movementMissile, "Missing asset");
        Assert.IsNotNull(this.movementProjectile, "Missing asset");
    }

    private void Start()
    {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TargetController targetController = collision.gameObject.GetComponent<TargetController>();

        if(targetController)
        {
            TargetData targetData = targetController.GetTargetData();
            Assert.IsNotNull(targetData, "Unexpected TargetController without a targetData");
            if(targetData)
            {
                if(targetData.IsAffectedByShot(this.shotData))
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
}
