using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FontManager : MonoBehaviour
{
    public TMP_FontAsset fontAsset;
    
    // Update is called once per frame
    void Update()
    {
        foreach (var t in FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None))
        {
            t.font = fontAsset;
        } 
    }
}
