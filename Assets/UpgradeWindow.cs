using TMPro;
using UnityEngine;

public class UpgradeWindow : MonoBehaviour
{
    public Building selectedBuilding;
    public TextMeshProUGUI nameOfBuildingSelected;
    public TextMeshProUGUI buildingCurrMPS;
    private static UpgradeWindow instance;


    private void Awake()
    {
        instance = this;
    }

    public static UpgradeWindow Instance()
    {
        return instance;
    }

    public void UpgradeEffLevel()
    {
        if (TokenManager.Instance().GetTokenCount() > 0)
        {
            Debug.Log("Eff Level Increased");
            BuildingManager.instance.buildingStates[selectedBuilding].IncreaseEffLevel();
            buildingCurrMPS.text = "Mps: " + BuildingManager.instance.buildingStates[selectedBuilding].GetCurrMPS();
            TokenManager.Instance().DecreaseTokenCount();
        }
    }

    public void SetSelectedBuilding(Building b)
    {
        this.selectedBuilding = b;
        nameOfBuildingSelected.text = b.buildingName;
        buildingCurrMPS.text = "Mps: " + BuildingManager.instance.buildingStates[b].GetCurrMPS();
    }


}
