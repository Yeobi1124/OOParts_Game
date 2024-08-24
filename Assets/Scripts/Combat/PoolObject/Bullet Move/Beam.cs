using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : BulletMove
{
    Vector2 dir;
    public int beamMaintainTime;

    private void Awake() {
    }

    public override void Act()
    {
        StartCoroutine(beamAct());
    }

    public override void Set(int? speed = null, Vector2? dir = null, GameObject target = null)
    {
        this.dir = (Vector2) dir;
    }

    IEnumerator beamAct(){
        transform.rotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector3.up, dir).eulerAngles.z);
        transform.Translate(transform.up * transform.localScale.y/2, Space.World);


        yield return new WaitForSeconds(beamMaintainTime);

        gameObject.SetActive(false);
    }
}
