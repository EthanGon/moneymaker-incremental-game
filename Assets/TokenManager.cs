using TMPro;
using UnityEngine;

public class TokenManager : MonoBehaviour
{
    [SerializeField] private int tokenCount;
    [SerializeField] private TextMeshProUGUI xpText;
    private static TokenManager instance;
    public double currentXP;
    public double neededXP;



    private void Awake()
    {
        instance = this;
    }

    public static TokenManager Instance()
    {
        return instance;
    }

    // Update is called once per frame
    void Update()
    {
        xpText.text = currentXP.ToString("N0") + "/" + neededXP.ToString("N0");
        
        if (currentXP >= neededXP)
        {
            tokenCount++;
            neededXP = neededXP * 2;
        }
    }

    public int GetTokenCount()
    {
        return tokenCount;
    }
}
