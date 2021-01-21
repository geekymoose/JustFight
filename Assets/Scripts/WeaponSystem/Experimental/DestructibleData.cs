using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    // TODO Remove experimental when ready
    [CreateAssetMenu(fileName = "Destructible", menuName = "ScriptableObjects/WeaponSystem/experimental/Destructible", order = 1)]
    public class DestructibleData : ScriptableObject
    {
        [Tooltip("List the type of damage that can affect this destructible")]
        public List<DamageType> affectedByDamageType;

        public bool IsAffectedByDamageType(DamageType damageType)
        {
            return this.affectedByDamageType.Contains(damageType);
        }
    }
}
