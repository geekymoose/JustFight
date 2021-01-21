using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    // TODO Remove experimental when ready
    [CreateAssetMenu(fileName = "Shot", menuName = "ScriptableObjects/WeaponSystem/experimental/Shot", order = 1)]
    public class ShotData : ScriptableObject
    {
        [Tooltip("Type of movement this shot uses")]
        public ShotMovement MovementType;

        [Tooltip("The damage behavior to apply on collision")]
        public ShotDamage Damage;

        [Tooltip("Modificators to apply on the shot according to the power")]
        public List<ShotPowerEffect> PowerEffects;
    }
}
