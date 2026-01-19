using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingLogic : MonoBehaviour
{
    public Building building;
    public Button button;
    public TextMeshProUGUI buildingText;


    private void Awake()
    {
        button = GetComponent<Button>();
        buildingText.text = building.name + " " + building.numberOfBuilding;
    }



    public void BuyBuilding()
    {
        building.numberOfBuilding++;
        buildingText.text = building.name + " " + building.numberOfBuilding;
        GameLogic.Instance().moneyPerSec += building.mps;
    }
}
