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
    public string selectedAchievement;

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
        achievements.Add(new Achievement("The Beginning", "Earn your first dollar.", (object o) => GameLogic.instance.moneyCount >= 1));
        achievements.Add(new Achievement("Good ol Tom", "Have $2 in your bank.", (object o) => GameLogic.instance.moneyCount >= 2));
        achievements.Add(new Achievement("He ain't got nothing on JWB!", "Have $5 in your bank.", (object o) => GameLogic.instance.moneyCount >= 5));
        achievements.Add(new Achievement("If I can prove that I never touched my balls", "Have $10 in your bank.", (object o) => GameLogic.instance.moneyCount >= 10));
        achievements.Add(new Achievement("Lucky number 7", "Have $20 in your bank.", (object o) => GameLogic.instance.moneyCount >= 20));
        achievements.Add(new Achievement("Five Zero", "Have $50 in your bank.", (object o) => GameLogic.instance.moneyCount >= 50));
        achievements.Add(new Achievement("Benjiiiii", "Have $100 in your bank.", (object o) => GameLogic.instance.moneyCount >= 100));
        achievements.Add(new Achievement("GRAND Opening.. Get it?", "Have $1000 in your bank.", (object o) => GameLogic.instance.moneyCount >= 1000));
        achievements.Add(new Achievement("It's over 9000!", "Have $9000 in your bank.", (object o) => GameLogic.instance.moneyCount >= 9000));
        achievements.Add(new Achievement("Brick or Crash?", "Have $10,000 in your bank.", (object o) => GameLogic.instance.moneyCount >= 10000));
        achievements.Add(new Achievement("Money Grinder", "Have $53,594 in your bank.", (object o) => GameLogic.instance.moneyCount >= 53594));
        achievements.Add(new Achievement("Enough cash to feed a ghost town", "Have $50,000 in your bank.", (object o) => GameLogic.instance.moneyCount >= 50000));
        achievements.Add(new Achievement("186A0", "Have $100,000 in your bank.", (object o) => GameLogic.instance.moneyCount >= 100000));
        achievements.Add(new Achievement("A Small Loan..", "Have $1,000,000 in your bank.", (object o) => GameLogic.instance.moneyCount >= 1000000));
        Invoke(nameof(CreateOwnedBuildingMilestones), 0);

        numAchievements = achievements.Count;
    }

    
    private void CreateOwnedBuildingMilestones()
    {
        int numOfMilestones = 5;
        int numOfScale = 100;
        BuildingButtonsManager bbm = BuildingButtonsManager.instance;
        BuildingManager bm = BuildingManager.instance;

        foreach (var buildingButton in bbm.buttons)
        {
            for (int i = 0; i < numOfMilestones; i++)
            {
                int numNeeded = (i + 1) * numOfScale;
                string achievementName = "x" + numNeeded + " " + buildingButton.GetComponent<BuildingLogic>().building.buildingName;
                string achievementDescription = "Own " + numNeeded + " " + buildingButton.GetComponent<BuildingLogic>().building.buildingName + " Buildings.";
                achievements.Add(new Achievement(achievementName, achievementDescription, (object o) => CheckCond(buildingButton.GetComponent<BuildingLogic>().building, numNeeded)));
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            var ach = new Steamworks.Data.Achievement(selectedAchievement);
            Debug.Log("Achievement ID: " + selectedAchievement + " was re-locked.");
            ach.Clear();
        }

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
    public static int ID = 0;
    public int ach_id;
    public Predicate<object> requirement;
  

    public Achievement(string name, string description, Predicate<object> requirement)
    {
        this.name = name;
        this.description = description;
        this.requirement = requirement;
        ach_id = ID;
        ID++;

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
            UnlockAchievement(ach_id.ToString());
            this.unlocked = true;
            
        }
    }


    public bool ConditionMet()
    {
        return requirement.Invoke(null);
    }


    public void CheckUnlockState(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);
        Debug.Log($"Ach: {id}, status: " + ach.State);
    }

    public void UnlockAchievement(string id)
    {
        var ach = new Steamworks.Data.Achievement("ACH_" + id);
        ach.Trigger();
        Debug.Log($"Ach: {id}, status: " + ach.State);
    }

    public void ClearAchievement(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);
        ach.Clear();
    }

}
