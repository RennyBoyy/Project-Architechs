using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float returnSpeed = 2f;
    [SerializeField] private float lerpIncrement = 0.1f;

    private bool isMouseOver = false;
    private Rigidbody rb;
    private Vector3 lastMousePosition;
    private Quaternion initialRotation;
    private float lerpFactor = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularDrag = 10;
        initialRotation = rb.rotation; // Armazena a rotação inicial
    }

    void FixedUpdate()
    {
        if (isMouseOver)
        {
            lerpFactor = Mathf.Min(lerpFactor + lerpIncrement * Time.fixedDeltaTime, 1f);
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - lastMousePosition;
            lastMousePosition = currentMousePosition;

            RotateTowardsMouse(mouseDelta);
        }
        else
        {
            lerpFactor = Mathf.Max(lerpFactor - lerpIncrement * Time.fixedDeltaTime, 0f);
            if (lerpFactor > 0f)
            {
                // Continua a rotacionar suavemente com base no lerpFactor
                RotateTowardsInitialRotation();
            }
            else
            {
                // Retorna suavemente à rotação inicial
                ReturnToInitialRotation();
            }
        }
    }

    void OnMouseOver()
    {
        isMouseOver = true;
    }

    void OnMouseExit()
    {
        isMouseOver = false;
        lastMousePosition = Input.mousePosition;
    }

    void RotateTowardsMouse(Vector3 mouseDelta)
    {
        if (Mathf.Abs(mouseDelta.x) > Mathf.Abs(mouseDelta.y))
        {
            RotateHorizontally(mouseDelta.x);
        }
        else
        {
            RotateVertically(mouseDelta.y);
        }
    }

    void RotateHorizontally(float delta)
    {
        float rotationAmount = delta * speed * Time.fixedDeltaTime * lerpFactor;
        Quaternion deltaRotation = Quaternion.Euler(0, rotationAmount, 0);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    void RotateVertically(float delta)
    {
        float rotationAmount = delta * speed * Time.fixedDeltaTime * lerpFactor;
        Quaternion deltaRotation = Quaternion.Euler(-rotationAmount, 0, 0);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    void RotateTowardsInitialRotation()
    {
        Quaternion targetRotation = Quaternion.Lerp(rb.rotation, initialRotation, lerpFactor * Time.fixedDeltaTime);
        rb.MoveRotation(targetRotation);
    }

    void ReturnToInitialRotation()
    {
        rb.rotation = Quaternion.Slerp(rb.rotation, initialRotation, returnSpeed * Time.fixedDeltaTime);
        if (Quaternion.Angle(rb.rotation, initialRotation) < 0.1f)
        {
            rb.rotation = initialRotation;
        }
    }
}