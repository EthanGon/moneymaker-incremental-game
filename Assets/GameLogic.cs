using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance;
    public Dictionary<int, string> placeLogValues;
    public TextMeshProUGUI moneyCounter;
    public TextMeshProUGUI placeValueText;
    public double numFormatted;
    public double moneyCount;
    public double moneyPerSec;
    public double placeValueOfMoney;
    public double logVal;
    public string[] placeValues;
 
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
        }
        PrintPlaceValue();

        double tenthPower = Mathf.Floor(Mathf.Log10((float)moneyCount));
        if (tenthPower >= 6)
        {
            placeValueText.text = placeLogValues[(int)tenthPower];
        }

        logVal = (int)tenthPower;
    }

    public void AddMoney(double mps)
    {
        moneyCount += mps * Time.deltaTime;
        moneyCounter.text = moneyCount.ToString("F3") + "dollars";
    }


    public void MoneyClick()
    {
        moneyCount++;
        moneyCounter.text = moneyCount.ToString("F3") + "dollars";
    }

    public void PrintPlaceValue()
    {
        double tenthPower = Mathf.Floor(Mathf.Log10((float)moneyCount)); // round down to prevent something like 99 being considers 100th place
        double place = Mathf.Pow(10, (float)tenthPower);
        placeValueOfMoney = place;

        numFormatted = moneyCount / place;
    }

    // when number gets to million and above then you should to format based on value

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
