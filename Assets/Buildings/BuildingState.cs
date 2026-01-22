using UnityEngine;


[System.Serializable]
public class BuildingState 
{
    public int numOfBuildings;
    public double currCost;

    public BuildingState(Building building)
    {
        this.numOfBuildings = 0;
        this.currCost = building.baseCost;
    }

    // work on this tmm 1/22 
    public double CalculateBuildingCost()
    {
        return 0;
    }
}
