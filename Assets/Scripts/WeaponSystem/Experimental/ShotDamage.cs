using UnityEngine;

namespace WeaponSystem
{
    public abstract class ShotDamage : ScriptableObject
    {
        [Tooltip("Type of damage used by this shot damage")]
        public DamageType DamageType;

        public abstract void Apply(ShotController controller, GameObject target);
    }
}
