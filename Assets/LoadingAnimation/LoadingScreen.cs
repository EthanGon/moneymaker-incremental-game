using System;
using TMPro;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private static LoadingScreen instance;
    [SerializeField] private int ticks;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private float textSwitchDelay;
    [SerializeField] private float timer;
    [SerializeField] private float timeOnScreenTimer;
    [SerializeField] private float loadTime;


    private void Start()
    {
        instance = this;
        gameObject.SetActive(true);
        loadingText.text = "LOADING";
    }


    // Update is called once per frame
    void Update()
    {
        if (timeOnScreenTimer < loadTime)
        {
            timeOnScreenTimer += Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }

        if (timer < textSwitchDelay)
        {
            timer += Time.deltaTime;
        }
        else
        {
            
            if (loadingText.text.Equals("LOADING..."))
            {
                loadingText.text = "LOADING";
            } 
            else
            {
                loadingText.text = loadingText.text + ".";
            }

            timer = 0;
            
        }
    }

    public static LoadingScreen GetInstance()
    {
        return instance;
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
    }
}
