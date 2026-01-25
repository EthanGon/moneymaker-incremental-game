using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FontManager : MonoBehaviour
{
    public TMP_FontAsset fontAsset;
    public List<TextMeshProUGUI> textGUIs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var t in FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None))
        {
            textGUIs.Add(t);
        }
        Debug.Log(textGUIs.Count);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < textGUIs.Count; i++)
        {
            textGUIs[i].font = fontAsset;
        }
    }
}
