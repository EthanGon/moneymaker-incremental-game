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


            PrintPlaceValue();

            double tenthPower = Mathf.Floor(Mathf.Log10((float)moneyCount));
            if (tenthPower >= 6)
            {
                placeValueText.text = placeLogValues[(int)tenthPower];
            }

            double tempLogVal = logVal;
            logVal = (int)tenthPower;

            

            if (tenthPower >= 6)
            {
                // number place has been changed
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
        PrintPlaceValue();
    }


    public void MoneyClick()
    {
        moneyCount++;
        PrintPlaceValue();
    }

    public void PrintPlaceValue()
    {
        double tenthPower = Mathf.Floor(Mathf.Log10((float)moneyCount)); // round down to prevent something like 99 being considers 100th place
        double place = Mathf.Pow(10, (float)tenthPower);
        placeValueOfMoney = place;

        
        if (logVal >= 6)
        {
            
            numFormatted = (moneyCount / place);

            // How much to move decimal left based on placeCount
            if (placeCount == 1)
            {
                numFormatted *= 10;
            }

            if (placeCount == 2)
            {
                numFormatted *= 10;
                numFormatted *= 10;
            }


            moneyCounter.text = numFormatted.ToString("F3") + "\n dollars";
        } 
        else
        {
            moneyCounter.text = moneyCount.ToString("F3") + "\n dollars";
        }

        
    }

    public void InitPlaceValues()
    {
        int num = 3;

        for (int i = 0; i < placeValues.Length; i++)
        {
            placeLogValues.Add(num, placeValues[i]);

            for (int j = 1; j <= 2; j++)
            {
                placeLogValues.Add(num + j, placeValues[i]);
            }

            num += 3;
        }

        //Log Dictionary
        foreach (KeyValuePair<int, string> pair in placeLogValues)
        {
            Debug.Log(pair.Key + " : " + pair.Value);
        }
    }

}
