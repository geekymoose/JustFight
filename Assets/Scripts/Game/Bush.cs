using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Bush : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        this.animator = this.GetComponent<Animator>();
        Assert.IsNotNull(this.animator, "Missing asset");
    }

    public void Destroy()
    {
        this.animator.SetTrigger("Destroy");
        GameObject.Destroy(this.gameObject, 1); // Let the anim plays
    }
}