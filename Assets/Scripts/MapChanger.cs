using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapChanger : MonoBehaviour
{
    public string transferMapName;
    public GameObject startPoint;
    public Collider2D collision;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.collision = collision;
        if (this.collision.gameObject.name == "Player")
        {
            Debug.Log("1");
            StartCoroutine(MapChangeCoroutine());
        }
    }

    IEnumerator MapChangeCoroutine()
    {
        Debug.Log("2");
        GameManager.Instance.currentMapName = transferMapName;
        GameManager.Instance.fadeManager.FadeOut();
        yield return new WaitForSeconds(GameManager.Instance.fadeManager.fadeDuration * 2);
        GameManager.Instance.player.transform.position = startPoint.transform.position;
        yield return new WaitForSeconds(GameManager.Instance.fadeManager.fadeDuration);
        GameManager.Instance.fadeManager.FadeIn();
    }
}

