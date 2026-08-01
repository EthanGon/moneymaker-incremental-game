using UnityEngine;

[CreateAssetMenu(fileName = "Buff", menuName = "Scriptable Objects/Buff")]
public class Buff : ScriptableObject
{
    public string buffName;
    public string buffDescription;
    public float buffMaxDuration;
    public float buffCurrDurection;
    public bool isActive;

    public void ResetDurr()
    {
        buffCurrDurection = buffMaxDuration;
    }

}
