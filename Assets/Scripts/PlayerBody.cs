using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public MovingObject movingObject;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        anim.SetBool("Walking",movingObject.anim.GetBool("Walking"));
        anim.SetFloat("DirX", movingObject.anim.GetFloat("DirX"));
        anim.SetFloat("DirY", movingObject.anim.GetFloat("DirY"));
    }
}
