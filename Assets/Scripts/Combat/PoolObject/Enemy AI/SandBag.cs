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
    public int _health;
    public override int health {
        get{return _health;}
        set{
            _health = value;
            
            if(_health <= 0 && state != State.Dead){
                EventManager.Instance.PostNotification(CombatEventType.Win, this);
            }
        }}
    
    [ContextMenu("Fill HP")]
    void FillHP(){
        health = maxHealth;
    }

    
    private void OnEnable() {
        health = maxHealth;
    }

    private void Start() {
        EventManager.Instance.AddEventListner(CombatEventType.Win, (CombatEventType Event_Type, Component component, object param) => {
            Debug.Log("Win");
            
            state = State.Stand;
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
    }
}
