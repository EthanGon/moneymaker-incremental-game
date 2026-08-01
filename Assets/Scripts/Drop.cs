using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool hasPowerUp;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked Drop");
        if (hasPowerUp)
        {
            BuffManager.Instance().GivePlayerPowerUp();
        }
        Destroy(gameObject);
    }

    public void GivePowerUp()
    {
        hasPowerUp = true;
    }
    
    
}
