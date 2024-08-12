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
    public override void Set(int speed, Vector2? dir, GameObject target = null){
        this.speed = speed;
        this.dir = (Vector2)dir;
    }
}
