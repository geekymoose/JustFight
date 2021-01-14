using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "TargetData", menuName = "ScriptableObjects/TargetData", order = 1)]
public class TargetData : ScriptableObject
{
    [SerializeField]
    [Tooltip("List of shot that can affect this target")]
    private List<ShotData> affectedByShots;

    private void Awake()
    {
        Assert.IsNotNull(this.affectedByShots, "Missing assets");
    }

    public bool IsAffectedByShot(ShotData shotData)
    {
        return this.affectedByShots.Contains(shotData);
    }
}
