using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;

    public float rotationSpeed = 50.0f;
    public float detectionRange = 8f;
    public float dashSpeed = 15f;
    public float stopDistance = 1.2f;
    public float viewAngle = 60f; //시야각

    public bool isDashing = false;

    private Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        float distance = (player.position - transform.position).magnitude;

        //내적으로 사용하여 전방 시야 60도 이내 판정
        Vector3 toPlayer = (player.position - transform.position).normalized;
        Vector3 forward = transform.forward;

        if (!isDashing)
        {
            float DotProduct(Vector3 forward, Vector3 toPlayer)
            {
                return forward.x * toPlayer.x + forward.y * toPlayer.y + forward.z * toPlayer.z;
            }
            float angle = Mathf.Acos(DotProduct(forward, toPlayer)) * Mathf.Rad2Deg; //내적을 각도로 변환

            if (angle < viewAngle / 2 && distance < 5f)
            {
                Debug.Log("플레이어 발견");
                isDashing = true;
            }
            // if (발견했다면)
            // isDashing = true;
        }
        else
        {
            //Dashing 일 땐 플레이어 쪽으로 가기 (거리 판단해서 플레이어랑 가까우면 CheckParry 수행
            if (isDashing)
            {
                transform.position = toPlayer;
                if (distance < 0.1f)
                {
                    CheckParry();
                }
            }
        }
    }
    void CheckParry()
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        //외적을 사용하여 플레이어 기준 왼쪽/오른쪽 패링


    }
}
