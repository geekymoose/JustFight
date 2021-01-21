using UnityEngine;

namespace WeaponSystem
{
    public enum WeaponFireType
    {
        INSTANT_FIRE_ENUM,
        PREPARE_FIRE_ENUM,
        NOTHIN_ENUM,
    }

    [CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/WeaponSystem/Weapon", order = 1)]
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

        [Tooltip("Time it takes for the weapon to be able to shot again after a fire")]
        public float TimeBetweenTwoShotsInSec = 1;

        [Tooltip("Time it takes for the weapon to have 100% of its power (in seconds)")]
        [Range(0, 5)]
        public float FullPowerChargingSpeedInSec = 1f;
    }
}
