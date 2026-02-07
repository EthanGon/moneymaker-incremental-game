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
        xpText.text = currentXP.ToString("N0") + "/" + neededXP.ToString("N0");
        xpText.text = GameLogic.Instance().FormatNumber(currentXP)[0] + " " + GameLogic.Instance().FormatNumber(currentXP)[1] + " / " 
            + GameLogic.Instance().FormatNumber(neededXP)[0] + " " + GameLogic.Instance().FormatNumber(neededXP)[1];
        
        if (currentXP >= neededXP)
        {
            tokenCount++;
            tokenCountText.text = tokenCount.ToString();
            currentXP = 0;
            neededXP += (neededXP * .50f);
        }

        xpBar.maxValue = (float) neededXP;
        xpBar.value = (float)currentXP;
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
