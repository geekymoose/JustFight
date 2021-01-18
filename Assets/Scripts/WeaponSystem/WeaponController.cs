using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Defines how the weapon actually behaves")]
    private WeaponData weaponData;

    [SerializeField]
    [Tooltip("Transform where the projectil spawns")]
    private Transform weaponEndPoint;

    [SerializeField]
    [Tooltip("Event raised when trying to fire but the weapon is still reloading")]
    private UnityEvent weaponReloadingEvent;

    [SerializeField]
    [Tooltip("Event raised when trying to fire but there is not enough energy")]
    private UnityEvent notEnoughPowerEvent;

    private float lastUsageTime; // When the weapon was last used (used for reload)

    private void Awake()
    {
        Assert.IsNotNull(this.weaponData, "Missing asset (WeaponData required)");
        Assert.IsNotNull(this.weaponEndPoint, "Missing asset (The weapon must have a fireing endpoint)");

        this.lastUsageTime = 0;
    }

    public void Fire(float powerAmount)
    {
        if(this.IsReloading())
        {
            Debug.Log("Unable to shoot: the weapon is still reloading");
            this.weaponReloadingEvent.Invoke();
        }
        else
        {
            powerAmount = Mathf.Clamp(powerAmount, 0, this.weaponData.GetMaxPower());
            if(weaponData.IsEnoughPowerToFire(powerAmount))
            {
                float shotSpeed = weaponData.GetShotMovementSpeed();
                float effectiveSpeed = (powerAmount / weaponData.GetMaxPower()) * shotSpeed;
                this.InstantiateShot(effectiveSpeed);
                this.Reload();
            }
            else
            {
                Debug.Log("Can't fire: not enough power");
                this.notEnoughPowerEvent.Invoke();
            }
        }
    }

    private void InstantiateShot(float speed)
    {
        GameObject shot = Instantiate(weaponData.GetShotPrefab(), this.weaponEndPoint);

        ShotController shotController = shot.GetComponent<ShotController>();
        Assert.IsNotNull(shotController, "Shot prefab doesn't have a ShotController component");
        shotController.WeaponData = this.weaponData;
        shotController.WeaponControllerOwner = this;

        switch(this.weaponData.GetShotMovementType())
        {
            case ShotMovementType.MISSILE:
                ShotMovementMissile moveM = shot.gameObject.AddComponent<ShotMovementMissile>();
                moveM.SetCurrentSpeed(speed);
                break;
            case ShotMovementType.PROJECTILE:
                ShotMovementProjectile moveP = shot.gameObject.AddComponent<ShotMovementProjectile>();
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

    public bool IsReloading()
    {
        return Time.time <= this.lastUsageTime + this.weaponData.GetReloadingSpeed();
    }

    public void Reload()
    {
        this.lastUsageTime = Time.time;
    }

    public WeaponData GetWeaponData()
    {
        return this.weaponData;
    }
}
