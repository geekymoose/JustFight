using UnityEngine;
using UnityEngine.Assertions;

namespace WeaponSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShotController : MonoBehaviour
    {
        [SerializeField]
        private ShotMovement movementType;

        private Rigidbody2D rg;
        private GameObject currentTarget;
        private float spawningTime = 0f; // Time when the shot was created

        private void Awake()
        {
            this.rg = this.GetComponent<Rigidbody2D>();
            Assert.IsNotNull(this.rg, "Missing asset");
            Assert.IsNotNull(this.movementType, "Missing asset");
        }

        private void Start()
        {
            this.spawningTime = Time.time;
            this.rg.velocity = Vector3.up * 10; // TODO TMP VALUE
        }

        private void FixedUpdate()
        {
            this.movementType.Apply(this, this.rg);
        }

        public Transform GetTarget()
        {
            return this.currentTarget.transform;
        }

        public float GetLifetimeDuration()
        {
            return Time.time - this.spawningTime;
        }
    }
}
