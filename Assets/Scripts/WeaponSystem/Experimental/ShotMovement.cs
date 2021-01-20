using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class ShotMovement : ScriptableObject
    {
        public abstract void Apply(ShotController controller, Rigidbody2D rg);
    }
}
