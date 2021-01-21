using UnityEngine;
using UnityEngine.Assertions;

namespace WeaponSystem
{
    /// Movement with an initial speed, then the speed decreases until it is null
    [CreateAssetMenu(fileName = "ShotMovement_Balistic", menuName = "ScriptableObjects/WeaponSystem/ShotMovement_Balistic", order = 1)]
    public class ShotMovementBalistic : ShotMovement
    {
        [Tooltip("Each second, the speed is affected by this modificator (in Unity force)")]
        public float SpeedModificator = 1;

        [Tooltip("Speed when movement starts")]
        public float InitSpeedInUnityUnits = 10;

        public override void Init(ShotController controller, Rigidbody2D rg)
        {
            float speed = this.UsesPowerModificator ? controller.CalculatedValueAfterPowerModification(this.InitSpeedInUnityUnits) : this.InitSpeedInUnityUnits;
            Vector2 forward = new Vector2(controller.gameObject.transform.up.x, controller.gameObject.transform.up.y);
            rg.velocity = forward * speed;
        }

        public override void Apply(ShotController controller, Rigidbody2D rg)
        {
            // TODO To re-integrate (movement is buggy)
            Assert.IsNotNull(controller, "Invalid parameter");
            Assert.IsNotNull(rg, "Invalid parameter");
            if(controller && rg)
            {
                // This is a hacky why to recreate a pseudo gravity
                float elapsedtime = Mathf.Exp(controller.GetLifetimeDuration());

                Vector2 slowForceDirection = -(rg.velocity.normalized);

                float modif = this.SpeedModificator * Time.deltaTime * elapsedtime;
                modif = Mathf.Clamp(modif, 0, rg.velocity.magnitude);

                Vector2 slowForce = slowForceDirection * modif;
                rg.AddForce(slowForce);
            }
        }
    }
}
