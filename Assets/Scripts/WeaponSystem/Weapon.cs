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

        // TODO Actuall set the weapon firing type
        this.currentWeaponFiringType = new WeaponFiringTypeChargeFire(this);
    }

    public void PrepareFire()
    {
        this.currentWeaponFiringType.PrepareFire();
    }

    public void HoldFire()
    {
        this.currentWeaponFiringType.HoldFire();
    }

    public void Fire()
    {
        this.currentWeaponFiringType.Fire();
    }

    public bool IsReloading()
    {
        return this.currentWeaponFiringType.IsReloading();
    }

    public void Reload()
    {
        this.currentWeaponFiringType.Reload();
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
        float speed = weaponData.ShotMovementSpeed * (power/100); // power in %, fall back to 0-1
        Debug.Log(speed);


        switch(this.weaponData.ShotMovementType)
        {
            case ShotMovementType.MISSILE:
                ShotMovementMissile moveM = shot.gameObject.AddComponent<ShotMovementMissile>();
                moveM.SetCurrentSpeed(power);
                break;
            case ShotMovementType.PROJECTILE:
                ShotMovementProjectile moveP = shot.gameObject.AddComponent<ShotMovementProjectile>();
                moveP.SetCurrentSpeed(power);
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
