using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonsManager : MonoBehaviour
{
    public Building[] buildings;
    public int buildingCount;
    public GameObject buildingButtonPrefab;
    public float yOffset;
    private List<GameObject> buttons = new List<GameObject>();
    public int displayCount;
    public static BuildingButtonsManager instance;

    private void Awake()
    {
        instance = this;

        for (int i = 0; i < buildings.Length; i++)
        {
            GameObject obj = Instantiate(buildingButtonPrefab, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(transform, false);
            obj.SetActive(false);
            buttons.Add(obj);
        }

        DisplayButtons();
    }

    // Update is called once per frame
    void Update()
    {


        // out of bounds error, fine for NOOOW but gotta fix it
        if (GameLogic.instance.moneyCount >= buildings[displayCount-1].baseCost && displayCount < buildings.Length)
        {
            displayCount += 2;
            DisplayButtons();
        }
        DisplayButtons();
    }

    // note I should update this so when it's called to enable buttons, it only goes through the ones that NEED to be enabled
    public void DisplayButtons()
    {
        float baseOffset = 0;

        for (int i = 0; i < buttons.Count; i++)
        {
            GameObject button = buttons[i];

            if (i < displayCount)
            {
                button.SetActive(true);
                button.GetComponent<BuildingLogic>().SetBuilding(buildings[i]);
                button.transform.localPosition = new Vector3(0 + 206.205f, baseOffset, 0); // I hard code 206.205 b/c the x kept being -206.205, and I didnt know how else to fix it lol
                baseOffset -= yOffset;

            }
            else
            {
                button.GetComponent<BuildingLogic>().SetBuilding(buildings[i]);
                button.SetActive(false);
            }

        }
    }
}
