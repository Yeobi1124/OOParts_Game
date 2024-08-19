using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum TypeInfo {PlayerHp, PlayerHpText, EnemyHp, ShieldGauge, ShieldReleaseGauge}

    public TypeInfo type;

    TextMeshProUGUI text;
    Slider slider;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        slider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch(type)
        {
            case TypeInfo.PlayerHp:
                float curHp = CombatManager.instance.player.GetComponent<PlayerStatus>().health;
                float maxHp = CombatManager.instance.player.GetComponent<PlayerStatus>().maxHealth;
                slider.value = curHp / maxHp;
                break;
             case TypeInfo.PlayerHpText:
                text.text= CombatManager.instance.player.GetComponent<PlayerStatus>().health.ToString();
                break;
            case TypeInfo.EnemyHp:
                float curEnHp = CombatManager.instance.enemy.GetComponent<Enemyy>().health;
                float maxEnHp = CombatManager.instance.enemy.GetComponent<Enemyy>().maxHealth;
                slider.value = curEnHp / maxEnHp;
                break;
            case TypeInfo.ShieldGauge:
                float curShieldGauge = CombatManager.instance.skill.defenseGauge;
                float maxShieldGauge = CombatManager.instance.skill.defenseMaxGauge;
                slider.value = curShieldGauge / maxShieldGauge;
                break;
            case TypeInfo.ShieldReleaseGauge:
                float curReleaseGauge = CombatManager.instance.skill.defenseReleaseGauge;
                float maxReleaseGauge = CombatManager.instance.skill.defenseReleaseMaxGauge;
                slider.value= curReleaseGauge / maxReleaseGauge;
                break;
        }
    }
}
