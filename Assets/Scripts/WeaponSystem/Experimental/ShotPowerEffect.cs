using UnityEngine;

namespace WeaponSystem
{
    public abstract class ShotPowerEffect : ScriptableObject
    {
        public abstract void Apply(float power, ShotController controller);
    }
}
