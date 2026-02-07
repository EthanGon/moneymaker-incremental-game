using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public double moneyCountSaved;
    public BuildingState[] buildingStatesSaved;
    public bool[] achievementStateSaved;
    public int buttonDisplayCountSaved;
    public int tokenCountSaved;
    public double currentXPSaved, currentXPNeededSaved;
    

    public PlayerData()
    {
        buildingStatesSaved = new BuildingState[BuildingButtonsManager.instance.buildings.Length];
        achievementStateSaved = new bool[AchievementManager.instance.achievements.Count];

        moneyCountSaved = GameLogic.Instance().moneyCount;

        tokenCountSaved = TokenManager.Instance().GetTokenCount();
        currentXPSaved = TokenManager.Instance().currentXP;
        currentXPNeededSaved = TokenManager.Instance().neededXP;

        SaveBuildingStates();
        SaveAchievementData();
    }

    public void SaveAchievementData()
    {
        // save achievements
        for (int i = 0; i < AchievementManager.instance.achievements.Count; i++)
        {
            achievementStateSaved[i] = AchievementManager.instance.achievements[i].unlocked;
        }
    }

    public void SaveBuildingStates()
    {
        buttonDisplayCountSaved = BuildingButtonsManager.instance.displayCount;
        for (int i = 0; i < BuildingButtonsManager.instance.displayCount; i++)
        {
            buildingStatesSaved[i] = BuildingManager.GetInstance().buildingStates[BuildingButtonsManager.instance.buildings[i]];
        }
    }

    /* Things to save:
     * moneyCount
     * every buildings state (effLevel, numOfBuilding, hasBought or has unlocked ability to buy
     * current xp, xp needed, tokin
     */
}
