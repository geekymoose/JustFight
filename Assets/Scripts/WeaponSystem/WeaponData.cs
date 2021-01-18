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
    [SerializeField]
    [Tooltip("Time it takes for this weapon to reload before next shoot (in seconds)")]
    private float reloadSpeedInSec;

    [SerializeField]
    [Tooltip("The maximum power that can be charged in the next shot")]
    private float maxPower;

    [SerializeField]
    [Tooltip("Minimum power required to fire a shot (in % of the max power required)")]
    [Range(0,100)]
    private float minPowerRequiredInPercents;

    [SerializeField]
    [Tooltip("The prefab to instanciate when the weapon fires")]
    private GameObject shotPrefab;
    public GameObject GetShotPrefab() { return this.shotPrefab; }

    [SerializeField]
    [Tooltip("Type of momement the shot uses")]
    private ShotMovementType shotMovementType;
    public ShotMovementType GetShotMovementType() { return this.shotMovementType; }

    [SerializeField]
    [Tooltip("Shot speed in Unity units (not used if Hitscan)")]
    [Range(1,100)]
    private float shotMovementSpeed;
    public float GetShotMovementSpeed() { return this.shotMovementSpeed; }

    [SerializeField]
    [Tooltip("Amount of damage the shot does on the target")]
    [Range(1,500)]
    private float shotDamageAmount;
    public float GetShotDamageAmount() { return this.shotDamageAmount; }

    private void Awake()
    {
        Assert.IsTrue(this.reloadSpeedInSec >= 0, "Invalid assert");
        Assert.IsTrue(this.maxPower > 0, "Invalid assert");
        Assert.IsTrue(this.minPowerRequiredInPercents >= 0, "Invalid assert");
        Assert.IsTrue(this.minPowerRequiredInPercents <= 100, "Invalid assert");
        Assert.IsNotNull(this.shotPrefab, "Missing asset");
        Assert.IsTrue(this.shotMovementSpeed > 0, "Invalid assert");
        Assert.IsTrue(this.shotDamageAmount >= 0, "Invalid assert");
    }

    public bool IsEnoughPowerToFire(float powerAmount)
    {
        return powerAmount > this.GetMinPowerRequired();
    }

    public float GetMaxPower()
    {
        return this.maxPower;
    }

    public float GetMinPowerRequired()
    {
        return (this.maxPower / 100) * this.minPowerRequiredInPercents;
    }

    public float GetReloadingSpeed()
    {
        return this.reloadSpeedInSec;
    }
}
