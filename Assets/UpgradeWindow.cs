using TMPro;
using UnityEngine;

public class UpgradeWindow : MonoBehaviour
{
    public Building selectedBuilding;
    public TextMeshProUGUI nameOfBuildingSelected;
    public TextMeshProUGUI buildingCurrMPS;
    public TextMeshProUGUI effCost;
    private static UpgradeWindow instance;
    private GameObject panel;


    private void Awake()
    {
        instance = this;
        panel = gameObject.transform.GetChild(0).gameObject;
        panel.SetActive(false);
    }

    public static UpgradeWindow Instance()
    {
        return instance;
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    public void UpgradeEffLevel()
    {
        int upgradeCost = (int) Mathf.Pow(2, BuildingManager.instance.buildingStates[selectedBuilding].GetEffLevel());

        if (TokenManager.Instance().GetTokenCount() > 0 && TokenManager.Instance().GetTokenCount() >= upgradeCost)
        {
            Debug.Log("Eff Level Increased");
            BuildingManager.instance.buildingStates[selectedBuilding].IncreaseEffLevel();
            buildingCurrMPS.text = "Mps: " + BuildingManager.instance.buildingStates[selectedBuilding].GetCurrMPS();
            effCost.text = "cost: " + (int)Mathf.Pow(2, BuildingManager.instance.buildingStates[selectedBuilding].GetEffLevel()); 
            TokenManager.Instance().DecreaseTokenCount();
        }
    }
  

    public void SetSelectedBuilding(Building b)
    {
        this.selectedBuilding = b;
        int upgradeCost = (int)Mathf.Pow(2, BuildingManager.instance.buildingStates[selectedBuilding].GetEffLevel());
        nameOfBuildingSelected.text = b.buildingName;
        effCost.text = "cost: " + upgradeCost;
        buildingCurrMPS.text = "Mps: " + BuildingManager.instance.buildingStates[b].GetCurrMPS();
    }


}
