using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWindow : MonoBehaviour
{
    [SerializeField] private Building selectedBuilding;
    [SerializeField] private TextMeshProUGUI nameOfBuildingSelected;
    [SerializeField] private TextMeshProUGUI buildingCurrMPS;
    [SerializeField] private TextMeshProUGUI buldingLevel;
    [SerializeField] private TextMeshProUGUI buildingUpgradeTokens;
    [SerializeField] private Button upgradeButton;
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
        if (BuildingManager.instance.buildingStates[selectedBuilding].availableUpgrades > 0)
        {
            Debug.Log("Eff Level Increased");
            BuildingManager.instance.buildingStates[selectedBuilding].IncreaseEffLevel();
            BuildingManager.instance.buildingStates[selectedBuilding].RemoveUpgradeToken();
            SetSelectedBuilding(selectedBuilding);
            
             
        }


    }

    public void HandleUpgradeButtonState()
    {
        if (BuildingManager.instance.buildingStates[selectedBuilding].availableUpgrades > 0)
        {
            EnableUpgradeButton();
        }
        else
        {
            DisableUpgradeButton();
        }
    }
  

    public void SetSelectedBuilding(Building b)
    {
        BuildingManager bm = BuildingManager.GetInstance();

        this.selectedBuilding = b;
        int upgradeCost = (int)Mathf.Pow(2, bm.buildingStates[selectedBuilding].GetEffLevel());
        nameOfBuildingSelected.text = "[" + b.buildingName + "]";
        buildingCurrMPS.text = "Mps: " + bm.buildingStates[b].GetCurrMPS();
        buldingLevel.text = "Level: " + bm.buildingStates[b].GetEffLevel();
        buildingUpgradeTokens.text = "Upgrade Tokens: " + bm.buildingStates[b].availableUpgrades;
        HandleUpgradeButtonState();
    }

    public void EnableUpgradeButton()
    {
        upgradeButton.interactable = true;
    }

    public void DisableUpgradeButton()
    {
        upgradeButton.interactable = false;
    }


}
