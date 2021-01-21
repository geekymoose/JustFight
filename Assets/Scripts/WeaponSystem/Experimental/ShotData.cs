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

        [Tooltip("Amount of damage the shot does on the target")]
        [Range(1,200)]
        public float ShotDamageAmount = 1;

        [Tooltip("Modificators to apply on the shot according to the power")]
        public List<ShotPowerEffect> PowerEffects;
    }
}
