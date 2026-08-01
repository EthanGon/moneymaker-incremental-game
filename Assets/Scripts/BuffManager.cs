using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    [SerializeField] private List<Buff> buffs;
    [SerializeField] private bool doubleMoneyActive;
    [SerializeField] private int numBuffsActive;
    private static BuffManager instance;
    public GameObject[] buffStatusUI;
    public List<Buff> activeBuff;
   

    void Start()
    {
        instance = this;

        foreach (var buff in buffStatusUI)
        {
            buff.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        RedoUI();
    }

    public void GivePlayerPowerUp()
    {
        Buff buffToGive = PickRandomBuff();
        if (IsBuffActiveAlready(buffToGive))
        {
            Debug.Log("Buff Already Active");
            FindAndResetValue(buffToGive);
        }
        else
        {
            buffToGive.buffCurrDurection = 0f;
            buffToGive.isActive = true;
            activeBuff.Add(buffToGive);
            buffStatusUI[activeBuff.Count - 1].GetComponent<BuffStatus>().AddBuff(buffToGive);
        }
    }

    public void RedoUI()
    {

        for (int i = 0; i < activeBuff.Count; i++)
        {
            buffStatusUI[i].SetActive(true);
            buffStatusUI[i].GetComponent<BuffStatus>().assignedBuff = activeBuff[i];
            buffStatusUI[i].GetComponent<BuffStatus>().UpdateUI(activeBuff[i]);
        }

        if (activeBuff.Count != 3)
        {
            for (int i = activeBuff.Count; i < 3; i++)
            {
                buffStatusUI[i].GetComponent<BuffStatus>().assignedBuff = null;
                buffStatusUI[i].SetActive(false);
               
            }
        }


    }


    public static BuffManager Instance()
    {
        return instance;
    }

    public bool DoubleMPSActive()
    {
        foreach (Buff buff in buffs)
        {
            if (buff.buffName.Equals("2X MPS") && buff.isActive)
            {
                return true;
            }

        }

        return false;
    }

    public Buff PickRandomBuff()
    {
        return buffs[Random.Range(0, buffs.Count)];
    }

    public bool IsBuffActiveAlready(Buff buffToCheck)
    {
        foreach (var buff in buffStatusUI)
        {
            if (buff.GetComponent<BuffStatus>().assignedBuff == buffToCheck)
            {
                return true;
            }
        }

        return false;
    }

    public void FindAndResetValue(Buff buffToReset)
    {
        foreach (var buff in buffStatusUI)
        {
            if (buff.GetComponent<BuffStatus>().assignedBuff == buffToReset)
            {
                buff.GetComponent<BuffStatus>().ResetDuration();
            }
        }
    }

    public void RemoveFromActiveBuff(Buff buffToRemove)
    {
        activeBuff.Remove(buffToRemove);
       
    }

    public bool IsBuffActive(string buffName)
    {
        if (activeBuff.Count == 0)
        {
            return false;
        }

        foreach (var buff in activeBuff)
        {
            if (!buff.buffName.Equals(buffName))
            {
                return true;
            }
        }

        return false;
    }

   
}
