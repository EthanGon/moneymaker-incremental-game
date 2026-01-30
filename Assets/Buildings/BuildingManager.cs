using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour 
{
    public Dictionary<Building, BuildingState> buildingStates = new Dictionary<Building, BuildingState>();
    public static BuildingManager instance;
    public enum tradeOptions { buy, sell };
    public tradeOptions tradeState;
    public Color buttonOffColor;
    public Color buttonOnColor;
    public Button buyButton;
    public Button sellButton;

    public void Awake()
    {
        instance = this;
        tradeState = tradeOptions.buy;
        SetButtonState(buyButton, true);
        SetButtonState(sellButton, false); 
    }

    public BuildingManager GetInstance()
    {
        return instance;
    }

    // adds up all the buildings mps based on number own buildings
    public double GetTotalMPS()
    {
        double totalMPS = 0;

        foreach (var kvp in buildingStates)
        {
            var data = kvp.Key;
            var state = kvp.Value;

            totalMPS += state.GetCurrMPS() * state.numOfBuildings;
        }

        return totalMPS;
    }

    public void SetTradeToBuy()
    {
        tradeState = tradeOptions.buy;
        SetButtonState(buyButton, true);
        SetButtonState(sellButton, false);
    }

    public void SetTradeToSell()
    {
        tradeState = tradeOptions.sell;
        SetButtonState(buyButton, false);
        SetButtonState(sellButton, true);
    }

    public void SetButtonState(Button button, bool state)
    {
        if (state == true)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().color = buttonOnColor;
        }
        else
        {
            button.GetComponentInChildren<TextMeshProUGUI>().color = buttonOffColor;
        }
    }


    /*
     * Gets number of cursors to display on screen 
     */
    public int NumberOfCursorBuildings(Building building)
    {
        if (!buildingStates.ContainsKey(building))
        {
            Debug.Log("Building Not Found");
            return 0;
        }
         
        if (!building.buildingName.Equals("GamingMouse"))
        {
            return 0;
        }

        return buildingStates[building].numOfBuildings;
    }




}
