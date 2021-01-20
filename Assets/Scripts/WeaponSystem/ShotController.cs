using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ShotController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The data used for this shot")]
    private ShotData ShotData;

    [SerializeField]
    [Tooltip("List of effects to apply on impact")]
    private List<GameObject> effectsOnImpact;

    public Weapon WeaponOwner { get; set;} // The one who did the shot

    private void Start()
    {
        Assert.IsNotNull(this.ShotData, "Missing asset");
        //Assert.IsNotNull(this.WeaponOwner, "Missing asset"); // TODO
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destructible destructible = collision.gameObject.GetComponent<Destructible>();
        ShotController shotController = collision.gameObject.GetComponent<ShotController>();

        // Special case of collision with another shot
        if(shotController)
        {
            bool isFriendlyShot = shotController.WeaponOwner == this.WeaponOwner;
            bool isEnemyShot = shotController.WeaponOwner != this.WeaponOwner;

            bool applyCollision = false;
            if(isFriendlyShot && this.ShotData.collidesWithFriendlyShots)
            {
                applyCollision = true;
            }
            else if(isEnemyShot && this.ShotData.collidesWithEnemyShots)
            {
                applyCollision = true;
            }

            if(!(applyCollision && destructible))
            {
                // Hack: bypass the collision process
                return;
            }
        }

        if(destructible)
        {
            DestructibleData destructibleData = destructible.GetDestructibleData();
            Assert.IsNotNull(destructibleData, "Missing asset"); // Internal error (DestructibleData required)
            if(destructibleData)
            {
                bool collidesWithOwner = (destructible.gameObject == this.WeaponOwner.gameObject);
                if(collidesWithOwner)
                {
                    if(this.ShotData.collidesWithFriends)
                    {
                        this.ApplyOnImpact();
                    }
                }
                else if(destructibleData.IsAffectedByShot(this.ShotData))
                {
                    destructible.TakeDamage(this.ShotData.ShotDamageAmount);
                    this.ApplyOnImpact();
                }
                else
                {
                    this.ApplyOnImpact();
                }
            }
        }
        else
        {
            this.ApplyOnImpact();
        }
    }

    private void ApplyOnImpact()
    {
        foreach (GameObject effect in this.effectsOnImpact)
        {
            Instantiate(effect, this.gameObject.transform.position, this.gameObject.transform.rotation);
        }
        GameObject.Destroy(this.gameObject);
    }

    public void SetShotData(ShotData shotData)
    {
        this.ShotData = shotData;
    }

    public void SetShotOwner(Weapon owner)
    {
        this.WeaponOwner = owner;
    }

    public static void InstantiateShot(Weapon owner, ShotController shotPrefab, Transform origin, float power)
    {
        GameObject newObject = Instantiate(shotPrefab.gameObject, origin);
        ShotController newShotController = newObject.GetComponent<ShotController>();
        Assert.IsNotNull(newShotController, "Missing component");
        ShotData shotData = shotPrefab.ShotData;
        float speed = shotData.ShotMovementSpeed * (power / 100); // power in %, fall back to 0-1

        newShotController.SetShotData(shotData);
        newShotController.SetShotOwner(owner);

        switch(shotData.ShotMovementType)
        {
            case ShotMovementType.MISSILE:
                ShotMovementMissile moveM = newObject.AddComponent<ShotMovementMissile>();
                moveM.SetCurrentSpeed(speed);
                break;
            case ShotMovementType.PROJECTILE:
                ShotMovementProjectile moveP = newObject.AddComponent<ShotMovementProjectile>();
                moveP.SetCurrentSpeed(speed);
                break;
            case ShotMovementType.HITSCAN:
                // Nothing for now
                break;
            default:
                Assert.IsTrue(false, "ShotMovementType not implemented");
                break;
        }
    }
}
