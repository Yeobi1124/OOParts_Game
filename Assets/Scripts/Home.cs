using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Home : MonoBehaviour
{
    public TilemapRenderer OverSprite1;
    public TilemapRenderer OverSprite2;
    public float invisibillity;
    public float fadeDuration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            AtHome();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            NotAtHome();
        }
    }

    private void AtHome()
    {
        BgmManager.instance.FadeOutMusic();
        BgmManager.instance.Play(1);
        BgmManager.instance.FadeInMusic();
        StartCoroutine(Invision());
    }

    IEnumerator Invision()
    {
        float elapsedTime = 0f;
        float invision = 1f;
        UnityEngine.Color color1 = OverSprite1.material.color;
        UnityEngine.Color color2 = OverSprite2.material.color;

        while (elapsedTime < fadeDuration)
        {
            invision = Mathf.Clamp01(1 - (1 - invisibillity) * elapsedTime / fadeDuration);

            // 알파 값만 수정
            color1.a = invision;
            color2.a = invision;

            // 수정된 색상을 다시 할당
            OverSprite1.material.color = color1;
            OverSprite2.material.color = color2;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }


    private void NotAtHome()
    {
        BgmManager.instance.FadeOutMusic();
        BgmManager.instance.Play(0);
        BgmManager.instance.FadeInMusic();
        StartCoroutine(UnInvision());
    }

    IEnumerator UnInvision()
    {
        float elapsedTime = 0f;
        float invision = invisibillity;
        UnityEngine.Color color1 = OverSprite1.material.color;
        UnityEngine.Color color2 = OverSprite2.material.color;

        while (elapsedTime < fadeDuration)
        {
            invision = Mathf.Clamp01(invisibillity + (1-invisibillity)*elapsedTime/fadeDuration);

            // 알파 값만 수정
            color1.a = invision;
            color2.a = invision;

            // 수정된 색상을 다시 할당
            OverSprite1.material.color = color1;
            OverSprite2.material.color = color2;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
