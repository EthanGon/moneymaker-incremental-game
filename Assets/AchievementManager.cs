using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;
    public List<Achievement> achievements;
    public int numAchievements;
    public int achievementsUnlocked;
    public int maxAchievementsOnScreen;
    public List<Achievement> recentAchievements;
    public GameObject[] popUpsHolder;
    public float timer;
    public float maxTimeOnScreen;

    private void Awake()
    {
        for (int i = 0; i < popUpsHolder.Length; i++)
        {
            popUpsHolder[i].SetActive(false);
        }

        recentAchievements = new List<Achievement>();
        instance = this;
        CreateAchievements();
    }

    public void CreateAchievements()
    {
        achievements = new List<Achievement>();

        // Achievement Based on Dollar Bills
        achievements.Add(new Achievement("The Beginning.", "Earn your first dollar.", (object o) => GameLogic.instance.moneyCount >= 1));
        achievements.Add(new Achievement("Good ol Tom.", "Have $2 in your bank.", (object o) => GameLogic.instance.moneyCount >= 2));
        achievements.Add(new Achievement("He ain't got nothing on JWB!", "Have $5 in your bank.", (object o) => GameLogic.instance.moneyCount >= 5));
        achievements.Add(new Achievement("If I can prove that I never touched my balls.", "Have $10 in your bank.", (object o) => GameLogic.instance.moneyCount >= 10));
        achievements.Add(new Achievement("Lucky number 7", "Have $20 in your bank.", (object o) => GameLogic.instance.moneyCount >= 20));
        achievements.Add(new Achievement("Five Zero", "Have $50 in your bank.", (object o) => GameLogic.instance.moneyCount >= 50));
        achievements.Add(new Achievement("Benjiiiii", "Have $100 in your bank.", (object o) => GameLogic.instance.moneyCount >= 100));
        Invoke(nameof(CreateOwnedBuildingMilestones), 1.5f);

        numAchievements = achievements.Count;
    }

    
    private void CreateOwnedBuildingMilestones()
    {
        int numOfMilestones = 5;
        int numOfScale = 1;
        BuildingButtonsManager bbm = BuildingButtonsManager.instance;
        BuildingManager bm = BuildingManager.instance;


        foreach (var building in bm.buildingStates)
        {
            Debug.Log("bm name " + building.Key.buildingName);
            for (int i = 0; i < numOfMilestones; i++)
            {
                int numNeeded = (i + 1) * numOfScale;
                string achievementName = "X" + numNeeded + " " + building.Key.buildingName;
                string achievementDescription = "Own " + numNeeded + " " + building.Key.buildingName + " Buildings.";
                achievements.Add(new Achievement(achievementName, achievementDescription, (object o) => CheckCond(building.Key, numNeeded)));
            }
        }




    }

    public bool CheckCond(Building key, int numReq)
    {
        BuildingManager bm = BuildingManager.instance;

        if (!bm.buildingStates.ContainsKey(key))
        {
            return false;
        }

        

        return bm.buildingStates[key].numOfBuildings >= numReq;
    }
    

   
    
    private void Update()
    {
        CheckAchievementCompletion();
        RemoveRecentUnlockFromUI();
        DisplayRecentAchievements();

   

    }

    private void DisplayRecentAchievements()
    {
        for (int i = 0; i < 4; i++)
        {
            popUpsHolder[i].SetActive(false);
        }

        for (int i = 0; i < recentAchievements.Count; i++)
        {
            popUpsHolder[i].SetActive(true);
            popUpsHolder[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = "[" + recentAchievements[i].name + "]";
            popUpsHolder[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = "> " + recentAchievements[i].description;
        }
    }

    private void RemoveRecentUnlockFromUI()
    {
        if (recentAchievements.Count > 0)
        {
            if (timer < maxTimeOnScreen)
            {
                timer += Time.deltaTime;
            }
            else
            {
                recentAchievements.RemoveAt(0);
                timer = 0;
            }
        }
        else
        {
            timer = 0;
        }
    }

    private void CheckAchievementCompletion()
    {
        if (achievements == null)
            return;

        foreach (var achievement in achievements)
        {
            achievement.UpdateState();
        }
    }


}

[System.Serializable]
public class Achievement
{
    public string name;
    public string description;
    public bool unlocked;
    public Predicate<object> requirement;
  

    public Achievement(string name, string description, Predicate<object> requirement)
    {
        this.name = name;
        this.description = description;
        this.requirement = requirement;
    }

    public void UpdateState()
    {
        if (unlocked)
        {
            return;
        }

        if (ConditionMet())
        {
            
            if (AchievementManager.instance.recentAchievements.Count < 4)
            {
                AchievementManager.instance.recentAchievements.Add(this);
            }
            else
            {
                AchievementManager.instance.recentAchievements.RemoveAt(3);
                AchievementManager.instance.recentAchievements.Insert(0, this);
            }

                Debug.Log($"{name}: {description}");
            this.unlocked = true;
        }
    }


    public bool ConditionMet()
    {
        return requirement.Invoke(null);
    }

}
