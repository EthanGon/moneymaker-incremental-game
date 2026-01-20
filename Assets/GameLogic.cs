using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    public string[] tens;
    public string[] hundreds;
    public List<string> names;
 
    private void Awake()
    {
        placeLogValues = new Dictionary<int, string>();
        InitPlaceValues();
        
        instance = this;
        moneyCounter.text = "MONEY\n" + moneyCount.ToString("F3");

        //Mathf.Floor(Mathf.Log10((float)moneyCount));
        Debug.Log(double.PositiveInfinity);
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

        if (moneyCount >= double.MaxValue && logVal < 0 )
        {
            moneyCount = double.PositiveInfinity;
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

            
            try
            {
                result = numFormatted.ToString("F3") + "\n" + placeLogValues[(int)tenthPower].ToLower() + " dollars";
            } 
            catch (KeyNotFoundException e)
            {
                Debug.LogError(e.Message);
            }
            
            
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

        for (int i = 0;i < tens.Length;i++)
        {
            names.Add(tens[i] + "llion");
            for (int j = 0; j < units.Length; j++)
            {
                

                if (j == 2 && (i == 1 || i == 2 || i == 3 || i == 4)) // Tre
                {
                    names.Add(units[j] + "s" + tens[i] + "llion");
                }
                else if (j == 5 && ((i == 1 || i == 2 || i == 3 || i == 4 || i == 6))) // Se
                {
                    if (i == 6) 
                    {
                        names.Add(units[j] + "x" + tens[i] + "llion");
                    } 
                    else
                    {
                        names.Add(units[j] + "s" + tens[i] + "llion");
                    }
                }
                else if (j == 6 && (i != 8)) // Septe
                {
                    if ((i == 0 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6)) {
                        names.Add(units[j] + "n" + tens[i] + "llion");
                    } 
                    else
                    {
                        names.Add(units[j] + "m" + tens[i] + "llion");
                    }
                }
                else if (j == 8 && (i != 8)) // Nove
                {
                    if ((i == 0 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6))
                    {
                        names.Add(units[j] + "n" + tens[i] + "llion");
                    }
                    else
                    {
                        names.Add(units[j] + "m" + tens[i] + "llion");
                    }
                }
                else
                {
                    names.Add(units[j] + tens[i] + "llion");
                }
            }

        }

        for (int i = 0; i < hundreds.Length; i++)
        {
            names.Add(hundreds[i] + "llion");
            for (int j = 0; j < units.Length; j++)
            {

                if (j == 2 && (i == 1 || i == 2 || i == 3 || i == 4)) // Tre
                {
                    names.Add(units[j] + "s" + hundreds[i] + "llion");
                }
                else if (j == 5 && ((i == 0 || i == 2 || i == 3 || i == 4 || i == 7))) // Se
                {
                    if (i == 0 || i == 7)
                    {
                        names.Add(units[j] + "x" + hundreds[i] + "llion");
                    }
                    else
                    {
                        names.Add(units[j] + "s" + hundreds[i] + "llion");
                    }
                }
                else if (j == 6 && (i != 8)) // Septe
                {
                    if ((i == 7))
                    {
                        names.Add(units[j] + "m" + hundreds[i] + "llion");
                    }
                    else
                    {
                        names.Add(units[j] + "n" + hundreds[i] + "llion");
                    }
                }
                else if (j == 8 && (i != 8)) // Nove
                {
                    if ((i == 7))
                    {
                        names.Add(units[j] + "m" + hundreds[i] + "llion");
                    }
                    else
                    {
                        names.Add(units[j] + "n" + hundreds[i] + "llion");
                    }
                }
                else
                {
                    names.Add(units[j] + hundreds[i] + "llion");
                }
            }


        }

        for (int i = 0; i < names.Count; i++)
        {
            if (num <= 0)
            {
                break;
            }
            placeLogValues.Add(num, names[i]);

            // the next two are still within the same place 
            for (int j = 1; j <= 2; j++)
            {
                placeLogValues.Add(num + j, names[i]);
            }

            num += 3;
        }

        LogDictionary();
    }

    public void LogDictionary()
    {
        foreach (KeyValuePair<int, string> kvp in placeLogValues)
        {
            Debug.Log("10^ " + kvp.Key + " : " + kvp.Value);
        }
    }

    public static GameLogic Instance()
    {
        return instance;
    }

}
