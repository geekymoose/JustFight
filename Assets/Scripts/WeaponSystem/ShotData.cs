using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "ShotData", menuName = "ScriptableObjects/ShotData", order = 1)]
public class ShotData : ScriptableObject
{
    [SerializeField]
    [Tooltip("List of target this shot can damage")]
    private List<TargetData> affectedTargetsByShot;

    [SerializeField]
    [Tooltip("Amount of damage this shot does on the target")]
    private float damageAmount;

    private void Awake()
    {
        Assert.IsNotNull(this.affectedTargetsByShot, "Missing asset");
        Assert.IsTrue(this.damageAmount > 0, "Invalid asset (amount of damage should be positive");
    }

    public bool canAffectTarget(TargetData targetData)
    {
        return this.affectedTargetsByShot.Contains(targetData);
    }
}
