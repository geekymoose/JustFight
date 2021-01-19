public enum WeaponFiringTypeEnum
{
    INSTANT_FIRE, // The shot always use the max power (you press, you shot full power)
    CHARGE_THEN_FIRE, // You press to charge the shot, you release to fire
    HOLD_TO_FIRE, // Continuously fire while pressing
}

public interface WeaponFiringType
{
    void PrepareFire();
    void HoldFire();
    void Fire();

    bool IsReloading();
    void Reload();

    float CurrentChargedPowerInPercent();
    bool IsChargingPower();
}
