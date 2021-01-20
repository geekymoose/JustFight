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

    [Tooltip("If true, this shot applies its effects when colliding with enemy shots")]
    public bool affectsEnemyShots = false;

    [Tooltip("If true, this shot applies its effects when colliding with friendly shots")]
    public bool affectsFriendlyShots = false;

    [Tooltip("If true, this shot applies its effects when colliding the one who shot it")]
    public bool affectsShooter = false;
}
