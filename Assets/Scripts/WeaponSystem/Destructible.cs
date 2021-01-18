using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class Destructible : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Type of destructible object")]
    private DestructibleData destructibleData;

    [SerializeField]
    [Tooltip("Event raised whenever the destructible object takes damage")]
    private UnityEvent<float> takeDamageEvent;

    private void Awake()
    {
        Assert.IsNotNull(this.destructibleData, "Missing asset");
    }

    public void TakeDamage(float amount)
    {
        this.takeDamageEvent.Invoke(amount);
    }

    public DestructibleData GetDestructibleData()
    {
        return this.destructibleData;
    }
}