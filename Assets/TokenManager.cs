using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TokenManager : MonoBehaviour
{
    [SerializeField] private int tokenCount;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI tokenCountText;
    private static TokenManager instance;
    public double currentXP;
    public double neededXP;
    public Slider xpBar;



    private void Awake()
    {
        instance = this;
        tokenCountText.text = tokenCount.ToString();
        this.xpBar = GetComponent<Slider>();
    }

    public static TokenManager Instance()
    {
        return instance;
    }

    // Update is called once per frame
    void Update()
    {
        
        DisplayXP();
        if (currentXP >= neededXP)
        {
            tokenCount++;
            tokenCountText.text = tokenCount.ToString();
            currentXP = 0;
            neededXP = neededXP * 2;
        }

        xpBar.maxValue = (float) neededXP;
        xpBar.value = (float)currentXP;
    }


    // index0 is the number formatted, index1 is the name of the place value
    public void DisplayXP()
    {
        string currentXPText = GameLogic.Instance().FormatNumber(currentXP)[0] + " " + GameLogic.Instance().FormatNumber(currentXP)[1];
        string XPneededText  = GameLogic.Instance().FormatNumber(neededXP)[0]  + " " + GameLogic.Instance().FormatNumber(neededXP)[1];

        xpText.text = currentXPText + " / " + XPneededText;

        ////xpText.text = currentXP.ToString("N0") + "/" + neededXP.ToString("N0");
        //xpText.text = GameLogic.Instance().FormatNumber(currentXP)[0] + " " + GameLogic.Instance().FormatNumber(currentXP)[1] + " / " 
        //    + GameLogic.Instance().FormatNumber(neededXP)[0] + " " + GameLogic.Instance().FormatNumber(neededXP)[1];
    }

    public void DecreaseTokenCount()
    {
        tokenCount--;
        tokenCountText.text = tokenCount.ToString();
    }

    public int GetTokenCount()
    {
        return tokenCount;
    }

    public void SetTokenCount(int count)
    {
        tokenCount = count;
        tokenCountText.text = tokenCount.ToString();
    }
}
