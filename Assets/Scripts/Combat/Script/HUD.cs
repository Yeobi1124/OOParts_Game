using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { PlayerHealth, PlayerShield, TargetHP, ChargeBar };
    public InfoType type;
    Slider slider;
    PlayerStatus playerStatus;
    SkillManager skillManager;
    Enemyy target;

    private void Awake() {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        switch(type){
            case InfoType.PlayerHealth:
                playerStatus = CombatManager.instance.player.GetComponent<PlayerStatus>();
                slider.value = (float) playerStatus.health / playerStatus.maxHealth;
                break;
            case InfoType.PlayerShield:
                skillManager = CombatManager.instance.skill;
                slider.value = (float) skillManager.defenseGauge / skillManager.defenseMaxGauge;
                break;
            case InfoType.TargetHP:
                target = CombatManager.instance.target;
                slider.value = (float) target.health / target.maxHealth;
                break;
            case InfoType.ChargeBar:
                skillManager = CombatManager.instance.skill;
                slider.value = (float) skillManager.attackCharge / skillManager.attackMaxCharge;
                break;
            default:
                break;
        }
    }
}
