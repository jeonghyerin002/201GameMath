using UnityEngine;

public class SystemRandomSeed : MonoBehaviour
{

    void Start()
    {
        System.Random rnd = new System.Random(1234);
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(rnd.Next(1, 7)); // 1~6 사이의 정수
        }
    }

}
