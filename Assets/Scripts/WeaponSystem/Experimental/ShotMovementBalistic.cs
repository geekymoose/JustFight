using UnityEngine;
using UnityEngine.Assertions;

namespace WeaponSystem
{
    // TODO Remove experimental when ready
    [CreateAssetMenu(fileName = "ShotMovementBalistic", menuName = "ScriptableObjects/WeaponSystem/experimental/ShotMovementBalistic", order = 1)]
    public class ShotMovementBalistic : ShotMovement
    {
        [SerializeField]
        [Tooltip("Each second, the speed is affected by this modificator (in Unity force)")]
        private float speedModificator = 1;

        public override void Apply(ShotController controller, Rigidbody2D rg)
        {
            Assert.IsNotNull(controller, "Invalid parameter");
            Assert.IsNotNull(rg, "Invalid parameter");
            if(controller && rg)
            {
                // This is a hacky why to recreate a pseudo gravity
                float elapsedtime = Mathf.Exp(controller.GetLifetimeDuration());
                float modifX = - (rg.velocity.x * this.speedModificator * Time.deltaTime * elapsedtime);
                float modifY = - (rg.velocity.y * this.speedModificator * Time.deltaTime * elapsedtime);

                Vector2 revertForce = new Vector2(modifX, modifY);
                rg.AddForce(revertForce);
                Debug.DrawRay(rg.transform.position, rg.velocity, Color.blue, 0.1f);
            }
        }
    }
}
