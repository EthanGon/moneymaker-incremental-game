using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Building")]
public class Building : ScriptableObject
{
    public string buildingName;
    public double baseMPS;
    public double baseCost;

}
