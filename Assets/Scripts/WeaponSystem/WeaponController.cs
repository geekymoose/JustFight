using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("List of currently usable weapons")]
    private List<WeaponData> weapons;

    [SerializeField]
    [Tooltip("This is where the projectil comes from")]
    private Transform weaponEndPoint;

    private int currentWeaponIndex = 0;

    private void Awake()
    {
        Assert.IsNotNull(this.weapons, "Missing asset");
        Assert.IsNotNull(this.weaponEndPoint, "Missing asset");
        Assert.IsTrue(this.currentWeaponIndex >= 0, "Invalid asset");
        Assert.IsTrue(this.currentWeaponIndex < this.weapons.Count, "Invalid asset");
    }

    public void Fire(float powerAmount)
    {
        WeaponData currentWeapon = this.GetCurrentWeapon();
        if(currentWeapon)
        {
            Debug.Log("Fire with " + powerAmount + " of energy");
            powerAmount = Mathf.Clamp(powerAmount, 0, currentWeapon.GetMaxPower());
            if(currentWeapon.IsEnoughPowerToFire(powerAmount))
            {
                GameObject shot = Instantiate(currentWeapon.GetShotData().getShotPrefab(), this.weaponEndPoint);
                ShotController shotController = shot.GetComponent<ShotController>();
                Assert.IsNotNull(shotController, "Shot prefab doesn't have a ShotController component");
                if(shotController)
                {
                    float shotSpeed = currentWeapon.GetShotData().GetShotMovementSpeed();
                    float effectiveSpeed = (currentWeapon.GetMaxPower() / 100 * powerAmount) * shotSpeed;
                    shotController.SetCurrentShotSpeed(effectiveSpeed);
                }
            }
        }
    }

    public void SelectNextWeapon()
    {
        this.currentWeaponIndex++;
        if(this.currentWeaponIndex >= this.weapons.Count)
        {
            this.currentWeaponIndex = 0;
        }
    }

    public void SelectPreviousWeapon()
    {
        this.currentWeaponIndex--;
        if(this.currentWeaponIndex < 0)
        {
            this.currentWeaponIndex = this.weapons.Count - 1;
        }
    }

    public void SelectWeaponAt(int indice)
    {
        if(indice >= 0 && indice < this.weapons.Count)
        {
            this.currentWeaponIndex = indice;
        }
    }

    public WeaponData GetCurrentWeapon()
    {
        if(this.weapons.Count > 1)
        {
            return this.weapons[this.currentWeaponIndex];
        }
        else
        {
            return null;
        }
    }
}
