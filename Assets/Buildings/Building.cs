using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Building")]
public class Building : ScriptableObject
{
    public string buildingName;
    public double baseMPS;
    public double baseCost;
    public double normalBaseCost;
    public Sprite buildingIconn;

    public void SetNormalBaseCost(double cost)
    {
        normalBaseCost = cost;
    }

    

}
