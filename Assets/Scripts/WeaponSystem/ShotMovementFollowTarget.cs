using UnityEngine;
using UnityEngine.Assertions;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "ShotMovement_FollowTarget", menuName = "ScriptableObjects/WeaponSystem/ShotMovement_FollowTarget", order = 1)]
    public class ShotMovementFollowTarget : ShotMovement
    {
        [Tooltip("The movement speed (in Unity units per seconds)")]
        public float SpeedInUnitsPerSec;

        public override void Init(ShotController controller, Rigidbody2D rg)
        {
            float speed = this.UsesPowerModificator ? controller.CalculatedValueAfterPowerModification(this.SpeedInUnitsPerSec) : this.SpeedInUnitsPerSec;
            Vector2 forward = new Vector2(controller.gameObject.transform.up.x, controller.gameObject.transform.up.y);
            rg.velocity = forward * speed;
        }

        public override void Apply(ShotController controller, Rigidbody2D rg)
        {
            Assert.IsNotNull(controller, "Invalid parameter");
            Assert.IsNotNull(rg, "Invalid parameter");
            if(controller && rg)
            {
                Transform target = controller.GetTarget();
                if(target)
                {
                    // TODO fix movement (and add power modificator)
                    Vector3 direction = target.position - rg.transform.position;
                    rg.velocity = direction.normalized * this.SpeedInUnitsPerSec;
                    Debug.DrawRay(rg.transform.position, rg.velocity, Color.blue, 0.1f);
                }
                else
                {
                    // TODO To implement
                }
            }
        }
    }

}