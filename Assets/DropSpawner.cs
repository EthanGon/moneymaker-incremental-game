using Unity.VisualScripting;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    public GameObject drop;

    [Header("Timer")]
    public float timer;
    public float cd;
    
    
    [Header("Drop Chances")]
    public double powerUpDropChance;
    public double dropChance;

    [Header("Money Rain Handler")]
    [SerializeField] private float moneyRainDelay;
    [SerializeField] private float moneyRainDropTimer;

    // Update is called once per frame
    void Update()
    {
        if (timer < cd)
        {
            timer += Time.deltaTime;
        }
        else
        {
            float dropChanceRoll = Random.Range(0f, 1f);

            if (dropChanceRoll <= dropChance / 100)
            {
                SpawnDrop();
            }

            timer = 0;
        }

        HandleMoneyRain();

        


    }

    private void HandleMoneyRain()
    {
        if (BuffManager.Instance().IsBuffActive("MoneyRain"))
        {
            if (moneyRainDropTimer < moneyRainDelay)
            {
                moneyRainDropTimer += Time.deltaTime;
            }
            else
            {
                MoneyRainDrop();
                moneyRainDropTimer = 0;
            }
        }
        else
        {
            moneyRainDropTimer = 0;
        }
    }



    public void SpawnDrop()
    {
        float randX = Random.Range(-850, 850);
        float randY = Random.Range(-450, 450);
        //Debug.Log("Spawned Drop Spawned at " + "(" + randX + "," + randY + ")");

        GameObject newDrop = Instantiate(drop, Vector3.zero, Quaternion.identity);

        float powerUpChanceRoll = Random.Range(0f, 1f);
        if (powerUpChanceRoll <= powerUpDropChance / 100)
        {
            newDrop.GetComponent<Drop>().GivePowerUp();
        }

        newDrop.transform.SetParent(transform, false);
        newDrop.transform.localPosition = new Vector3(randX, randY, 0);
    }

    private void MoneyRainDrop()
    {
        float randX = Random.Range(-850, 850);
        float randY = Random.Range(-450, 450);
        //Debug.Log("Spawned Drop Spawned at " + "(" + randX + "," + randY + ")");

        GameObject newDrop = Instantiate(drop, Vector3.zero, Quaternion.identity);
        newDrop.GetComponent<Drop>().SetAsMoneyRainDrop();

        newDrop.transform.SetParent(transform, false);
        newDrop.transform.localPosition = new Vector3(randX, randY, 0);

    }
}
