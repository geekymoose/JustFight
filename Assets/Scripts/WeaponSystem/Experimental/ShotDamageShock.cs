using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "ShotDamage_Shock", menuName = "ScriptableObjects/WeaponSystem/experimental/ShotDamage_Shock", order = 1)]
    public class ShotDamageShock : ShotDamage
    {
        [Tooltip("Unity force to push the target")]
        public float forceInUnityForce = 30f;

        public override void Apply(ShotController controller, GameObject target)
        {
            // TODO
            GameObject.Destroy(controller.gameObject);
        }
    }
}
