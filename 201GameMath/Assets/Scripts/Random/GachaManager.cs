using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;

public class GachaManager : MonoBehaviour
{
    public Button oneTimeGachaButton;
    public Button tenTimeGachaBuytton;

    public TextMeshProUGUI resultText;

    void Update()
    {
        oneTimeGachaButton.onClick.AddListener(SimulateGachaSingle);
        tenTimeGachaBuytton.onClick.AddListener(SimulateGachaTenTime);
    }
    public void SimulateGachaSingle()
    {
        Debug.Log("Gacha Result : " + Simulate());
        resultText.text = "Gacha Result : " + Simulate();
    }
    public void SimulateGachaTenTime()
    {
        List<string> results = new List<string>();
        for (int i = 0; i < 9; i++)
        {
            results.Add(Simulate());
        }

        //10번째는 A 등급 이상으로 설정, 확률은 A : S를 2:1로 설정
        float r2 = Random.value;
        string result2 = string.Empty;
        if (r2 < 2f / 3f) result2 = "A";
        else result2 = "S";
        results.Add(result2);

        resultText.text = "Gacha Result : " + Simulate();
    }
    string Simulate()
    {
        float r = Random.value;
        string result = string.Empty;

        if (r < 0.4f) result = "C";
        else if (r < 0.7f) result = "B";
        else if (r < 0.9f) result = "A";
        else result = "S";

        return result;
    }
}
