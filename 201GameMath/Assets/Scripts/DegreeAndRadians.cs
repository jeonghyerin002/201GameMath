using UnityEngine;

public class DegreeAndRadians : MonoBehaviour
{

    void Start()
    {
        //각도, 라디안 이해
        float degrees = 45f;
        float radians = degrees * Mathf.Deg2Rad;
        Debug.Log("45도 -> 라디안 :" + radians);

        float radianValue = Mathf.PI / 3;
        float degreeValue = radianValue * Mathf.Rad2Deg;
        Debug.Log("파이 / 3 라디안 -> 도 변환:" + degreeValue);
    }
    void Update()
    {
        //삼각함수 기초 / 캐릭터를 특정 각도로 이동시키기
        float speed = 5f;
        float angle = 30f;
        float radians = angle * Mathf.Deg2Rad;

        Vector3 direction = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));
        transform.position += direction * speed * Time.deltaTime;
    }
}
