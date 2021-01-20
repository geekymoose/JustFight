using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// You press to charge the shot, you release to fire
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

    public void PressFire()
    {
        if(!this.IsReloading() && !this.IsChargingPower())
        {
            this.isChargingPower = true;
            this.chargingStartTime = Time.time;
        }
    }

    public void HoldFire()
    {
        if(!this.IsReloading() && this.IsChargingPower())
        {
            if(this.CurrentChargedPowerInPercent() >= 100)
            {
                this.ReleaseFire();
            }
        }
        else
        {
            this.PressFire();
        }
    }

    public void ReleaseFire()
    {
        if(!this.IsReloading() && this.IsChargingPower())
        {
            float currentPower = this.CurrentChargedPowerInPercent();
            if(this.weapon.GetWeaponData().IsEnoughPowerToFire(currentPower))
            {
                this.weapon.InstantiateNewShot(currentPower);
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

    public void Reload()
    {
        this.isChargingPower = false;
        this.lastFireTime = Time.time;
    }

    public bool IsReloading()
    {
        return Time.time <= this.lastFireTime + this.weapon.GetWeaponData().ReloadSpeedInSec;
    }

}
