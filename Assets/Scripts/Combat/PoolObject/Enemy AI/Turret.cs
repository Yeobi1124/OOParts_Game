using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor.UIElements;
public class Turret : MonoBehaviour
{
    public int attackSpeed;
    public int attackCoolDown;
    public Enemyy enemy;
    float time;
    
    private void Awake() {
        enemy = GetComponent<Enemyy>();
        if (enemy == null)
        {
            Debug.LogError("Enemyy component is missing on " + gameObject.name);
            return;
        }
        enemy.health = enemy.maxHealth;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(time > attackCoolDown)
        {
            time = 0;
            AttackAct();
        }
    }

    private void AttackAct()
    {
        Vector2 pos = transform.position;
        Vector2 playerPos = CombatManager.instance.player.transform.localPosition;
        bool dir = pos.x < playerPos.x; //true: ¿À¸¥ÂÊ, false: ¿ÞÂÊ

        BulletMove bulletMove = CombatManager.instance.pool.Make(1, pos).GetComponent<BulletMove>();
        bulletMove.Set(attackSpeed, dir ? Vector2.right : Vector2.left);
        bulletMove.Act();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Bullet") && !other.GetComponent<Bullet>().isEnemy){
            enemy.health -= other.GetComponent<Bullet>().damage;
        }
    }
}
