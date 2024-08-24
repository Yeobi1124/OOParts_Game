using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : BulletMove
{
    Vector2 dir;

    SpriteRenderer rend;
    private void Awake() {
        rend = GetComponent<SpriteRenderer>();
    }

    public override void Act(){
        rend.flipX = dir == Vector2.left ? true : false;

        transform.position += (Vector3)dir * 1.5f;

        StartCoroutine(meleeAttack());
    }
    public override void Set(int? speed = null, Vector2? dir = null, GameObject target = null){
        this.dir = (Vector2)dir;
    }

    IEnumerator meleeAttack(){
        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);
    }
}
