using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;
    public LayerMask collisionLayers; // Layers to check for collision
    public float minCollisionCheckDistance = 0.5f;

    public float orbitRadius = 5.0f;
    public float orbitSensitivity = 0.5f;
    public float minY = -5.0f; // Limite mínimo no eixo Y
    public float maxY = 5.0f;  // Limite máximo no eixo Y
    public float zoomSensitivity = 2.0f;
    public float minDistance = 2.0f;
    public float maxDistance = 10.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void Update()
    {
        // Controle de zoom
        orbitRadius -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        orbitRadius = Mathf.Clamp(orbitRadius, minDistance, maxDistance);

        if (Input.GetMouseButton(0)) // Arrasto do mouse
        {
            yaw += Input.GetAxis("Mouse X") * orbitSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * orbitSensitivity;
            pitch = Mathf.Clamp(pitch, minY, maxY);
        }

        // Collision Detection
        RaycastHit hit;
        Vector3 direction = target.position - transform.position;
        float desiredOrbitDistance = Mathf.Max(minCollisionCheckDistance, orbitRadius);
        if (Physics.Raycast(target.position, -direction.normalized, out hit, desiredOrbitDistance, collisionLayers))
        {
            orbitRadius = hit.distance - minCollisionCheckDistance; // Move camera closer if there is an obstacle
        }
        else
        {
            orbitRadius = desiredOrbitDistance; // Otherwise, maintain the desired distance
        }

        Vector3 offset = new Vector3(0, 0, -orbitRadius);
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = target.position + rotation * offset;
        transform.LookAt(target);
    }
}