using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    protected Bullet bullet;
    protected int speed;
    public virtual void Act(){}
    public virtual void Set(int speed, Vector2? dir = null, GameObject target = null){}
}
