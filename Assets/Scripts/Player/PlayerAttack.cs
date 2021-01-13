using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Amount of power charged in one second (this is the loading speed)")]
    private float chargingTimeInPowerPerSec = 0;

    private float effectivePower = 0;
    private bool isChargingPower = false;
    private WeaponController weaponController;

    private void Awake()
    {
        this.weaponController = this.GetComponent<WeaponController>();
        Assert.IsNotNull(this.weaponController, "Missing asset (Player must have a WeaponController)");
        Assert.IsTrue(this.chargingTimeInPowerPerSec > 0, "Invalid asset (Charging time value)");
    }

    private void Update()
    {
        if(this.isChargingPower)
        {
            this.effectivePower += this.chargingTimeInPowerPerSec * Time.deltaTime;
        }
        else
        {
            this.effectivePower = 0;
        }
    }

    private void Fire()
    {
        this.weaponController.Fire(this.effectivePower);
        this.isChargingPower = false;
        this.effectivePower = 0;
    }

    public void OnInputFire(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                // Button pressed
                this.isChargingPower = true;
                break;
            case InputActionPhase.Canceled:
                // Button released
                this.Fire();
                break;
        }
    }
}
