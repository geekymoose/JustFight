using UnityEngine;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField]
    private float maxHealth;

    private float effectiveHealth;

    private void Awake()
    {
        Assert.IsTrue(this.maxHealth > 0, "Invalid asset (Max health should be positive)");
        this.effectiveHealth = this.maxHealth;
    }

    public void takeDamage(float amount)
    {
        Debug.Log("Take damage: " + amount + " / remains: " + this.effectiveHealth);
        this.effectiveHealth -= amount;
        //this.effectiveHealth = Mathf.Clamp(this.effectiveHealth, 0, this.maxHealth);
        if(this.effectiveHealth == 0)
        {
            this.kill();
        }
    }

    public void kill()
    {
        Debug.Log("Kill player");
        GameObject.Destroy(this.gameObject);
        this.effectiveHealth = 0;
        // TODO Send event
        // TODO Destroy anim
    }
}
