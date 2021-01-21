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
        private GameObject currentTarget;
        private float spawningTime = 0f; // Time when the shot was created

        public float DamageEfficiencyInPercent {get; set; }
        public float MovementEfficiencyInPercent {get; set; }

        private void Awake()
        {
            this.DamageEfficiencyInPercent = 100;
            this.MovementEfficiencyInPercent = 100;

            this.rg = this.GetComponent<Rigidbody2D>();

            Assert.IsNotNull(this.rg, "Missing asset");
            Assert.IsNotNull(this.shotData, "Missing asset");
            Assert.IsNotNull(this.shotData.MovementType, "Missing asset");
        }

        private void Start()
        {
            this.spawningTime = Time.time;
            this.rg.velocity = Vector3.up * 10; // TODO TMP VALUE
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

        public List<ShotPowerEffect> GetPowerEffects()
        {
            return this.shotData.PowerEffects;
        }

        public static void InstantiateShot(WeaponController owner, ShotController shotPrefab, Transform origin, float power)
        {
            GameObject newObject = Instantiate(shotPrefab.gameObject, origin);
            ShotController newShotController = newObject.GetComponent<ShotController>();
            Assert.IsNotNull(newShotController, "Missing component");

            foreach(ShotPowerEffect effect in newShotController.GetPowerEffects())
            {
                // TODO This is WIP
                effect.Apply(power, newShotController);
            }
        }

    }
}
