using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance;
    public double moneyCount;
    public TextMeshProUGUI moneyCounter;

    private void Awake()
    {
        instance = this;
        moneyCounter.text = "MONEY\n" + moneyCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
