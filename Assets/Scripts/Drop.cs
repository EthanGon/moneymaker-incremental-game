using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool hasPowerUp;
    [SerializeField] private float timeBeforeFade;
    private float timer;
    private bool moneyRainDrop;
    private Animator anim;
    private bool fadeTriggered;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleLifeSpan();
    }

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

    private void HandleLifeSpan()
    {
        if (fadeTriggered)
        {
            return;
        }

        if (timer < timeBeforeFade)
        {
            timer += Time.deltaTime;
        }
        else
        {
            anim.SetTrigger("fade");
            fadeTriggered = true;
        }

        
    }

    public void DeleteDrop()
    {
        Destroy(gameObject);
    }
    
    
}
