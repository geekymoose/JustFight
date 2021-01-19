using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Continuously fire while pressing
public class WeaponFiringTypeHoldFire : WeaponFiringType
{
    private Weapon weapon;

    public WeaponFiringTypeHoldFire(Weapon weapon)
    {
        this.weapon = weapon;
    }

    public void PressFire()
    {
        // Nothing to do
    }

    public void HoldFire()
    {
        float currentPower = this.CurrentChargedPowerInPercent();
        this.weapon.InstantiateShot(100);
    }

    public void ReleaseFire()
    {
        // Nothing to do
    }

    public void Reload()
    {
        // Nothing to do
    }

    public bool IsReloading()
    {
        return false;
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
