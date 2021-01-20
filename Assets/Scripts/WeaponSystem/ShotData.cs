using UnityEngine;

public enum ShotMovementType
{
    HITSCAN,
    MISSILE,
    PROJECTILE,
}

[CreateAssetMenu(fileName = "Shot", menuName = "ScriptableObjects/WeaponSystem/Shot", order = 1)]
public class ShotData : ScriptableObject
{
    [Tooltip("Type of momement the shot uses")]
    public ShotMovementType ShotMovementType = ShotMovementType.PROJECTILE;

    [Range(1,30)]
    [Tooltip("Shot speed in Unity units (not used if Hitscan)")]
    public float ShotMovementSpeed = 5;

    [Range(1,200)]
    [Tooltip("Amount of damage the shot does on the target")]
    public float ShotDamageAmount = 1;

    [Tooltip("If false, this shot passes through enemy shots (no collision)")]
    public bool collidesWithEnemyShots = false;

    [Tooltip("If false, this shot passes through friendly shots (no collision)")]
    public bool collidesWithFriendlyShots = false;

    [Tooltip("If false, this shot passes through friends")]
    public bool collidesWithFriends = false;
}
