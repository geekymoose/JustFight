using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Amount of power charged in one second (this is the loading speed)")]
    private float chargingTimeInPowerPerSec = 0;

    [SerializeField]
    [Tooltip("GameObject uses to indicate the weapon direction and reloading power")]
    private GameObject weaponDirectionIndicator; // This is hacky

    [SerializeField]
    [Tooltip("Weapon used for slot 1")]
    private WeaponController weapon1;

    [SerializeField]
    [Tooltip("Weapon used for slot 2")]
    private WeaponController weapon2;

    [SerializeField]
    [Tooltip("Weapon used for slot 3")]
    private WeaponController weapon3;

    private WeaponController currentWeapon; // Internal use

    private float effectivePower = 0;
    private bool isChargingPower = false;

    private void Awake()
    {
        Assert.IsNotNull(this.weapon1, "Missing asset (WeaponController)");
        Assert.IsNotNull(this.weapon2, "Missing asset (WeaponController)");
        Assert.IsNotNull(this.weapon3, "Missing asset (WeaponController)");
        Assert.IsNotNull(this.weaponDirectionIndicator, "Missing asset");

        Assert.IsTrue(this.chargingTimeInPowerPerSec > 0, "Invalid asset (Charging time value)");

        this.weaponDirectionIndicator.SetActive(false);
    }

    private void Update()
    {
        if(this.isChargingPower)
        {
            this.weaponDirectionIndicator.SetActive(true);
            float currentPower = Mathf.Clamp(this.effectivePower, 0, this.currentWeapon.GetWeaponData().GetMaxPower());
            float percentLoaded = currentPower / this.currentWeapon.GetWeaponData().GetMaxPower();
            float newScale = percentLoaded * 3; // *3 because in our case, scale goes from 0 to 3 (hacky)
            this.weaponDirectionIndicator.transform.localScale = new Vector3(this.weaponDirectionIndicator.transform.localScale.x, newScale, 1);
            this.effectivePower += this.chargingTimeInPowerPerSec * Time.deltaTime;
        }
        else
        {
            this.weaponDirectionIndicator.SetActive(false);
            this.effectivePower = 0;
        }
    }

    private void PrepareFire()
    {
        if(!this.currentWeapon.IsReloading())
        {
            this.isChargingPower = true;
        }
    }

    private void Fire()
    {
        this.currentWeapon.Fire(this.effectivePower);
        this.isChargingPower = false;
        this.effectivePower = 0;
    }

    public void OnInputFire1(InputAction.CallbackContext context)
    {
        this.currentWeapon = this.weapon1;
        this.HandleOnputFire(context);
    }

    public void OnInputFire2(InputAction.CallbackContext context)
    {
        this.currentWeapon = this.weapon2;
        this.HandleOnputFire(context);
    }

    public void OnInputFire3(InputAction.CallbackContext context)
    {
        this.currentWeapon = this.weapon3;
        this.HandleOnputFire(context);
    }

    private void HandleOnputFire(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                // Button pressed
                this.PrepareFire();
                break;
            case InputActionPhase.Canceled:
                // Button released
                this.Fire();
                break;
        }
    }
}
