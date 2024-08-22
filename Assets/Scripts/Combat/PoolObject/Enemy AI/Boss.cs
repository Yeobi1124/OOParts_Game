using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class Boss : Enemyy
{
    enum State {Stand, Attack, Dead};
    State state;
    
    private void Awake() {

        state = State.Stand;
        health = maxHealth;
    }

    private void Start() {
        EventManager.Instance.AddEventListner(CombatEventType.Win, (CombatEventType Event_Type, Component component, object param) => {
            state = State.Dead;
            gameObject.SetActive(false);
            SceneManager.LoadScene("Story");
        });
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Bullet"){
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
