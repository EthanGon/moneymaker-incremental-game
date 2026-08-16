using UnityEngine;

public class SteamIntegration : MonoBehaviour
{
    private void Start()
    {

        
        try
        {
            Steamworks.SteamClient.Init(4413330);
            PrintSteamName();
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex);
        }
        

    }

    private void PrintSteamName()
    {
        Debug.Log(Steamworks.SteamClient.Name);
    }

    public void Update()
    {
        Steamworks.SteamClient.RunCallbacks();
    }

    private void OnApplicationQuit()
    {
        Steamworks.SteamClient.Shutdown();
    }

    private void WipeAchievements(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var ach = new Steamworks.Data.Achievement("ACH_" + i);
            ach.Clear();
        }
    }
   

}
