using UnityEngine;
using UnityEngine.Assertions;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "ShotMovementForward", menuName = "ScriptableObjects/WeaponSystem/ShotMovementForward", order = 1)]
    public class ShotMovementForward : ShotMovement
    {
        [SerializeField]
        [Tooltip("The forward speed (in unity force to apply)")]
        private float speedInUnityForce;

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