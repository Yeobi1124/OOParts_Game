using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
public class SandBag : MonoBehaviour
{
    Enemyy enemy;
    
    private void Awake() {
        enemy = GetComponent<Enemyy>();

        enemy.health = enemy.maxHealth;
    }

    private void Start() {
        EventManager.Instance.AddEventListner(CombatEventType.Win, (CombatEventType Event_Type, Component component, object param) => {
            Debug.Log("Win");
            
            enemy.isDead = true;
            gameObject.SetActive(false);
            SceneManager.LoadScene("Story");
        });
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Bullet"){
            enemy.health -= other.GetComponent<Bullet>().damage;
        }
    }

    private void Update() {
        if(enemy.health <= 0 && !enemy.isDead){
            Debug.Log("Post Event Win");
            EventManager.Instance.PostNotification(CombatEventType.Win, this);
        }
    }
}
