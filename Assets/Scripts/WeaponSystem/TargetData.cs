using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "TargetData", menuName = "ScriptableObjects/WeaponSystem/TargetData", order = 1)]
public class TargetData : ScriptableObject
{
    [SerializeField]
    [Tooltip("List of weapons that can affect this target")]
    private List<WeaponData> affectedByWeapons;

    private void Awake()
    {
        Assert.IsNotNull(this.affectedByWeapons, "Missing assets");
    }

    public bool IsAffectedByShot(WeaponData weapon)
    {
        return this.affectedByWeapons.Contains(weapon);
    }
}
