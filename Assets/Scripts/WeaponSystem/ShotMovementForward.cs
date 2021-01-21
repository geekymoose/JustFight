using UnityEngine;
using UnityEngine.Assertions;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "ShotMovement_Forward", menuName = "ScriptableObjects/WeaponSystem/ShotMovement_Forward", order = 1)]
    public class ShotMovementForward : ShotMovement
    {
        [Tooltip("The forward speed (in unity force to apply)")]
        public float SpeedInUnityForce = 5;

        public override void Init(ShotController controller, Rigidbody2D rg)
        {
            float speed = this.UsesPowerModificator ? controller.CalculatedValueAfterPowerModification(this.SpeedInUnityForce) : this.SpeedInUnityForce;
            rg.AddForce(Vector2.up * speed);
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