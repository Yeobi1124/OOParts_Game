using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{
    private void FixedUpdate() {
        gameObject.transform.position = CombatManager.instance.cam.WorldToScreenPoint(CombatManager.instance.player.transform.position);
    }
}
