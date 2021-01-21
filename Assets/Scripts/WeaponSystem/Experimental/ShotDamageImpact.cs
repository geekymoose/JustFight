using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "ShotDamageImpact", menuName = "ScriptableObjects/WeaponSystem/experimental/ShotDamageImpact", order = 1)]
    public class ShotDamageImpact : ShotDamage
    {
        public float amountOfDamage = 50;

        public override void Apply(ShotController controller, GameObject target)
        {
            DestructibleController destructible = target.GetComponent<DestructibleController>();
            if(destructible)
            {
                if(destructible.GetDestructibleData().IsAffectedByDamageType(controller.GetDamageType()))
                {
                    destructible.TakeDamage(this.amountOfDamage);
                    GameObject.Destroy(controller.gameObject);
                }
            }
        }
    }
}
