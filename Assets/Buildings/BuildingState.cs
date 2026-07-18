using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class BuildingState 
{
    public bool unlocked;
    public double currCost;
    private double baseMPS;
    public int[] upgradeChecker;
    private int efficiencyLevel; // how many times to multiply (baseCost * 2)
    public int numOfBuildings;
    public int numOfPossibleUpgrades = 10;
    public int scaleAmount = 20; // every x amount of buildings, u can upgrade it
    public int availableUpgrades = 0;
    public int upgradeRequirementReached = 0;

    public BuildingState(Building building)
    {
        InitMaxPossibleUpgrades();
        InitBaseValues(building);
        
    }

    private void InitBaseValues(Building building)
    {
        this.currCost = building.baseCost;
        this.baseMPS = building.baseMPS;
    }

    private void InitMaxPossibleUpgrades()
    {
        upgradeChecker = new int[numOfPossibleUpgrades];
        for (int i = 0; i < numOfPossibleUpgrades; i++)
        {
            upgradeChecker[i] = ((i + 1) * scaleAmount);
        }
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

    public void IncreaseUpgradeTokens()
    {
        this.availableUpgrades++;
    }

    public void RemoveUpgradeToken()
    {
        this.availableUpgrades--;
    }

   
}
