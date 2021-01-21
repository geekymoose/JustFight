using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "ShotDamage_Impact", menuName = "ScriptableObjects/WeaponSystem/ShotDamage_Impact", order = 1)]
    public class ShotDamageImpact : ShotDamage
    {
        [Tooltip("Amount of damage this shot does to the target")]
        public float amountOfDamage = 50;

        public override void Apply(ShotController controller, GameObject target)
        {
            DestructibleController destructible = target.GetComponent<DestructibleController>();
            if(destructible)
            {
                if(destructible.GetDestructibleData().IsAffectedByDamageType(controller.GetDamageType()))
                {
                    float damages = this.UsesPowerModificator ? controller.CalculatedValueAfterPowerModification(this.amountOfDamage) : this.amountOfDamage;
                    destructible.TakeDamage(damages);
                    GameObject.Destroy(controller.gameObject);
                }
            }
        }
    }
}
