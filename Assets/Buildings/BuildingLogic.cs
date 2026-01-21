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
    }

    public void BuyBuilding()
    {
        buildingState.numOfBuildings++;
        GameLogic.Instance().moneyCount -= buildingState.currCost;
        buildingState.currCost = Math.Ceiling((buildingState.currCost + 0.83423f * buildingState.numOfBuildings) * building.costMultiplier);
        DisplayBuildingDetails();
    }

    public void DisplayBuildingDetails()
    {
        buildingNameText.text = building.buildingName;
        buildingAmountText.text = "x" + buildingState.numOfBuildings;
        buildingCostText.text = "buy: " + GameLogic.instance.FormatNumber(buildingState.currCost);
    }
}
