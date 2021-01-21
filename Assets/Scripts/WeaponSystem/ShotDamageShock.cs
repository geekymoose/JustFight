using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "ShotDamage_Shock", menuName = "ScriptableObjects/WeaponSystem/ShotDamage_Shock", order = 1)]
    public class ShotDamageShock : ShotDamage
    {
        [Tooltip("Unity force to push the target")]
        public float forceInUnityForce = 30f;

        public override void Apply(ShotController controller, GameObject target)
        {
            // TODO add shock
            GameObject.Destroy(controller.gameObject);
        }
    }
}
