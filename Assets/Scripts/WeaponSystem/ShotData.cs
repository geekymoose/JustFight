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
    [Tooltip("The prefab to instanciate for this shot type")]
    private GameObject shotPrefab;

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
        Assert.IsTrue(this.damageAmount > 0, "Invalid asset (amount of damage should be positive");
        Assert.IsNotNull(this.shotPrefab.GetComponent<ShotController>(), "Missing asset (shot must have a ShotController)");
        if(this.shotMovementType != ShotMovementType.HITSCAN)
        {
            Assert.IsTrue(this.movementSpeed > 0, "Invalid asset");
        }
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

    public GameObject getShotPrefab()
    {
        return this.shotPrefab;
    }
}
