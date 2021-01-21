using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    // TODO Remove experimental when ready
    [CreateAssetMenu(fileName = "Destructible", menuName = "ScriptableObjects/WeaponSystem/experimental/Destructible", order = 1)]
    public class DestructibleData : ScriptableObject
    {
        [Tooltip("List of shots that can destruct this element")]
        public List<ShotData> affectedByShots;

        public bool IsAffectedByShot(ShotData shotData)
        {
            return this.affectedByShots.Contains(shotData);
        }
    }
}

