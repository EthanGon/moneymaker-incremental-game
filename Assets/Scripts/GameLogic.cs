using System;
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
    public TextMeshProUGUI mps;
    public TextMeshProUGUI mpm;
    public double moneyCount;
    public double moneyPerSec;
    public double moneyPerMin;
    public int levelTokens;

    [Header("Values")]
    public string[] placeValues;
    public string numFormatted;
    public double placeValueOfMoney;
    private double logVal;

    private float delayTimer;
    [SerializeField] private float delayTime;
    [SerializeField] private string[] units;
    [SerializeField] private string[] tens;
    [SerializeField] private string[] hundreds;
    [SerializeField] private List<string> names; 

    private void Awake()
    {
        instance = this;
        placeLogValues = new Dictionary<int, string>();
        InitPlaceValues();
        
        
    }


    private void Update()
    {
        moneyPerSec = BuildingManager.instance.GetTotalMPS();
        moneyPerMin = moneyPerSec * 60.0;
        mps.text = "per second: " + FormatNumber(moneyPerSec)[0] + " " + FormatNumber(moneyPerSec)[1];
        mpm.text = "per minute: " + FormatNumber(moneyPerMin)[0] + " " + FormatNumber(moneyPerMin)[1];


        if (moneyCount < double.MaxValue)
        {
            AddMoney(moneyPerSec);
            DisplayMoneyCount();

            double tenthPower = Math.Floor(Math.Log10((moneyCount))); // Getting Log10 of 0 results in -Infinity (note to self)
            double tempLogVal = logVal;
            logVal = (int)tenthPower;

        }

        

    }

    public double GetMoneyAmount()
    {
        return moneyCount;
    }

    public void AddMoney(double mps)
    {
        moneyCount += mps * Time.deltaTime;
        TokenManager.Instance().currentXP += mps * Time.deltaTime; 
        DisplayMoneyCount();
    }


    public void MoneyClick()
    {
        moneyCount++;
        TokenManager.Instance().currentXP++;
        DisplayMoneyCount();
    }

    // how much to move the decimal left based on where the first comma is
    public int MoveDecimal(double number)
    {
        string numToString = number.ToString("N");

        if (numToString[3] == ',')
        {
            return 2;
        }

        if (numToString[2] == ',')
        {
            return 1;
        }

        return 0;
    }

    public string[] FormatNumber(double moneyToFormat)
    {
        // round down to prevent something like 99 being considers 100th place
        double tenthPower = Math.Floor(Math.Log10((moneyToFormat)));
        double place = Math.Pow(10, tenthPower);
        double formatted;

        // index0 = number formatted, index1 = name of place value
        string[] result = new string[2];
        

        if (tenthPower >= 307)
        {
            result[0] = "A LOT OF FREAKING\n" + "dollars";
        }
        else if ((int)tenthPower >= 6) // moneyCount >= 1,000,000
        {
            formatted = (moneyToFormat / place);
              
            // How much to move decimal left based on placeCount
            for (int i = 0; i < MoveDecimal(moneyToFormat); i++)
            {
                formatted *= 10;
            }

            try
            {
                result[0] = formatted.ToString("F3");
                result[1] = placeLogValues[(int)tenthPower].ToLower();
            }
            catch (KeyNotFoundException e)
            {
                result[0] = formatted.ToString("F3");
                result[1] = placeLogValues[(int)308].ToLower();
            }

        }
        else // moneyCount < 1,000,000
        {
            double mon = moneyToFormat;

            // fixes the mps rounding up if I specify if the argument passed is the mps
            if (moneyToFormat == moneyPerSec)
            {
                if (moneyToFormat == 0)
                {
                    result[0] = mon.ToString("F0");
                }
                else
                {

                    // removes the decimal point if num is whole b/c 1.0, 2.0, etc looks ugly
                    if (moneyToFormat % 1 != 0)
                    {
                        result[0] = mon.ToString("N1");
                    }
                    else
                    {
                        result[0] = mon.ToString("N0");
                    }
                }
                    
            }
            else
            {
                result[0] = mon.ToString("N0");
            }
  
        }

        return result;
    }

    public void DisplayMoneyCount()
    {
        string result = "";

        if (moneyCount >= 1000000)
        {
            result = FormatNumber(moneyCount)[0] + "\n" + FormatNumber(moneyCount)[1] + " ";
        }
        else
        {
            result = FormatNumber(moneyCount)[0] + "\n";
            
        }
        
        numFormatted = result;
        
        // Delay for display
        if (delayTimer < delayTime)
        {
            delayTimer += Time.deltaTime;
        } 
        else
        {
            moneyCounter.text = result + "dollars";
            delayTimer = 0;
        }


    }


    // Need to improve this at some point, it does what I want but could be wayyy better I'm sure
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

        
    }

    public void LogDictionary()
    {
        int max = 0;
        int maxKey = 0;

        foreach (KeyValuePair<int, string> kvp in placeLogValues)
        {
            Debug.Log("10^ " + kvp.Key + " : " + kvp.Value);
            if (kvp.Value.Length > max)
            {
                maxKey = kvp.Key;
            }
        }

        //Debug.Log("Key value with longest string is: " + maxKey + " with value " + placeLogValues[maxKey]);
    }

    public static GameLogic Instance()
    {
        return instance;
    }

}
