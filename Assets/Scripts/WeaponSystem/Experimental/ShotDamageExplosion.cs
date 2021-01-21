using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "ShotDamageExplosion", menuName = "ScriptableObjects/WeaponSystem/experimental/ShotDamageExplosion", order = 1)]
    public class ShotDamageExplosion : ShotDamage
    {
        [Tooltip("Prefab to instanciate when the explosion happens (e.g., for rendering)")]
        public GameObject explosionPrefab;

        [Tooltip("Amount of damage the explosion does")]
        [Range(1, 200)]
        public float explosionDamageAmount = 100f;

        public override void Apply(ShotController controller, GameObject target)
        {
            // TODO
        }
    }
}
