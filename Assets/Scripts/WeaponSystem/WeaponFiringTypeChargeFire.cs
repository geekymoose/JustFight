using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFiringTypeChargeFire : WeaponFiringType
{
    private Weapon weapon;

    private bool isChargingPower;
    private float chargingStartTime; // When the charging started
    private float lastFireTime; // When the weapon was last used (used for reload)

    public WeaponFiringTypeChargeFire(Weapon weapon)
    {
        this.weapon = weapon;
        this.lastFireTime = 0;
        this.chargingStartTime = 0;
        this.isChargingPower = false;
    }

    public void PrepareFire()
    {
        if(!this.IsReloading() && !this.IsChargingPower())
        {
            this.isChargingPower = true;
            this.chargingStartTime = Time.time;
        }
    }

    public void HoldFire()
    {
        // Nothing to do
    }

    public void Fire()
    {
        if(!this.IsReloading() && this.IsChargingPower())
        {
            float currentPower = this.CurrentChargedPowerInPercent();
            if(this.weapon.GetWeaponData().IsEnoughPowerToFire(currentPower))
            {
                this.weapon.InstantiateShot(currentPower);
            }
            this.Reload();
        }
    }

    public float CurrentChargedPowerInPercent()
    {
        if(this.IsChargingPower())
        {
            float full = this.weapon.GetWeaponData().FullPowerChargingSpeedInSec;
            float current = Mathf.Clamp((Time.time - this.chargingStartTime), 0, full);
            return (current * 100) / full;
        }
        else
        {
            return 0;
        }
    }

    public bool IsChargingPower()
    {
        return this.isChargingPower;
    }

    public bool IsReloading()
    {
        return Time.time <= this.lastFireTime + this.weapon.GetWeaponData().ReloadSpeedInSec;
    }

    public void Reload()
    {
        this.isChargingPower = false;
        this.lastFireTime = Time.time;
    }
}
