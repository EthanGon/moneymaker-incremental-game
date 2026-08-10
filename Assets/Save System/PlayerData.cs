using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public double moneyCountSaved;
    public BuildingState[] buildingStatesSaved;
    public bool[] achievementStateSaved;
    public Buff[] activeBuffsSaved;
    public int buttonDisplayCountSaved;
    public int tokenCountSaved;
    public double currentXPSaved, currentXPNeededSaved;

    public string[] buffSaved;
    public float[] buffDurationSaved;
    

    public PlayerData()
    {
        buildingStatesSaved = new BuildingState[BuildingButtonsManager.instance.buildings.Length];
        achievementStateSaved = new bool[AchievementManager.instance.achievements.Count];

        moneyCountSaved = GameLogic.Instance().moneyCount;


        SaveBuildingStates();
        SaveAchievementData();
        SaveActiveBuffs();
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

    public void SaveActiveBuffs()
    {
        BuffManager bm = BuffManager.Instance();
        int buffCount = bm.activeBuff.Count;

        buffSaved = new string[buffCount];
        buffDurationSaved = new float[buffCount];

        for (int i = 0; i < buffCount; i++)
        {
            buffSaved[i] = bm.activeBuff[i].name;
            buffDurationSaved[i] = bm.activeBuff[i].buffCurrDurection;
        }

    }

    /* Things to save:
     * moneyCount
     * every buildings state (effLevel, numOfBuilding, hasBought or has unlocked ability to buy
     * current xp, xp needed, tokin
     */
}
