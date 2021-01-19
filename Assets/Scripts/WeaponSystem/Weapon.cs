using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Defines how the weapon actually behaves")]
    private WeaponData weaponData;

    [SerializeField]
    [Tooltip("Transform where the projectil spawns")]
    private Transform weaponEndPoint;

    private WeaponFiringType currentWeaponFiringType;

    private void Awake()
    {
        Assert.IsNotNull(this.weaponData, "Missing asset (WeaponData required)");
        Assert.IsNotNull(this.weaponEndPoint, "Missing asset (The weapon must have a fireing endpoint)");

        switch(this.weaponData.FiringType)
        {
            case WeaponFiringTypeEnum.INSTANT_FIRE:
                this.currentWeaponFiringType = new WeaponFiringTypeInstant(this);
                break;
            case WeaponFiringTypeEnum.CHARGE_THEN_FIRE:
                this.currentWeaponFiringType = new WeaponFiringTypeChargeFire(this);
                break;
            case WeaponFiringTypeEnum.HOLD_TO_FIRE:
                this.currentWeaponFiringType = new WeaponFiringTypeHoldFire(this);
                break;
        }
    }

    public void PressFire()
    {
        this.currentWeaponFiringType.PressFire();
    }

    public void HoldFire()
    {
        this.currentWeaponFiringType.HoldFire();
    }

    public void ReleaseFire()
    {
        this.currentWeaponFiringType.ReleaseFire();
    }

    public void Reload()
    {
        this.currentWeaponFiringType.Reload();
    }

    public bool IsReloading()
    {
        return this.currentWeaponFiringType.IsReloading();
    }

    public float CurrentChargedPowerInPercent()
    {
        return this.currentWeaponFiringType.CurrentChargedPowerInPercent();
    }

    public bool IsChargingPower()
    {
        return this.currentWeaponFiringType.IsChargingPower();
    }

    public WeaponData GetWeaponData()
    {
        return this.weaponData;
    }

    public void InstantiateShot(float power)
    {
        GameObject shot = Instantiate(weaponData.ShotPrefab, this.weaponEndPoint);

        ShotController shotController = shot.AddComponent<ShotController>();
        shotController.WeaponData = this.weaponData;
        shotController.WeaponOwner = this;
        float speed = weaponData.ShotMovementSpeed * (power / 100); // power in %, fall back to 0-1

        switch(this.weaponData.ShotMovementType)
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
}
