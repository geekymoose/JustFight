using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField]
    [Tooltip("Player health bar UI")]
    private Slider healthSliderUI;

    [SerializeField]
    [Tooltip("Player health bar UI Canvas")]
    private GameObject healthSliderCanvasUI; // This is hacky, because we can't apply the transform on the slider directly

    private Vector3 healthSliderOffseltUI;

    private float effectiveHealth;

    private Animator anim;

    private void Awake()
    {
        this.anim = this.GetComponent<Animator>();

        Assert.IsNotNull(this.anim, "Missing asset (Animator)");
        Assert.IsTrue(this.fullHealth > 0, "Invalid asset (Max health should be positive)");
        Assert.IsNotNull(this.healthSliderUI, "Missing asset");
        Assert.IsNotNull(this.playerKilledEvent, "Missing asset (Player killed GameEvent required)");

        this.effectiveHealth = this.fullHealth;
        this.healthSliderUI.value = effectiveHealth / this.fullHealth;
        this.healthSliderOffseltUI = this.transform.position - this.healthSliderCanvasUI.transform.position;
        this.healthSliderOffseltUI.x = 0;
        this.healthSliderOffseltUI.z = 0;
    }

    private void LateUpdate()
    {
        this.healthSliderCanvasUI.transform.position = this.transform.position - this.healthSliderOffseltUI;
        this.healthSliderCanvasUI.transform.LookAt(this.transform.position - Vector3.back);
    }

    public void TakeDamage(float amount)
    {
        this.effectiveHealth -= amount;
        this.effectiveHealth = Mathf.Clamp(this.effectiveHealth, 0, this.fullHealth);
        this.healthSliderUI.value = effectiveHealth / this.fullHealth;
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
        this.playerKilledEvent.Raise();
        this.anim.SetTrigger("Dies");
        GameObject.Destroy(this.gameObject, 1); // Let the anim plays
    }
}
