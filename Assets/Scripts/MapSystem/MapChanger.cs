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
            StartCoroutine(MapChangeCoroutine());
        }
    }

    IEnumerator MapChangeCoroutine()
    {
        GameManager.Instance.currentMapName = transferMapName;
        GameManager.Instance.fadeManager.FadeOut();
        yield return new WaitForSeconds(GameManager.Instance.fadeManager.fadeDuration);
        GameManager.Instance.player.transform.position = startPoint.transform.position;
        GameManager.Instance.player.canMove = false;
        yield return new WaitForSeconds(GameManager.Instance.fadeManager.fadeDuration);
        GameManager.Instance.player.canMove = true;
        GameManager.Instance.fadeManager.FadeIn();
    }
}

