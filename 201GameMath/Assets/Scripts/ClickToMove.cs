using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 mouseScreenPosition;
    private Vector3 targetPosition;
    bool isMoving = false;

    public void OnPoint(InputValue value)
    {
        mouseScreenPosition = value.Get<Vector2>();
    }

    public void OnClick(InputValue value)
    {
        if(value.isPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject != gameObject)
                {
                    targetPosition = hit.point;
                    targetPosition.y = transform.position.y;
                    isMoving = true;

                    break;
                }
            }
        }
    }

    void Update()
    {
        if (isMoving)
        {
            Vector3 playerPos = transform.position;
            Vector3 direction = new Vector3 (targetPosition.x - transform.position.x, 0 , targetPosition.z - transform.position.z);

            float sqrMagnitude = direction.x * direction.x + direction.y * direction.y + direction.z * direction.z;
            float magnitude = Mathf.Sqrt(sqrMagnitude);

            direction = direction / magnitude;
            transform.position += direction * moveSpeed * Time.deltaTime;


            if(magnitude < 0.1f)
                isMoving = false;
        }
    }
}
