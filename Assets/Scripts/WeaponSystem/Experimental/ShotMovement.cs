using UnityEngine;

namespace WeaponSystem
{
    public abstract class ShotMovement : ScriptableObject
    {
        public abstract void Init(ShotController controller, Rigidbody2D rg);
        public abstract void Apply(ShotController controller, Rigidbody2D rg);
    }
}
