using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance;
    public Dictionary<int, string> placeLogValues;

    [Header("UI Stuff")]
    public TextMeshProUGUI moneyCounter;
    public TextMeshProUGUI placeValueText;

    [Header("Values")]
    public double numFormatted;
    public double moneyCount;
    public double moneyPerSec;
    public double placeValueOfMoney;
    public double logVal;
    public string[] placeValues;
    public int placeCount;

    public float delayTimer;
    public float delayTime;
    public string[] units;
 
    private void Awake()
    {
        placeLogValues = new Dictionary<int, string>();
        InitPlaceValues();
        
        instance = this;
        moneyCounter.text = "MONEY\n" + moneyCount.ToString("F3");
    }

    private void Update()
    {
        
        if (moneyCount < double.MaxValue)
        {
            AddMoney(moneyPerSec);
            DisplayMoneyCount();

            double tenthPower = Mathf.Floor(Mathf.Log10((float)moneyCount));
            
            double tempLogVal = logVal;
            logVal = (int)tenthPower;

            // when money counter is in the millions and above
            if (tenthPower >= 6)
            {
                // number place has been changed if logVal != previous logVal
                if (logVal != tempLogVal)
                {
                    if (placeCount == 2)
                    {
                        placeCount = -1;
                    }
                    placeCount++;
                }
            }
        }
        else if (moneyCount >= double.MaxValue)
        {
            moneyCounter.text = "INFINITY" + "\ndollars";

        }

        
        
    }

    public void AddMoney(double mps)
    {
        moneyCount += mps * Time.deltaTime;
        DisplayMoneyCount();
    }


    public void MoneyClick()
    {
        moneyCount++;
        DisplayMoneyCount();
    }

    public void DisplayMoneyCount()
    {
        double tenthPower = Mathf.Floor(Mathf.Log10((float)moneyCount)); // round down to prevent something like 99 being considers 100th place
        double place = Mathf.Pow(10, (float)tenthPower);
        placeValueOfMoney = place;
        string result = "";

        if (logVal >= 6)
        {
            
            numFormatted = (moneyCount / place);

            // How much to move decimal left based on placeCount
            for (int i = 0; i < placeCount; i++)
            {
                numFormatted *= 10;
            }
         
            result = numFormatted.ToString("F3") + "\n" + placeLogValues[(int)tenthPower] + " dollars";
        } 
        else
        {
            result = moneyCount.ToString("F3") + "\n dollars";
        }

        
        
        if (delayTimer < delayTime)
        {
            delayTimer += Time.deltaTime;
        } else
        {
            moneyCounter.text = result;
            delayTimer = 0;
        }
    }

    public void InitPlaceValues()
    {
        int num = 3;

        for (int i = 0; i < placeValues.Length; i++)
        {
            placeLogValues.Add(num, placeValues[i]);

            // the next two are still within the same place 
            for (int j = 1; j <= 2; j++)
            {
                placeLogValues.Add(num + j, placeValues[i]);
            }

            num += 3;
        }

        

    }

    public static GameLogic Instance()
    {
        return instance;
    }

}
