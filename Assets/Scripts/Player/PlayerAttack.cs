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

    private float effectivePower = 0;
    private bool isChargingPower = false;
    private WeaponController weaponController;

    private void Awake()
    {
        this.weaponController = this.GetComponent<WeaponController>();
        Assert.IsNotNull(this.weaponController, "Missing asset (Player must have a WeaponController)");
        Assert.IsNotNull(this.weaponDirectionIndicator, "Missing asset");
        Assert.IsTrue(this.chargingTimeInPowerPerSec > 0, "Invalid asset (Charging time value)");

        this.weaponDirectionIndicator.SetActive(false);
    }

    private void Update()
    {
        if(this.isChargingPower)
        {
            this.weaponDirectionIndicator.SetActive(true);
            float currentPower = Mathf.Clamp(this.effectivePower, 0, this.weaponController.GetCurrentWeapon().GetMaxPower());
            float percentLoaded = currentPower / this.weaponController.GetCurrentWeapon().GetMaxPower();
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
        if(!this.weaponController.IsCurrentWeaponReloading())
        {
            this.isChargingPower = true;
        }
    }

    private void Fire()
    {
        this.weaponController.Fire(this.effectivePower);
        this.isChargingPower = false;
        this.effectivePower = 0;
    }

    public void OnInputFire1(InputAction.CallbackContext context)
    {
        this.weaponController.SelectWeaponAt(0);
        this.HandleOnputFire(context);
    }

    public void OnInputFire2(InputAction.CallbackContext context)
    {
        this.weaponController.SelectWeaponAt(1);
        this.HandleOnputFire(context);
    }

    public void OnInputFire3(InputAction.CallbackContext context)
    {
        this.weaponController.SelectWeaponAt(2);
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
