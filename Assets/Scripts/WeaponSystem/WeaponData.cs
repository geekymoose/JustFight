using UnityEngine;
using UnityEngine.Assertions;

public enum ShotMovementType
{
    HITSCAN,
    MISSILE,
    PROJECTILE,
}

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/WeaponSystem/Weapon", order = 1)]
public class WeaponData : ScriptableObject
{
    [Tooltip("The prefab to instanciate when the weapon fires")]
    public GameObject ShotPrefab;

    [Tooltip("Time it takes for this weapon to reload before next shoot (in seconds)")]
    [Range(0,10)]
    public float ReloadSpeedInSec;

    [Tooltip("Type of fire this weapon uses")]
    public WeaponFiringTypeEnum FiringType;

    [Tooltip("This is the time it takes to be fully loaded")]
    [Range(0,10)]
    public float FullPowerChargingSpeedInSec;

    [Tooltip("Minimum power required to fire a shot (in % of the max power required)")]
    [Range(0,100)]
    public float MinPowerRequiredInPercent;

    [Tooltip("Type of momement the shot uses")]
    public ShotMovementType ShotMovementType;

    [Tooltip("Shot speed in Unity units (not used if Hitscan)")]
    [Range(1,30)]
    public float ShotMovementSpeed;

    [Tooltip("Amount of damage the shot does on the target")]
    [Range(1,200)]
    public float ShotDamageAmount;

    private void Awake()
    {
        Assert.IsNotNull(this.ShotPrefab, "Missing asset");
    }

    public bool IsEnoughPowerToFire(float powerPercentCharge)
    {
        return powerPercentCharge >= this.MinPowerRequiredInPercent;
    }
}
