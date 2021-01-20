using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/WeaponSystem/Weapon", order = 1)]
public class WeaponData : ScriptableObject
{
    [Tooltip("The prefab to instanciate when the weapon fires")]
    public ShotController ShotControllerPrefab;

    [Range(0, 10)]
    [Tooltip("Time it takes for this weapon to reload before next shoot (in seconds)")]
    public float ReloadSpeedInSec = 1.0f;

    [Tooltip("Type of fire this weapon uses")]
    public WeaponFiringTypeEnum FiringType = WeaponFiringTypeEnum.CHARGE_THEN_FIRE;

    [Range(0, 10)]
    [Tooltip("This is the time it takes to be fully loaded")]
    public float FullPowerChargingSpeedInSec = 0.5f;

    [Range(0, 100)]
    [Tooltip("Minimum power required to fire a shot (in % of the max power required)")]
    public float MinPowerRequiredInPercent = 30.0f;

    private void Awake()
    {
        Assert.IsNotNull(this.ShotControllerPrefab, "Missing asset");
    }

    public bool IsEnoughPowerToFire(float powerPercentCharge)
    {
        return powerPercentCharge >= this.MinPowerRequiredInPercent;
    }
}
