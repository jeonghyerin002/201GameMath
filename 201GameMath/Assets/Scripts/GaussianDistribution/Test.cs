using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int sampleCount;

    public int min;
    public int max;

    public void StandardDeviation()   //샘플 수, 범위(min, max)를 inspector에서 수정할 수 있게.
    {
        int n = sampleCount; //샘플 수
        float[] samples = new float[n];
        for (int i = 0; i < n; i++)
        {
            samples[i] = Random.Range(min, max);
        }

        float mean = samples.Average();
        float sumOfSquares = samples.Sum(x => Mathf.Pow(x - mean, 2));
        float stdDev = Mathf.Sqrt(sumOfSquares / n);

        Debug.Log($"평균 : {mean},평균 편차 : {stdDev}");
    }
    public float GenerateGaussian(float mean, float stdDev)   //버튼 누를 때 마다 로그로 값이 찍히게.
    {
        float u1 = 1.0f - Random.value; //0보다 큰 난수
        float u2 = 1.0f - Random.value; //0보다 큰 난수

        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);

        return mean + stdDev * randStdNormal;
    }

    public void ButtonClicked()
    {
        Debug.Log(GenerateGaussian(100f, 10f));
    }
}
