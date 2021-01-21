using UnityEngine;
using UnityEngine.Assertions;

namespace WeaponSystem
{
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
            rg.velocity = Vector2.up * speed;
        }

        public override void Apply(ShotController controller, Rigidbody2D rg)
        {
            Assert.IsNotNull(controller, "Invalid parameter");
            Assert.IsNotNull(rg, "Invalid parameter");
            if(controller && rg)
            {
                // This is a hacky why to recreate a pseudo gravity
                float elapsedtime = Mathf.Exp(controller.GetLifetimeDuration());
                float modifX = - (rg.velocity.x * this.SpeedModificator * Time.deltaTime * elapsedtime);
                float modifY = - (rg.velocity.y * this.SpeedModificator * Time.deltaTime * elapsedtime);

                Vector2 revertForce = new Vector2(modifX, modifY);
                rg.AddForce(revertForce);
                Debug.DrawRay(rg.transform.position, rg.velocity, Color.blue, 0.1f);
            }
        }
    }
}
