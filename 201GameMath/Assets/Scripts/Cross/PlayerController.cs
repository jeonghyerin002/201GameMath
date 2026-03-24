using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public float moveSpeed = 5.0f;

    private Vector2 moveInput;

    public bool isLeftParrying = false;
    public bool isRightParrying = false;

    public Transform target;
    public float parryingDistance = 20.0f;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLeftParrying(InputValue value)
    {
        isLeftParrying = value.isPressed;
    }
    public void OnRightParrying(InputValue value)
    {
        isRightParrying = value.isPressed;
    }
    void Update()
    {
        PlayerRotate();
        EnemyCheck();
    }
    void PlayerRotate() //플레이어 회전 움직임
    {
        float rotation = moveInput.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotation, 0f);

        Vector3 moveDir = moveInput.y * moveSpeed * Time.deltaTime * transform.forward;
        transform.Translate(moveDir);
    }

    void EnemyCheck()
    {
        Vector3 forward = transform.forward;
        Vector3 dirToTarget = (target.position - transform.forward).normalized;

        Vector3 crossProduct = Vector3.Cross(forward, dirToTarget);

        if (crossProduct.y > 0.1f)
        {
            //Debug.Log("왼쪽 적 발견");
            //Enemy 발견까지는 했어. 발견은 해 발견은 했는데 이제 발견한 애가 Enemy다 라는걸 어떻게 하지 tag??????
        }
        else if (crossProduct.y < 0.1f)
        {
            //Debug.Log("오른쪽 적 발견");
            //Enemy 발견
        }
    }
    public void PlayerParrying(InputValue value)
    {

    }
}
