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
        public GameObject ShotPrefab;

        public WeaponFireType PressFireType;
        public WeaponFireType HoldFireType;
        public WeaponFireType ReleaseFireType;

        public int FireRateInShotsPerSec = 1;

        public float GetTimeBetweenTwoShots()
        {
            return 1 / this.FireRateInShotsPerSec;
        }
    }
}
