using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingLogic : MonoBehaviour
{
    public Building building;
    public BuildingState buildingState;
    public Button button;
    public TextMeshProUGUI buildingNameText;
    public TextMeshProUGUI buildingAmountText;
    public TextMeshProUGUI buildingCostText;


    private void Awake()
    {
        
        button = GetComponent<Button>();

        if (!BuildingManager.instance.buildingStates.ContainsKey(building))
        {
            BuildingManager.instance.buildingStates[building] = new BuildingState(building);
        }

        this.buildingState = BuildingManager.instance.buildingStates[building];

        DisplayBuildingDetails();

        button.onClick.AddListener(BuyBuilding);

        Debug.Log("# of " + buildingNameText.text + "s: " + buildingState.numOfBuildings);

        

    }

    private void Update()
    {
        if (GameLogic.Instance().moneyCount >= buildingState.currCost)
        {
            this.button.interactable = true;
        }
        else
        {
            this.button.interactable = false;
        }

        // Have to remove listener before adding new one or else it does both (idk why)
        if (BuildingManager.instance.tradeState == BuildingManager.tradeOptions.buy)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(BuyBuilding);
        }
        else
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(SellBuilding);
        }
    }

    public void SellBuilding()
    {
        if (buildingState.numOfBuildings > 0)
        {
            double previousCost = Math.Ceiling((building.baseCost + 0.83423f * buildingState.numOfBuildings - 1) * building.costMultiplier);
            GameLogic.Instance().moneyCount += previousCost;

            buildingState.numOfBuildings--;
            buildingState.currCost = previousCost;

            double moneyBack = previousCost;

            if (buildingState.numOfBuildings == 0)
            {
                buildingState.currCost = building.baseCost;
                moneyBack = building.baseCost;
            }
            Debug.Log("Player got " + moneyBack + " back.");
            DisplayBuildingDetails();
        }
    }

    public void BuyBuilding()
    {
        buildingState.numOfBuildings++;
        GameLogic.Instance().moneyCount -= buildingState.currCost;
        buildingState.currCost = Math.Ceiling((building.baseCost + 0.83423f * buildingState.numOfBuildings) * building.costMultiplier);
        DisplayBuildingDetails();
    }

    public void DisplayBuildingDetails()
    {
        buildingNameText.text = building.buildingName;
        buildingAmountText.text = "x" + buildingState.numOfBuildings;
        buildingCostText.text = "buy: " + GameLogic.instance.FormatNumber(buildingState.currCost);
    }
}
