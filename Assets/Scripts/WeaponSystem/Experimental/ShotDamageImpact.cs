using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "ShotDamage_Impact", menuName = "ScriptableObjects/WeaponSystem/experimental/ShotDamage_Impact", order = 1)]
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
                    float effectiveDamages = this.amountOfDamage;
                    if(this.UsesPowerModificator)
                    {
                        effectiveDamages = (this.amountOfDamage * controller.EffectivePowerInPercent) / 100;
                    }

                    destructible.TakeDamage(this.amountOfDamage);
                    GameObject.Destroy(controller.gameObject);
                }
            }
        }
    }
}
