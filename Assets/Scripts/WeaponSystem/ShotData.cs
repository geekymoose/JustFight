using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum ShotMovementType
{
    HITSCAN,
    MISSILE,
    PROJECTILE,
}

[CreateAssetMenu(fileName = "ShotData", menuName = "ScriptableObjects/ShotData", order = 1)]
public class ShotData : ScriptableObject
{
    [SerializeField]
    [Tooltip("List of target this shot can damage")]
    private List<TargetData> affectedTargetsByShot;

    [SerializeField]
    [Tooltip("Type of momement this shot uses")]
    private ShotMovementType shotMovementType;

    [SerializeField]
    [Tooltip("Shot speed in Unity Units (not used if Hitscan)")]
    private float movementSpeed;

    [SerializeField]
    [Tooltip("Amount of damage this shot does on the target")]
    private float damageAmount;

    private void Awake()
    {
        Assert.IsNotNull(this.affectedTargetsByShot, "Missing asset");
        Assert.IsTrue(this.damageAmount > 0, "Invalid asset (amount of damage should be positive");
        if(this.shotMovementType != ShotMovementType.HITSCAN)
        {
            Assert.IsTrue(this.movementSpeed > 0, "Invalid asset");
        }
    }

    public bool canAffectTarget(TargetData targetData)
    {
        return this.affectedTargetsByShot.Contains(targetData);
    }

    public ShotMovementType GetShotMovementType()
    {
        return this.shotMovementType;
    }

    public float GetShotMovementSpeed()
    {
        return this.movementSpeed;
    }

    public float GetDamageAmount()
    {
        return this.damageAmount;
    }
}
