using UnityEngine;
using UnityEngine.Assertions;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "ShotMovement_Propelled", menuName = "ScriptableObjects/WeaponSystem/ShotMovement_Propelled", order = 1)]
    public class ShotMovementPropelled : ShotMovement
    {
        [Tooltip("The forward speed (in unity force to apply)")]
        public float SpeedInUnityForce = 5;

        public override void Init(ShotController controller, Rigidbody2D rg)
        {
            float speed = this.UsesPowerModificator ? controller.CalculatedValueAfterPowerModification(this.SpeedInUnityForce) : this.SpeedInUnityForce;
            Vector2 forward = new Vector2(controller.gameObject.transform.up.x, controller.gameObject.transform.up.y);
            rg.velocity = forward * speed;
        }

        public override void Apply(ShotController controller, Rigidbody2D rg)
        {
            Assert.IsNotNull(controller, "Invalid parameter");
            Assert.IsNotNull(rg, "Invalid parameter");
            if(controller && rg)
            {
                // TODO Movement to fix, currently, it increase constantly (+ apply modificator)
                Transform transform = rg.gameObject.transform;
                Vector2 forwardForce = new Vector2(transform.up.x, transform.up.y);
                forwardForce *= this.SpeedInUnityForce * Time.deltaTime;
                rg.AddForce(forwardForce);
                Debug.DrawRay(transform.position, rg.velocity, Color.blue, 0.1f);
            }
        }
    }
}