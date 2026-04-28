using UnityEngine;

public class LerpMover : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;

    [SerializeField] private float duration = 2f;
    [SerializeField] private float t = 0f;
    
    void Update()
    {
        if (t < 1f)
        {
            t = Mathf.PingPong(Time.time / duration, 1f); //PIngPong = 정해진 두 값을 왔다 갔다.
            //t += Time.deltaTime / duration;

            //Vector3 a = startPos.position;
            //Vector3 b = endPos.position;
            //Vector3 p = (1f - t) * a + t * b;
            //transform.position = p; //Lerp 구현

            //transform.position = Vector3.Lerp(startPos.position , endPos.position, t);

            transform.position = Vector3.LerpUnclamped(startPos.position, endPos.position, t); //Lerp 대신 LerpUnclamped 사용 가능


        }
    }
}
