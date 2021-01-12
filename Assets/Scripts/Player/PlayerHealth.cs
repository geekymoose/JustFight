using UnityEngine;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Event thrown when the player is killed")]
    private GameEvent playerKilledEvent;

    [SerializeField]
    [Tooltip("Event thrown when the player takes damage")]
    private GameEvent playerTakesDamageEvent;

    [SerializeField]
    [Tooltip("Amount of life the player has when he is 100% health")]
    private float fullHealth;

    private float effectiveHealth;

    private void Awake()
    {
        Assert.IsTrue(this.fullHealth > 0, "Invalid asset (Max health should be positive)");
        Assert.IsNotNull(this.playerKilledEvent, "Missing asset (Player killed GameEvent required)");
        this.effectiveHealth = this.fullHealth;
    }

    public void TakeDamage(float amount)
    {
        this.effectiveHealth -= amount;
        this.effectiveHealth = Mathf.Clamp(this.effectiveHealth, 0, this.fullHealth);
        Debug.Log("Player takes  " + amount + " damage" + amount + " / remaining: " + this.effectiveHealth);
        this.playerTakesDamageEvent.Raise();
        if(this.effectiveHealth == 0)
        {
            this.Kill();
        }
    }

    public void Kill()
    {
        Debug.Log("Player is killed");
        this.effectiveHealth = 0;
        GameObject.Destroy(this.gameObject);
        this.playerKilledEvent.Raise();
    }
}
