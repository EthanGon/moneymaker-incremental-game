using UnityEngine;

[System.Serializable]
public class BuildingState 
{
    public int numOfBuildings;
    public double currCost;
    private int efficiencyLevel; // how many times to multiply (baseCost * 2)
    private double baseMPS;
    public bool unlocked;

    public BuildingState(Building building)
    {
        this.currCost = building.baseCost;
        this.baseMPS = building.baseMPS;
    }

    // Returns the buildings currMPS based on what the efficiencyLevel is
    public double GetCurrMPS()
    {
        double temp = baseMPS;

        for (int i = 0; i < efficiencyLevel; i++)
        {
            temp = temp * 2;
        }

        return temp;
    }

    public void SetEffLevel(int n)
    {
        this.efficiencyLevel = n;
    }

    public int GetEffLevel()
    {
        return efficiencyLevel;
    }


    public void IncreaseEffLevel()
    {
        this.efficiencyLevel++;
    }

   
}
