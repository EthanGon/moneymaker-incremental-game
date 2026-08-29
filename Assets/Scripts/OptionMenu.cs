using TMPro;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private Animator autoSaveTextAnimation;
    [SerializeField] private TextMeshProUGUI autoSaveText;
    [SerializeField] private GameObject optionMenu;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            optionMenu.SetActive(true);
        }
    }


    public void Save()
    {
        autoSaveText.text = "Game Saved";
        autoSaveTextAnimation.SetTrigger("fade");

        SaveSystem.SavePlayer();
    }

    public void Quit()
    {
        SaveSystem.SavePlayer();
        Application.Quit();
    }

    public void CloseMenu()
    {
        optionMenu.SetActive(false);
    }

    public void ToggleMenu()
    {
        optionMenu.SetActive(!optionMenu.activeInHierarchy);
    }


}
