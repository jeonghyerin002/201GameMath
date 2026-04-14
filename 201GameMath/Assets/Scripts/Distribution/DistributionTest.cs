using UnityEngine;

public class DistributionTest : MonoBehaviour
{
    int PoissonDistribution(float lamda)
    {
        int k = 0;
        float p = 1f;
        float l = Mathf.Exp(-lamda);
        while (p < l)
        {
            k++;
            p *= Random.value;
        }
        return k - 1;
    }
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            int count = PoissonDistribution(3f);
            Debug.Log($"Minute {i + 1}: {count} events");
        }
    }
}
