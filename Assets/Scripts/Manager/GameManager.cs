using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Options")]
    public string currentMapName;
    public Camera mainCamera;
    public List<ItemData> itemData;
    public EnemyData enemyData;

    [Header("Managers")]
    public PlayerManager player;
    public FadeManager fadeManager;
    public MapManager mapManager;
    public DialogueManager dialogueManager;
    public QuestManager questManager;
    public AudioManager audioManager;
    public InventoryManager inventoryManager;
    public PoolManager poolManager;
    public EncounterManager encounterManager;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    private void Start()
    {
        GameLoad();
    }

    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        for(int i =0; i < inventoryManager.inventory.Count; i++)
        {
            PlayerPrefs.SetInt("Item" + inventoryManager.inventory[i].itemId.ToString(), inventoryManager.inventory[i].itemCount);
        }
        PlayerPrefs.Save();

        dialogueManager.menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");
        for(int i =0; i < itemData.Count; i++)
        {
            
            if (PlayerPrefs.HasKey("Item" + itemData[i].itemId.ToString()))
            {
                for (int j = 0; j < PlayerPrefs.GetInt("Item" + itemData[i].itemId.ToString()); j++)
                    inventoryManager.Add(itemData[i]);
            }
        }

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
    }
    public void GameExit()
    {
        Application.Quit();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Combat");
    }

}
