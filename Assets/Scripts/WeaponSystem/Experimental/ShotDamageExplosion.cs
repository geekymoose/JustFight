using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "ShotDamage_Explosion", menuName = "ScriptableObjects/WeaponSystem/experimental/ShotDamage_Explosion", order = 1)]
    public class ShotDamageExplosion : ShotDamage
    {
        [Tooltip("Prefab to instanciate when the explosion happens (e.g., for rendering)")]
        public GameObject ExplosionPrefab;

        [Tooltip("Amount of damage the explosion does")]
        [Range(1, 200)]
        public float ExplosionDamageAmount = 100f;

        public override void Apply(ShotController controller, GameObject target)
        {
            float damages = this.UsesPowerModificator ? controller.CalculatedValueAfterPowerModification(this.ExplosionDamageAmount) : this.ExplosionDamageAmount;
            // TODO add damages to the explosion (atm, does nothing more than prefab instantiation)
            Instantiate(this.ExplosionPrefab, controller.gameObject.transform.position, controller.gameObject.transform.rotation);
            GameObject.Destroy(controller.gameObject);
        }
    }
}
