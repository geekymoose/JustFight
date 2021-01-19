using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    [Tooltip("GameObject uses to indicate the weapon direction and reloading power")]
    private GameObject weaponDirectionIndicator; // This is hacky

    [SerializeField]
    [Tooltip("Weapon used for slot 1")]
    private Weapon weapon1;

    [SerializeField]
    [Tooltip("Weapon used for slot 2")]
    private Weapon weapon2;

    [SerializeField]
    [Tooltip("Weapon used for slot 3")]
    private Weapon weapon3;

    private Weapon currentWeapon; // Currently used weapon

    private void Awake()
    {
        Assert.IsNotNull(this.weapon1, "Missing asset (Weapon)");
        Assert.IsNotNull(this.weapon2, "Missing asset (Weapon)");
        Assert.IsNotNull(this.weapon3, "Missing asset (Weapon)");
        Assert.IsNotNull(this.weaponDirectionIndicator, "Missing asset");

        this.weaponDirectionIndicator.SetActive(false);
        this.currentWeapon = this.weapon1;
    }

    private void Update()
    {
        if(this.currentWeapon.IsChargingPower())
        {
            this.weaponDirectionIndicator.SetActive(true);

            float newScale = (this.currentWeapon.CurrentChargedPowerInPercent() / 100) * 3; // 3 because our scale goes from 0 to 3 (hacky)
            this.weaponDirectionIndicator.transform.localScale = new Vector3(this.weaponDirectionIndicator.transform.localScale.x, newScale, 1);
        }
        else
        {
            this.weaponDirectionIndicator.SetActive(false);
        }
    }

    private void PrepareFire()
    {
        this.currentWeapon.PrepareFire();
    }

    private void Fire()
    {
        this.currentWeapon.Fire();
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
