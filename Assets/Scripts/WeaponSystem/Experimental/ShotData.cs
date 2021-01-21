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

        [Tooltip("If false, this shot does not impact with enemy shots")]
        public bool DamagedByEnemyShots = false;

        [Tooltip("If false, this shot does not impact with friendly shots")]
        public bool DamagedByFriendlyShots = false;

        [Tooltip("If false, this shot does not impact with the shooter")]
        public bool DamagedByTheShooter = false;
    }
}
