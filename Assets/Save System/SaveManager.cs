using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public bool dataLoaded;
    [Header("Money Rain Handler")]
    [SerializeField] private float autoSaveTime;
    [SerializeField] private float autoSaveTimer;
    [SerializeField] private Animator autoSaveTextAnimation;
    [SerializeField] private TextMeshProUGUI autoSaveText;

    private static SaveManager instance;

    private void Start()
    {
        instance = this;
        
    }

    private void Awake()
    {
        dataLoaded = false;
    }


    void Update()
    {
        LoadSaveAttempt();
        HandleAutoSave();

    }

    private void LoadSaveAttempt()
    {
        // will constantly attempt to load save until everything is created
        if (!dataLoaded && File.Exists(Application.persistentDataPath + "/player.save"))
        {
            LoadingScreen.GetInstance().TurnOn();

            try
            {
                StartCoroutine(LoadGame());
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message + ".. Data failed to load trying again...");
                return;
            }

        }
    }

    private void HandleAutoSave()
    {
        if (autoSaveTimer < autoSaveTime)
        {
            autoSaveTimer += Time.deltaTime;
        }
        else
        {
            autoSaveText.text = "Game Autosaved";
            autoSaveTextAnimation.SetTrigger("fade");
            Debug.Log("Autosave Triggered.");
            SaveSystem.SavePlayer();
            autoSaveTimer = 0f;
        }
    }

   

    public IEnumerator LoadGame()
    {
        
        PlayerData dataToLoad = SaveSystem.LoadPlayer();
        GameLogic.Instance().moneyCount = dataToLoad.moneyCountSaved;

        // Load data for building states like numOfBuilding, unlocked status, etc
        BuildingButtonsManager.instance.displayCount = dataToLoad.buttonDisplayCountSaved;
        for (int i = 0; i < BuildingButtonsManager.instance.displayCount; i++)
        {
            BuildingManager.GetInstance().buildingStates[BuildingButtonsManager.instance.buildings[i]] = dataToLoad.buildingStatesSaved[i];
        }

        // Load achievement data
        for (int i = 0; i < AchievementManager.instance.achievements.Count; i++)
        {
            AchievementManager.instance.achievements[i].unlocked = dataToLoad.achievementStateSaved[i];
        }

        BuffManager bm = BuffManager.Instance();
        bm.LoadSavedBuffs(dataToLoad.buffSaved, dataToLoad.buffDurationSaved);

        
        yield return new WaitForSeconds(2.5f);
        dataLoaded = true;
    }

    public static SaveManager GetInstance()
    {
        return instance;
    }

    public void ShowAutoSaveText()
    {
        autoSaveText.gameObject.SetActive(true);
    }

    [ContextMenu("Save")]
    public void SaveGame()
    {
        SaveSystem.SavePlayer();
    }

    

}
