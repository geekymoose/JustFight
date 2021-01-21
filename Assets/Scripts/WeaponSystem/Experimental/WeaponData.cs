using UnityEngine;

namespace WeaponSystem
{
    public enum WeaponFireType
    {
        INSTANT_FIRE_ENUM,
        PREPARE_FIRE_ENUM,
        NOTHIN_ENUM,
    }

    // TODO Remove experimental when ready
    [CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/WeaponSystem/experimental/Weapon", order = 1)]
    public class WeaponData : ScriptableObject
    {
        [Tooltip("The shot prefab to instanciate when the weapon fires")]
        public ShotController ShotControllerPrefab;

        [Tooltip("Firing action to do when the fire key is pressed")]
        public WeaponFireType PressFireType;

        [Tooltip("Firing action to do when the fire key is hold")]
        public WeaponFireType HoldFireType;

        [Tooltip("Firing action to do when the fire key is released")]
        public WeaponFireType ReleaseFireType;

        [Tooltip("Number of shots per seconds the weapon can fire")]
        public int FireRateInShotsPerSec = 1;

        [Tooltip("Time it takes for the weapon to have 100% of its power (in seconds)")]
        [Range(0, 5)]
        public float FullPowerChargingSpeedInSec = 1f;

        public float GetTimeBetweenTwoShots()
        {
            return 1 / this.FireRateInShotsPerSec;
        }
    }
}
