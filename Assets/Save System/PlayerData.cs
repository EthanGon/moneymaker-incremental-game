using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public double moneyCountSaved;
    public BuildingState[] buildingStatesSaved;
    public int buttonDisplayCountSaved;
    

    public PlayerData()
    {
        buildingStatesSaved = new BuildingState[BuildingButtonsManager.instance.buildings.Length];

        moneyCountSaved = GameLogic.Instance().moneyCount;


        buttonDisplayCountSaved = BuildingButtonsManager.instance.displayCount;
        for (int i = 0; i < BuildingButtonsManager.instance.displayCount;  i++)
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
