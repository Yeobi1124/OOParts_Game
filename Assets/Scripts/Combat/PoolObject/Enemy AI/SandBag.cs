using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class SandBag : MonoBehaviour
{
    Enemyy enemy;
    
    private void Awake() {
        enemy = GetComponent<Enemyy>();

        enemy.health = enemy.maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Bullet"){
            enemy.health -= other.GetComponent<Bullet>().damage;
        }
    }
}
