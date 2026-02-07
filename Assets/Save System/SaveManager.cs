using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public bool dataLoaded;

    private void Awake()
    {
        dataLoaded = false;
    }


    void Update()
    {
        // attempts to load data, incase things dont load correctly try again
        if (!dataLoaded && File.Exists(Application.persistentDataPath + "/player.save"))
        {
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
        


        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Game Saved");
            SaveSystem.SavePlayer();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(LoadGame());
        }
        


    }

   

    public IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(0);

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

        TokenManager.Instance().SetTokenCount(dataToLoad.tokenCountSaved);
        TokenManager.Instance().currentXP = dataToLoad.currentXPSaved;
        TokenManager.Instance().neededXP = dataToLoad.currentXPNeededSaved;

        dataLoaded = true;
    }

}
