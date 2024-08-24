using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : BulletMove
{
    Rigidbody2D rigid;
    Vector2 dir;
    GameObject target;
    private void Awake() {
        bullet = GetComponent<Bullet>();
        rigid = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate() {
        if(target != null)
            Following();
    }

    public override void Act(){
    }
    public override void Set(int? speed = null, Vector2? dir = null, GameObject target = null){
        if(speed != null)
            this.speed = (int)speed;

        this.target = target;
    }

    private void Following(){
        dir = (target.transform.position - gameObject.transform.position).normalized;
        transform.rotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector3.up, this.dir).eulerAngles.z);

        rigid.velocity = dir * speed;
    }
}
