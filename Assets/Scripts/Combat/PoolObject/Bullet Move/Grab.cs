using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grab : BulletMove
{
    Vector2 dir;
    Vector2 startpos;
    public float range;
    float dist;
    public bool getSuccess;

    [ContextMenu("Grab Test")]
    void test(){
        CombatManager.instance.player.transform.position = startpos;
    }

    private void OnEnable() {
        getSuccess = false;
    }

    private void FixedUpdate() {
        transform.position = transform.position + (Vector3)(dir * speed) * Time.fixedDeltaTime;

        dist = Vector2.Distance(transform.position, startpos);

        if(dist > range)
            gameObject.SetActive(false);
    }

    public override void Act()
    {
        dist = 0;
        startpos = transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector3.up, this.dir).eulerAngles.z);
    }

    public override void Set(int? speed = null, Vector2? dir = null, GameObject target = null)
    {
        this.dir = (Vector2) dir;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            Debug.Log("Grab Player");
            other.transform.position = startpos + dir * 1.5f; //하드 코딩된 부분 나중에 수정
            //CombatManager.instance.player.transform.position = startpos;
            //test();

            //other.gameObject.GetComponent<Rigidbody2D>().MovePosition(startpos + dir * 2);
            getSuccess = true;

            Invoke("tempFunc", 0.1f);
        }
    }

    void tempFunc(){
        gameObject.SetActive(false);
    }
}
