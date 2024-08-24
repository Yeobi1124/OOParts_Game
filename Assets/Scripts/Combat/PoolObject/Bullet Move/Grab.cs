using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grab : BulletMove
{
    Vector2 dir;
    Vector2 startpos;
    public float range;
    float dist;

    private void Awake() {
    }

    private void FixedUpdate() {
        dist = Vector2.Distance(transform.position, startpos);

        if(dist > range)
            gameObject.SetActive(false);
    }

    public override void Act()
    {
        dist = 0;
        startpos = transform.position;
    }

    public override void Set(int? speed = null, Vector2? dir = null, GameObject target = null)
    {
        this.dir = (Vector2) dir;
    }
}
