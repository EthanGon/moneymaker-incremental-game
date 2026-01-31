using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    public GameObject drop;
    public float timer;
    public float cd;
    public int numGot;
    public double chance;
    public int range;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        chance = 1.0f / range;
        if (timer < cd)
        {
            timer += Time.deltaTime;
        }
        else
        {
            int coin = Random.Range(0, range);
            
            numGot = coin;

            if (coin == 1)
            {
                SpawnDrop();
            }
            
            timer= 0;
        }
    }



    public void SpawnDrop()
    {
        float randX = Random.Range(-850, 850);
        float randY = Random.Range(-450, 450);
        Debug.Log("Spawned Drop Spawned at " + "(" + randX + "," + randY + ")");

        GameObject newDrop = Instantiate(drop, Vector3.zero, Quaternion.identity);
        newDrop.transform.SetParent(transform, false);
        newDrop.transform.localPosition = new Vector3(randX, randY, 0);
    }
}
