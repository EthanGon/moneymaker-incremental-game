using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingLogic : MonoBehaviour
{
    public Building building;
    public Button button;
    public TextMeshProUGUI buildingNameText;
    public TextMeshProUGUI buildingAmountText;
    public TextMeshProUGUI buildingCostText;


    private void Awake()
    {
        button = GetComponent<Button>();
        DisplayBuildingDetails();

        button.onClick.AddListener(BuyBuilding);
    }

    private void Update()
    {
        if (GameLogic.Instance().GetMoneyAmount() < this.building.buildingCost)
        {
            this.button.interactable = false;
        } 
        else
        {
            this.button.interactable = true;
        }

        GameLogic.Instance().AddMoney(building.moneyPerSecond * building.numberOfBuilding);

    }

    public void BuyBuilding()
    {
        building.numberOfBuilding++;
        GameLogic.Instance().moneyCount -= building.buildingCost;
        building.buildingCost += (building.numberOfBuilding * 0.25f);
        DisplayBuildingDetails();

    }

    public void DisplayBuildingDetails()
    {
        buildingNameText.text = building.name;
        buildingAmountText.text = "x" + building.numberOfBuilding.ToString();

        string updateCostString = GameLogic.Instance().FormatNumber(building.buildingCost).Replace("\n", "");
        Debug.Log(updateCostString);
        buildingCostText.text = "buy: " + updateCostString;
    }
}
