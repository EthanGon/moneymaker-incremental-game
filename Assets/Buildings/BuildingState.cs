using UnityEngine;


[System.Serializable]
public class BuildingState 
{
    public int numOfBuildings;
    public double currCost;
    public int efficiencyLevel; // how many times to multiply (baseCost * 2)
    public int influenceLevel; // bonus mps given to other buildings

    public BuildingState(Building building)
    {
        this.numOfBuildings = 0;
        this.currCost = building.baseCost;
    }

}
