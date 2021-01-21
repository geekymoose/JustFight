using UnityEngine;

namespace WeaponSystem
{
    public abstract class ShotDamage : ScriptableObject
    {
        [Tooltip("Type of damage used by this shot damage")]
        public DamageType DamageType;

        [Tooltip("If false, the 100% power value is always used (power often affects the amount of damage)")]
        public bool UsesPowerModificator;

        public abstract void Apply(ShotController controller, GameObject target);
    }
}
