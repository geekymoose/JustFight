using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace WeaponSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShotController : MonoBehaviour
    {
        [SerializeField]
        private ShotData shotData;

        private Rigidbody2D rg;
        private GameObject currentTarget; // TODO Add target selection
        private float spawningTime = 0f; // Time when the shot was created

        public WeaponController WeaponOrigin { get; set;} // Weapon which did the shot
        public float EffectivePowerInPercent {get; set; }

        private void Awake()
        {
            this.rg = this.GetComponent<Rigidbody2D>();
            this.EffectivePowerInPercent = 100; // By default, 100% if not set

            Assert.IsNotNull(this.rg, "Missing asset");
            Assert.IsNotNull(this.shotData, "Missing asset");
            Assert.IsNotNull(this.shotData.MovementType, "Missing asset");
        }

        private void Start()
        {
            this.spawningTime = Time.time;
            this.shotData.MovementType.Init(this, this.rg);
        }

        private void FixedUpdate()
        {
            this.shotData.MovementType.Apply(this, this.rg);
        }

        public Transform GetTarget()
        {
            return (this.currentTarget) ? this.currentTarget.transform : null;
        }

        public float GetLifetimeDuration()
        {
            return Time.time - this.spawningTime;
        }

        public DamageType GetDamageType()
        {
            return this.shotData.Damage.DamageType;
        }

        public float CalculatedValueAfterPowerModification(float value)
        {
            // Returns the value with the "power" applied on it.
            // The power is simply the percent to get of this value.
            return (this.EffectivePowerInPercent * value) / 100;
        }

        public static void InstantiateShot(WeaponController weaponOrigin, ShotController shotPrefab, Transform origin, float power)
        {
            GameObject newObject = Instantiate(shotPrefab.gameObject, origin);
            ShotController newShotController = newObject.GetComponent<ShotController>();
            Assert.IsNotNull(newShotController, "Missing asset");

            newShotController.WeaponOrigin = weaponOrigin;
            newShotController.EffectivePowerInPercent = power;
        }

        // TODO Add on trigger collisions

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check for special case with collision is actually the one who did the shot.
            // TODO Update with a better implementation (I don't know it the cost of the GetComponentsInChildren is that expensive)
            // This requires that collision is in the parent (or same gameobject)
            WeaponController[] weaponControllers = collision.gameObject.GetComponentsInChildren<WeaponController>();
            foreach(WeaponController weapon in weaponControllers)
            {
                Assert.IsNotNull(this.WeaponOrigin, "Invalid asset");
                if(weapon.gameObject == this.WeaponOrigin.gameObject)
                {
                    if(this.shotData.DamagedByTheShooter)
                    {
                        this.shotData.Damage.Apply(this, collision.gameObject);
                    }
                    else
                    {
                        // Don't do anything else
                        return;
                    }
                }
            }

            // Special case of collision with another shot
            ShotController collisionShotController = collision.gameObject.GetComponent<ShotController>();
            if(collisionShotController)
            {
                bool isFriendlyShot = collisionShotController.WeaponOrigin == this.WeaponOrigin;
                bool isEnemyShot = !isFriendlyShot;

                if(isFriendlyShot && this.shotData.DamagedByFriendlyShots)
                {
                    this.shotData.Damage.Apply(this, collision.gameObject);
                }
                else if(isEnemyShot && this.shotData.DamagedByEnemyShots)
                {
                    this.shotData.Damage.Apply(this, collision.gameObject);
                }
            }
            else
            {
                this.shotData.Damage.Apply(this, collision.gameObject);
            }
        }
    }
}
