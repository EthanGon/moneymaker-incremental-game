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
    public double currentBuildingCost;


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
        currentBuildingCost = buildingState.currCost;
        SetButtonStates();
        UpdateButtonListeners();
    }

    public void SetButtonStates()
    {
        if (GameLogic.Instance().moneyCount >= buildingState.currCost && BuildingManager.instance.tradeState == BuildingManager.tradeOptions.buy)
        {
            this.button.interactable = true;
        }
        else if (GameLogic.Instance().moneyCount < buildingState.currCost && BuildingManager.instance.tradeState == BuildingManager.tradeOptions.buy)
        {
            this.button.interactable = false;
        }

        if (this.buildingState.numOfBuildings > 0 && BuildingManager.instance.tradeState == BuildingManager.tradeOptions.sell)
        {
            this.button.interactable = true;
        }
        else if (this.buildingState.numOfBuildings == 0 && BuildingManager.instance.tradeState == BuildingManager.tradeOptions.sell)
        {
            this.button.interactable = false;
        }
    }

    public void UpdateButtonListeners()
    {
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
        DisplayBuildingDetails();
    }

    public void SellBuilding()
    {
        if (buildingState.numOfBuildings > 0)
        {
            double previousCost = (building.baseCost * building.costMultiplier);
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
        buildingState.currCost = building.baseCost * building.costMultiplier;
        DisplayBuildingDetails();
    }

    private void DisplayBuildingDetails()
    {
        if (BuildingManager.instance.tradeState == BuildingManager.tradeOptions.buy)
        {
            buildingNameText.text = building.buildingName;
            buildingAmountText.text = "x" + buildingState.numOfBuildings;
            buildingCostText.text = "buy: " + GameLogic.instance.FormatNumber(buildingState.currCost)[0] + " " + GameLogic.instance.FormatNumber(buildingState.currCost)[1];
        }
        else
        {
            double previousCost = building.baseCost * building.costMultiplier;
            double moneyBack = previousCost;

            if (buildingState.numOfBuildings == 0)
            {
                moneyBack = 0;
            }

            if (buildingState.numOfBuildings-1 == 0)
            {
                moneyBack = building.baseCost;
            }

            buildingNameText.text = building.buildingName;
            buildingAmountText.text = "x" + buildingState.numOfBuildings;
            buildingCostText.text = "sell: " + GameLogic.instance.FormatNumber(moneyBack)[0] + GameLogic.instance.FormatNumber(moneyBack)[1];
        }
    }
}
