using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class CriticalManager : MonoBehaviour
{
    public int totalHits = 0;
    public int critHits = 0;
    public float targetRate = 0.1f; //10% 목표 확률

    public TextMeshProUGUI systemText;
    public TextMeshProUGUI resultText;

    public Button hitButton;

    void Update()
    {
        hitButton.onClick.AddListener(SimulateCritical);
    }
    public bool RollCrit()
    {
        totalHits++;
        float currentRate = 0f;
        if (critHits > 0)
            currentRate = (float)critHits / totalHits;
        if (currentRate < targetRate && (float)(critHits + 1) / totalHits <= targetRate)
        {
            systemText.text = "Critical Hit!, (Forced)";
            critHits++;
            return true; //치명타가 발생한 이후에도 현재 비율이 여전히 낮다면 무조건 발생시킴.
        }
        if (currentRate > targetRate && (float)critHits / totalHits >=  targetRate)
        {
            systemText.text = "Normal Hit! (Forced)";
            return false;
        }
        if (Random.value < targetRate)
        {
            systemText.text = "Critical Hit!, Base";
            critHits++;
            return true; //기본 확률 처리
        }
        systemText.text = "Normal Hit!, Base";
        return false;
    }
    public void SimulateCritical()
    {
        RollCrit();
        resultText.text = "Total Hits : " + totalHits;
        resultText.text = "Critical Hits : " + critHits;
        resultText.text = "Current Critical Rate : " + (float)critHits / totalHits;
    }  
}
