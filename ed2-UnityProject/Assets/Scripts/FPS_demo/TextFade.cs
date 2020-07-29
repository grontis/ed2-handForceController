using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    private TextMeshProUGUI missionText;
    
    // Start is called before the first frame update
    void Start()
    {
        missionText = GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(FadeText());
    }

    IEnumerator FadeText()
    {
        yield return new WaitForSeconds(10);
        missionText.enabled = false;
    }

}
