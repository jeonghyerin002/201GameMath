using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEngine.ParticleSystem;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DiceSimulator : MonoBehaviour
{
    int[] counts = new int[6];

    public int trials = 100;

    public TextMeshProUGUI text1;
    public TextMeshProUGUI[] labels = new TextMeshProUGUI[6];

    public Button button;

    void Start()
    {

        
        //text1.text = "안녕하세요!";
    }
    void Update()
    {
        button.onClick.AddListener(ButtomClick);
    }

    public void ButtomClick()
    {
        Debug.Log("버튼 클릭됨!");

        for (int i = 0; i < trials; i++)
        {
            int result = Random.Range(1, 7);
            counts[result - 1]++;
        }
        for (int i = 0; i < counts.Length; i++)
        {
            float persent = (float)counts[i] / trials * 100f;
            string result = ($"{i + 1} : {counts[i]}회, ({persent:F2}%)");
            labels[i].text = result;
        }
    }
}
