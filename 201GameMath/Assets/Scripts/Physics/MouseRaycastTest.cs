using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MouseRaycastTest : MonoBehaviour
{
    public float forcePower = 10f;
    public float rayDistance = 100f;

    public GameObject player1;
    public GameObject player2;
    public GameObject ball1;
    public GameObject ball2;

    private Rigidbody p1Rb;
    private Rigidbody p2Rb;
    private Rigidbody b1Rb;
    private Rigidbody b2Rb;

    private bool isMoving = false;

    GameObject turn;

    [SerializeField] private float speed;
    void Start()
    {
        turn = player1;

        if (player1 != null) p1Rb = player1.GetComponent<Rigidbody>();
        if (player2 != null) p2Rb = player2.GetComponent<Rigidbody>();
        if (ball1 != null) b1Rb = ball1.GetComponent<Rigidbody>();
        if (ball2 != null) b2Rb = ball2.GetComponent<Rigidbody>();
    }
    void Update()
    {

        if (isMoving)
        {
            if (AreAllBallsStopped())
            {
                SwitchTurn();
                isMoving = false; //큰일 공 회전이 안멈춤
            }
        }
    }
    public void OnClick(InputValue value)
    {

        if (!value.isPressed || isMoving)
            return;
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            Rigidbody rb = hit.collider.attachedRigidbody;

            if (rb != null)
            {
                if (hit.collider.gameObject != turn)
                    return;
                
                Vector3 forceDirection = rb.transform.position - hit.point;
                forceDirection.y = 0; //튀어오르기 방지
                forceDirection.Normalize();
                rb.AddForce(forceDirection * forcePower, ForceMode.Impulse);
                speed = rb.linearVelocity.magnitude;

                isMoving = true;
            }
        }
    }
    private bool AreAllBallsStopped()
    {

        float stopThreshold = 0.01f;

        bool p1Stopped = p1Rb == null || p1Rb.linearVelocity.magnitude <= stopThreshold;
        bool p2Stopped = p2Rb == null || p2Rb.linearVelocity.magnitude <= stopThreshold;
        bool b1Stopped = b1Rb == null || b1Rb.linearVelocity.magnitude <= stopThreshold;
        bool b2Stopped = b2Rb == null || b2Rb.linearVelocity.magnitude <= stopThreshold;

        return p1Stopped && p2Stopped && b1Stopped && b2Stopped;
    }
    private void SwitchTurn()
    {
        if (turn == player1)
        {
            turn = player2;
        }
        else
        {
            turn = player1;
        }
    }

}
