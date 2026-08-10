using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool hasPowerUp;
    private bool moneyRainDrop;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked Drop");
        if (hasPowerUp)
        {
            BuffManager.Instance().GivePlayerPowerUp();
        }

        if (moneyRainDrop)
        {
            // GIVE SOME FIXED AMOUNT
            GameLogic GL = GameLogic.Instance();
            double minReward = GL.moneyPerMin/2;
            double maxReward = GL.moneyPerMin;

            double reward = Random.Range((float) minReward, (float) maxReward);

            GL.moneyCount += reward;
        }

        if (!moneyRainDrop && !hasPowerUp)
        {
            GameLogic GL = GameLogic.Instance();
            
            if (GL.moneyPerSec == 0f)
            {
                GL.moneyPerSec += 100;
            }
            else
            {
                GL.moneyCount += GL.moneyPerMin * 5;
            }
        }

        Destroy(gameObject);
    }

    public void GivePowerUp()
    {
        hasPowerUp = true;
    }

    public void SetAsMoneyRainDrop()
    {
        moneyRainDrop = true;
    }
    
    
}
