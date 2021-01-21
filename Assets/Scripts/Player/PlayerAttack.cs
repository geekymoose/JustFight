using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using WeaponSystem;

public class PlayerAttack : MonoBehaviour
{
    [Tooltip("GameObject uses to indicate the weapon direction and reloading power")]
    [SerializeField]
    private GameObject weaponDirectionIndicator; // This is hacky

    [Tooltip("Weapon used for slot 1")]
    [SerializeField]
    private WeaponController weapon1;

    [Tooltip("Weapon used for slot 2")]
    [SerializeField]
    private WeaponController weapon2;

    [Tooltip("Weapon used for slot 3")]
    [SerializeField]
    private WeaponController weapon3;

    private WeaponController currentWeapon; // Currently used weapon

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
        float power = this.currentWeapon.GetCurrentPower();
        if(power > 0)
        {
            this.weaponDirectionIndicator.SetActive(true);

            float newScale = (power / 100) * 3; // 3 because our scale goes from 0 to 3 (hacky)
            this.weaponDirectionIndicator.transform.localScale = new Vector3(this.weaponDirectionIndicator.transform.localScale.x, newScale, 1);
        }
        else
        {
            this.weaponDirectionIndicator.SetActive(false);
        }
    }

    private void PressFire()
    {
        this.currentWeapon.OnPressFire();
    }

    private void ReleaseFire()
    {
        this.currentWeapon.OnReleaseFire();
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
                this.PressFire();
                break;
            case InputActionPhase.Canceled:
                // Button released
                this.ReleaseFire();
                break;
        }
    }
}
