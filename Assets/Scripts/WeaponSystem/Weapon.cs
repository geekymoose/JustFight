using UnityEngine;
using UnityEngine.Assertions;

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

    public void InstantiateNewShot(float power)
    {
        ShotController.InstantiateShot(this, this.weaponData.ShotControllerPrefab, this.weaponEndPoint.transform, power);
    }
}
