using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("List of currently usable weapons")]
    private List<WeaponData> weapons;

    [SerializeField]
    [Tooltip("This is where the projectil comes from")]
    private Transform weaponEndPoint;

    [SerializeField]
    [Tooltip("Event raised whenever the player tries to fire with not enough energy")]
    private UnityEvent notEnoughPowerEvent;

    [SerializeField]
    [Tooltip("Event raised when the player tries to fire but not weapons are available")]
    private UnityEvent noWeaponAvailableEvent;

    [SerializeField]
    [Tooltip("Event raised whenever the player tries to fire when the weapon is still reloading")]
    private UnityEvent weaponReloadingEvent;

    private int currentWeaponIndex = 0;

    // TODO warning: if the weapon list is update in the editor (during dev), this list is not updated yet
    // The unity editor action could call a script "add" method maybe
    private List<float> weaponsLastTimeUsages; // 1 to 1 link with the list of weapons (reloading for each)

    private void Awake()
    {
        Assert.IsNotNull(this.weapons, "Missing asset");
        Assert.IsNotNull(this.weaponEndPoint, "Missing asset");
        Assert.IsTrue(this.currentWeaponIndex >= 0, "Invalid asset");
        Assert.IsTrue(this.currentWeaponIndex < this.weapons.Count, "Invalid asset");
        this.weaponsLastTimeUsages = new List<float>(this.weapons.Count);
        for(int k = 0; k < this.weapons.Count; ++k)
        {
            this.weaponsLastTimeUsages.Add(Time.time);
        }
        Assert.IsTrue(this.weapons.Count == this.weaponsLastTimeUsages.Count);
    }

    public void Fire(float powerAmount)
    {
        WeaponData currentWeapon = this.GetCurrentWeapon();
        if(currentWeapon)
        {
            if(this.IsCurrentWeaponReloading())
            {
                Debug.Log("Unable to shoot: Weapon is still reloading");
                this.weaponReloadingEvent.Invoke();
            }
            else
            {
                powerAmount = Mathf.Clamp(powerAmount, 0, currentWeapon.GetMaxPower());
                if(currentWeapon.IsEnoughPowerToFire(powerAmount))
                {
                    GameObject shot = Instantiate(currentWeapon.GetShotData().getShotPrefab(), this.weaponEndPoint);
                    ShotController shotController = shot.GetComponent<ShotController>();
                    Assert.IsNotNull(shotController, "Shot prefab doesn't have a ShotController component");
                    if(shotController)
                    {
                        shotController.SetShowData(currentWeapon.GetShotData());
                        float shotSpeed = currentWeapon.GetShotData().GetShotMovementSpeed();
                        float effectiveSpeed = (powerAmount / currentWeapon.GetMaxPower()) * shotSpeed;
                        shotController.SetCurrentShotSpeed(effectiveSpeed);
                        shotController.SetShotOwner(this);
                        this.ReloadCurrentWeapon();
                    }
                }
                else
                {
                    Debug.Log("Can't fire: not enough power");
                    this.notEnoughPowerEvent.Invoke();
                }
            }
        }
        else
        {
            Debug.Log("Can't fire: no weapon available");
            this.noWeaponAvailableEvent.Invoke();
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
        if(this.weapons.Count > 0)
        {
            return this.weapons[this.currentWeaponIndex];
        }
        else
        {
            return null;
        }
    }

    public bool IsCurrentWeaponReloading()
    {

        WeaponData current = this.GetCurrentWeapon();
        float lastUsage = this.weaponsLastTimeUsages[this.currentWeaponIndex];
        return Time.time <= lastUsage + current.GetReloadingSpeed();
    }

    public void ReloadCurrentWeapon()
    {
        this.weaponsLastTimeUsages[this.currentWeaponIndex] = Time.time;
    }
}
