using System.Collections;
using UnityEngine;

public class SaveLogic : MonoBehaviour
{
    public int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        StartCoroutine(Load());
    }

    public IEnumerator Load()
    {
        int i = 0;

        yield return new WaitForSeconds(1.5f);
        foreach (var kvp in BuildingManager.instance.buildingStates)
        {
            var data = kvp.Key;
            var state = kvp.Value;

            string buldingNum = "b" + i.ToString() + "count";
            state.numOfBuildings = PlayerPrefs.GetInt(buldingNum);

            string effLevel = "b" + i.ToString() + "eLevel";
            state.SetEffLevel(PlayerPrefs.GetInt(effLevel));
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Data Saved");

            foreach (var kvp in BuildingManager.instance.buildingStates)
            {
                var data = kvp.Key;
                var state = kvp.Value;

                string buldingNum = "b" + index.ToString() + "count";
                PlayerPrefs.SetInt(buldingNum, state.numOfBuildings);

                string effLevel = "b" + index.ToString() + "eLevel";
                PlayerPrefs.SetInt(effLevel, state.GetEffLevel());

                index++;
            }
            index = 0;
        }
    }
}
