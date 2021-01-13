using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class TargetController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The type of this target")]
    private TargetData targetData;

    [SerializeField]
    [Tooltip("Event raised whenever the target takes damage")]
    private UnityEvent<float> takeDamageEvent;

    private void Awake()
    {
        Assert.IsNotNull(this.targetData, "Missing asset (target data)");
    }

    public void TakeDamage(float amount)
    {
        Debug.Log("TargetController takes damage (" + amount + ")");
        this.takeDamageEvent.Invoke(amount);
    }

    public TargetData GetTargetData()
    {
        return this.targetData;
    }
}
