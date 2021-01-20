using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// The shot always use the max power (you press, you shot full power)
public class WeaponFiringTypeInstant : WeaponFiringType
{
    private Weapon weapon;

    private float lastFireTime; // When the weapon was last used (used for reload)

    public WeaponFiringTypeInstant(Weapon weapon)
    {
        this.weapon = weapon;
        this.lastFireTime = 0;
    }

    public void PressFire()
    {
        if(!this.IsReloading())
        {
            this.weapon.InstantiateNewShot(100);
            this.Reload();
        }
    }

    public void HoldFire()
    {
        // Nothing to do
    }

    public void ReleaseFire()
    {
        // Nothing to do
    }

    public void Reload()
    {
        this.lastFireTime = Time.time;
    }

    public bool IsReloading()
    {
        return Time.time <= this.lastFireTime + this.weapon.GetWeaponData().ReloadSpeedInSec;
    }

    public float CurrentChargedPowerInPercent()
    {
        return 100; // It's always 100% charged since it's instant shot
    }

    public bool IsChargingPower()
    {
        return false;
    }
}
