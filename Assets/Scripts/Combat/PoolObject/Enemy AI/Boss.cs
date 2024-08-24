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
    public int phase;
    public int _health;
    public bool actFinish;
    public override int health {
        get{return _health;}
        set{
            _health = value;

            if(state == State.Dead)
                return;
            
            if(_health <= 0){
                EventManager.Instance.PostNotification(CombatEventType.Win, this);
            }
            else if(_health <= maxHealth/2){
                phase = 1;
            }
        }
    }
    
    private void OnEnable() {
        Debug.Log("Boss OnEnable");
        phase = 0;
        state = State.Stand;
        health = maxHealth;
        actFinish = true;
        Debug.Log($"health : {health}");

        StartCoroutine(Stand());
    }

    private void Start() {
        EventManager.Instance.AddEventListner(CombatEventType.Win, (CombatEventType Event_Type, Component component, object param) => {
            state = State.Dead;
            gameObject.SetActive(false);
            //SceneManager.LoadScene("Story");
        });
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Bullet" && !other.GetComponent<Bullet>().isEnemy){
            health -= other.GetComponent<Bullet>().damage;
        }
    }

    private IEnumerator Stand(){
        state = State.Stand;

        yield return new WaitUntil(() => actFinish);
        yield return new WaitForSeconds(5);

        SelectAttack();
    }

    private void SelectAttack(){
        float rand = UnityEngine.Random.value;
        float[] phase1Prob = new float[3] {0.40f, 0.40f, 0.20f};
        float[] phase2Prob = new float[4] {0.25f, 0.25f, 0.25f, 0.25f};

        actFinish = false;

        if(phase == 0){
            // foreach(float prob in phase1Prob){
            //     if(rand <= prob){

            //     }
            // }
            if(rand <= phase1Prob[0] && rand > 0) Arrow();
            rand -= phase1Prob[0];

            if(rand <= phase1Prob[1] && rand > 0) FollowingArrow();
            rand -= phase1Prob[1];

            if(rand <= phase1Prob[2] && rand > 0) Beam(); //Attack3();
            rand -= phase1Prob[2];
        }
        else if(phase == 1){
            if(rand <= phase2Prob[0] && rand > 0) Arrow();
            rand -= phase2Prob[0];

            if(rand <= phase2Prob[1] && rand > 0) FollowingArrow();
            rand -= phase2Prob[1];

            if(rand <= phase2Prob[2] && rand > 0) Beam();
            rand -= phase2Prob[2];

            if(rand <= phase2Prob[3] && rand > 0) Grab();
            rand -= phase2Prob[3];
        }

        StartCoroutine(Stand());
    }

    #region Boss Attack
    private void Arrow(){
        Debug.Log("Arrow Attack");
        BulletMove bullet = CombatManager.instance.pool.Make(2, gameObject.transform.position).GetComponent<BulletMove>();
        bullet.Set(target: CombatManager.instance.player.gameObject);
        bullet.Act();

        actFinish = true;
    }

    private void FollowingArrow(){
        Debug.Log("Following Arrow Attack");
        BulletMove bullet = CombatManager.instance.pool.Make(3, gameObject.transform.position).GetComponent<BulletMove>();
        bullet.Set(target: CombatManager.instance.player.gameObject);
        bullet.Act();

        actFinish = true;
    }

    private void Attack3(){
        Debug.Log("Attack3 Act");

        actFinish = true;
    }

    private void Beam(){
        Debug.Log("Beam Act");
        
        StartCoroutine(BeamAct());
    }

    IEnumerator BeamAct(){
        BulletMove bulletRange = CombatManager.instance.pool.Make(5, gameObject.transform.position).GetComponent<BulletMove>();
        bulletRange.Set(dir: transform.position.x > CombatManager.instance.player.transform.position.x ? Vector2.left : Vector2.right);
        bulletRange.Act();

        BulletMove bullet = CombatManager.instance.pool.Make(4, gameObject.transform.position).GetComponent<BulletMove>();
        bullet.Set(dir: transform.position.x > CombatManager.instance.player.transform.position.x ? Vector2.left : Vector2.right);
        bullet.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f); //하드 코딩된 부분 나중에 수정 필요

        bullet.gameObject.SetActive(true);
        bullet.Act();
        actFinish = true;
        actFinish = true;
    }

    private void Grab(){
        Debug.Log("Grab Act");
        BulletMove bulletRange = CombatManager.instance.pool.Make(6, gameObject.transform.position).GetComponent<BulletMove>();
        bulletRange.Set(dir: transform.position.x > CombatManager.instance.player.transform.position.x ? Vector2.left : Vector2.right);
        bulletRange.Act();

        actFinish = true;
    }

    private void GrabLinkedAttack(){

    }
    #endregion
}
