using UnityEngine;
using UnityEngine.Assertions;

namespace WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Defines how the weapon actually behaves")]
        private WeaponData weaponData;

        [SerializeField]
        [Tooltip("Transform where the projectil spawns")]
        private Transform weaponEndPoint;

        private WeaponFire pressFire;
        private WeaponFire holdFire;
        private WeaponFire releaseFire;
        private float lastFireTime = 0;
        private float lastPrepareTime = 0;

        private void Awake()
        {
            Assert.IsNotNull(this.weaponData, "Missing asset");
            Assert.IsNotNull(this.weaponEndPoint, "Missing asset");

            this.pressFire = this.NewFireType(this.weaponData.PressFireType);
            this.holdFire = this.NewFireType(this.weaponData.HoldFireType);
            this.releaseFire = this.NewFireType(this.weaponData.ReleaseFireType);

            Assert.IsNotNull(this.pressFire, "Missing asset");
            Assert.IsNotNull(this.holdFire, "Missing asset");
            Assert.IsNotNull(this.releaseFire, "Missing asset");
        }

        public void OnPressFire()
        {
            Debug.Log("OnPressFire()");
            this.pressFire.Apply(this);
        }

        public void OnHoldFire()
        {
            Debug.Log("OnHoldFire()");
            this.holdFire.Apply(this);
        }

        public void OnReleaseFire()
        {
            Debug.Log("OnReleaseFire()");
            this.releaseFire.Apply(this);
        }

        public void DoPrepareFire()
        {
            Debug.Log("DoPrepareFire()");
            this.lastPrepareTime = Time.time;
        }

        public void DoFire()
        {
            Debug.Log("DoFire()");
            this.lastFireTime = Time.time;
            Instantiate(this.weaponData.ShotPrefab); // TODO To update with actual shot
        }

        public bool CanFire()
        {
            return Time.time > this.lastFireTime + this.weaponData.GetTimeBetweenTwoShots();
        }

        private WeaponFire NewFireType(WeaponFireType type)
        {
            switch(type)
            {
                case WeaponFireType.INSTANT_FIRE_ENUM:
                    return new WeaponFireInstant();
                case WeaponFireType.PREPARE_FIRE_ENUM:
                    return new WeaponFirePrepare();
                case WeaponFireType.NOTHIN_ENUM:
                    return new WeaponFireNothin();
                default:
                    Assert.IsTrue(false, "Internal error");
                    return new WeaponFireNothin();
            }
        }
    }
}
