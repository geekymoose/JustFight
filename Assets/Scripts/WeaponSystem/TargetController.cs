using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TargetController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The type of this target")]
    private TargetData targetData;

    private void Awake()
    {
        Assert.IsNotNull(this.targetData, "Missing asset (target data)");
    }

    public TargetData GetTargetData()
    {
        return this.targetData;
    }
}
