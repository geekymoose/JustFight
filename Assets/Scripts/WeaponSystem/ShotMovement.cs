using UnityEngine;

namespace WeaponSystem
{
    public abstract class ShotMovement : ScriptableObject
    {
        [Tooltip("If false, the 100% power value is always used (power often affects the movement speed")]
        public bool UsesPowerModificator;

        public abstract void Init(ShotController controller, Rigidbody2D rg);
        public abstract void Apply(ShotController controller, Rigidbody2D rg);
    }
}
