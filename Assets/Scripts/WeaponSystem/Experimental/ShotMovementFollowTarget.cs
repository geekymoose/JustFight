using UnityEngine;
using UnityEngine.Assertions;

namespace WeaponSystem
{
    // TODO Remove experimental when ready
    [CreateAssetMenu(fileName = "ShotMovement_FollowTarget", menuName = "ScriptableObjects/WeaponSystem/experimental/ShotMovement_FollowTarget", order = 1)]
    public class ShotMovementFollowTarget : ShotMovement
    {
        [SerializeField]
        [Tooltip("The movement speed (in Unity units per seconds)")]
        private float speedInUnitsPerSec;

        public override void Init(ShotController controller, Rigidbody2D rg)
        {
            // Initial speed
            rg.velocity = Vector2.up * this.speedInUnitsPerSec;
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
                    Vector3 direction = target.position - rg.transform.position;
                    rg.velocity = direction.normalized * this.speedInUnitsPerSec;
                    Debug.DrawRay(rg.transform.position, rg.velocity, Color.blue, 0.1f);
                }
                else
                {
                    // TODO
                }
            }
        }
    }

}