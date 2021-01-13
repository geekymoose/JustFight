using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ShotDamage : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The data to use for this shot")]
    private ShotData shotData;

    private void Awake()
    {
        Assert.IsNotNull(this.shotData, "Missing asset");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TargetData targetData = collision.gameObject.GetComponent<TargetData>();

        if(targetData)
        {
            if(this.shotData.canAffectTarget(targetData))
            {
            }
        }
    }
}
