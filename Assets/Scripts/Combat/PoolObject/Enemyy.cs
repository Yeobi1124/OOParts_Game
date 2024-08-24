using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Enemyy : PoolObject
{
    public int maxHealth;
    public virtual int health {get; set;}
    public int speed;
}
