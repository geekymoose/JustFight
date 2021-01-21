using UnityEngine;
using UnityEngine.Assertions;

namespace WeaponSystem
{
    // TODO Remove experimental when ready
    [CreateAssetMenu(fileName = "ShotMovement_Forward", menuName = "ScriptableObjects/WeaponSystem/experimental/ShotMovement_Forward", order = 1)]
    public class ShotMovementForward : ShotMovement
    {
        [Tooltip("The forward speed (in unity force to apply)")]
        [SerializeField]
        private float speedInUnityForce;

        public override void Init(ShotController controller, Rigidbody2D rg)
        {
            // Initial speed
            rg.AddForce(Vector2.up * this.speedInUnityForce);
        }

        public override void Apply(ShotController controller, Rigidbody2D rg)
        {
            Assert.IsNotNull(controller, "Invalid parameter");
            Assert.IsNotNull(rg, "Invalid parameter");
            if(controller && rg)
            {
                // TODO Movement to fix, currently, it increase constantly
                Transform transform = rg.gameObject.transform;
                Vector2 forwardForce = new Vector2(transform.up.x, transform.up.y);
                forwardForce *= this.speedInUnityForce * Time.deltaTime;
                rg.AddForce(forwardForce);
                Debug.DrawRay(transform.position, rg.velocity, Color.blue, 0.1f);
            }
        }
    }
}