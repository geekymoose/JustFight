
public enum WeaponFiringTypeEnum
{
    INSTANT_FIRE,
    CHARGE_THEN_FIRE,
    HOLD_TO_FIRE,
}

public interface WeaponFiringType
{
    void PressFire();
    void HoldFire();
    void ReleaseFire();

    void Reload();
    bool IsReloading();

    float CurrentChargedPowerInPercent();
    bool IsChargingPower();
}
