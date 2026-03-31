using UnityEngine;

public class UnityRandomSeed : MonoBehaviour
{
    //통제된 확률을 사용해야할 때 
    //랜덤으로 구현한 시스템이지만 튜토리얼 같은 경우 확률을 통제해주면 좋으니까 사용하는걸 추천
    void Start()
    {
        Random.InitState(1234); // Unity 난수 시드 고정
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(Random.Range(1, 7)); // 1~6 사이의 난수
        }
    }

}
