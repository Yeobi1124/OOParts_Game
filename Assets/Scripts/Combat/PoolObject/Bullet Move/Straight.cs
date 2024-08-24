using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight : BulletMove
{
    Rigidbody2D rigid;
    Vector2 dir;
    private void Awake() {
        bullet = GetComponent<Bullet>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public override void Act(){
        rigid.velocity = dir * speed;
    }
    public override void Set(int? speed = null, Vector2? dir = null, GameObject target = null){
        if(speed != null)
            this.speed = (int)speed;

        if(target == null)
            this.dir = (Vector2)dir;
        else{
            this.dir = (target.transform.position - gameObject.transform.position).normalized;
        }

        if(this.dir!=null){
            transform.rotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector3.up, this.dir).eulerAngles.z);

        }
    }
}
