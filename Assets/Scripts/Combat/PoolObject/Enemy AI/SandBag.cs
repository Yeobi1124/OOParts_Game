using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
public class SandBag : Enemyy
{
    enum State {Stand, Attack, Dead};
    State state;
    
    private void Awake() {

        health = maxHealth;
    }

    private void Start() {
        EventManager.Instance.AddEventListner(CombatEventType.Win, (CombatEventType Event_Type, Component component, object param) => {
            Debug.Log("Win");
            
            state = State.Dead;
            gameObject.SetActive(false);
            SceneManager.LoadScene("Story");
        });
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Bullet") && !other.GetComponent<Bullet>().isEnemy)
        {
            health -= other.GetComponent<Bullet>().damage;
        }
    }

    private void Update() {
        if(health <= 0 && state != State.Dead){
            Debug.Log("Post Event Win");
            EventManager.Instance.PostNotification(CombatEventType.Win, this);
        }
    }
}
