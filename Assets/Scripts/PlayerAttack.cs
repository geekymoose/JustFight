using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Projectile to throw")]
    private GameObject projectile;

    [SerializeField]
    [Tooltip("This is where the projectil comes from")]
    private Transform weaponEndPoint;

    [SerializeField]
    [Tooltip("Amount of power charged in one second (this is the loading speed)")]
    private float chargingTimeInPowerPerSec = 0;

    [SerializeField]
    [Tooltip("Maximum power that the weapon can have (power gives projectile speed and range")]
    private float maxPower = 0;

    private float effectivePower = 0;
    private bool isChargingPower = false;

    private void Start()
    {
        Assert.IsTrue(this.chargingTimeInPowerPerSec > 0, "Invalid asset (Charging time value)");
        Assert.IsTrue(this.maxPower > 0, "Invalid asset (Max power value)");
        Assert.IsNotNull(this.projectile, "Missing asset (projectile prefab)");
        Assert.IsNotNull(this.weaponEndPoint, "Missing asset (weapon end point)");
    }

    private void Update()
    {
        if(this.isChargingPower)
        {
            this.effectivePower += this.chargingTimeInPowerPerSec * Time.deltaTime;
            this.effectivePower = Mathf.Clamp(this.effectivePower, 0, this.maxPower);
        }
    }

    private void Fire()
    {
        GameObject newProjectile = Instantiate(this.projectile, this.weaponEndPoint);
        newProjectile.GetComponent<ProjectileMovement>().applySpeed(this.effectivePower);
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
