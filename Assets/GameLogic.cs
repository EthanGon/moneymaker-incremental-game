using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance;
    public double moneyCount;
    public TextMeshProUGUI moneyCounter;
    public double moneyPerSec;
    public double placeValueOfMoney;

    private void Awake()
    {
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
    }

    public void AddMoney(double mps)
    {
        moneyCount += mps * Time.deltaTime;
        moneyCounter.text = "MONEY\n" + moneyCount.ToString("F0");
    }


    public void MoneyClick()
    {
        moneyCount++;
        moneyCounter.text = "MONEY\n" + moneyCount.ToString("F3");
    }

    public void PrintPlaceValue()
    {
        double tenthPower = Mathf.Floor(Mathf.Log10((float)moneyCount));
        double place = Mathf.Pow(10, (float)tenthPower);
        placeValueOfMoney = place;
    }



}
