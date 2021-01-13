using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    [Tooltip("The shot that this weapon will shot when firing")]
    private ShotData shotData;

    [SerializeField]
    [Tooltip("Time it takes for this weapon to reload before next shoot (in seconds)")]
    private float reloadingSpeedInSec;

    [SerializeField]
    [Tooltip("The maximum power that can be charged in the next shot")]
    private float maxPower;

    [SerializeField]
    [Tooltip("Minimum power required to fire a shot (in % of the max power required)")]
    [Range(0,100)]
    private float minPowerRequiredInPercents;

    private void Awake()
    {
        Assert.IsNotNull(this.shotData, "Missing asset");
        Assert.IsTrue(this.reloadingSpeedInSec >= 0, "Invalid assert");
        Assert.IsTrue(this.maxPower > 0, "Invalid assert");
        Assert.IsTrue(this.minPowerRequiredInPercents >= 0, "Invalid assert");
        Assert.IsTrue(this.minPowerRequiredInPercents <= 100, "Invalid assert");
    }

    public bool IsEnoughPowerToFire(float powerAmount)
    {
        return powerAmount > this.GetMinPowerRequired();
    }

    public ShotData GetShotData()
    {
        return this.shotData;
    }

    public float GetMaxPower()
    {
        return this.maxPower;
    }

    public float GetMinPowerRequired()
    {
        return (this.maxPower / 100) * this.minPowerRequiredInPercents;
    }
}
