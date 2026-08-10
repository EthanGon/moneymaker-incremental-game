using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffStatus : MonoBehaviour
{
    public Slider slider;
    public Buff assignedBuff;
    public TextMeshProUGUI textMeshProUGUI;
    public BuffState state;

    void Update()
    {
        if (assignedBuff != null)
        {
            if (slider.value > 0)
            {
                slider.value -= Time.deltaTime;
                assignedBuff.buffCurrDurection = slider.value;
            } 
            else
            {
                assignedBuff.isActive = false;
                BuffManager.Instance().RemoveFromActiveBuff(assignedBuff);
                this.assignedBuff = null;
                this.gameObject.SetActive(false);

            }
        }
    }

    public void AddBuff(Buff buff)
    {
        this.gameObject.SetActive(true);
        textMeshProUGUI.text = buff.name;
        this.assignedBuff = buff;
        assignedBuff.buffCurrDurection = buff.buffMaxDuration;
        SetMaxDuration(buff.buffMaxDuration);
        SetDuration(buff.buffMaxDuration);
    }

    public void AddSavedBuff(Buff buff, float savedDuration)
    {
        this.gameObject.SetActive(true);
        textMeshProUGUI.text = buff.name;
        this.assignedBuff = buff;
        assignedBuff.buffCurrDurection = savedDuration;
        SetMaxDuration(buff.buffMaxDuration);
        
    }

    public void SetMaxDuration(float maxDuration)
    {
        this.slider.maxValue = maxDuration;
    }

    public void SetDuration(float duration)
    {
        this.slider.value = duration;
    }

    public void ResetDuration()
    {
        assignedBuff.ResetDurr();
        slider.value = assignedBuff.buffMaxDuration;
        slider.maxValue = assignedBuff.buffMaxDuration;
        
    }

    public void UpdateUI(Buff buff)
    {
        
        textMeshProUGUI.text = buff.name;
        this.assignedBuff = buff;
        SetDuration(buff.buffCurrDurection);
    }

}
