using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour 
{
    public Dictionary<Building, BuildingState> buildingStates = new Dictionary<Building, BuildingState>();
    public static BuildingManager instance;

    public void Awake()
    {
        instance = this;
    }

    public BuildingManager GetInstance()
    {
        return instance;
    }

    public double GetTotalMPS()
    {
        double totalMPS = 0;

        foreach (var kvp in buildingStates)
        {
            var data = kvp.Key;
            var state = kvp.Value;

            totalMPS += data.baseMPS * state.numOfBuildings;
        }

        return totalMPS;
    }




}
