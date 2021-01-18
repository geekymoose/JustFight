using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "Destructible", menuName = "ScriptableObjects/WeaponSystem/Destructible", order = 1)]
public class DestructibleData : ScriptableObject
{
    [SerializeField]
    [Tooltip("List of weapons that can destruct this element")]
    private List<WeaponData> affectedByWeapons;

    private void Awake()
    {
    }

    public bool IsAffectedByWeapon(WeaponData weapon)
    {
        return this.affectedByWeapons.Contains(weapon);
    }
}
