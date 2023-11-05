using UnityEngine;

public class ShipOnWaves : MonoBehaviour
{
    public float verticalMotionAmplitude = 1f;
    public float verticalMotionSpeed = 0.5f;
    public float pitchAmplitude = 5f; // Rotation around the x-axis
    public float rollAmplitude = 5f;  // Rotation around the z-axis
    public float rotationSpeed = 0.2f;
    public Vector3 waveDirection = new Vector3(1, 0, 0); // Waves coming from this direction

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        // Simulate the vertical movement
        float verticalOffset = Mathf.Sin(Time.time * verticalMotionSpeed) * verticalMotionAmplitude;
        transform.position = originalPosition + new Vector3(0f, verticalOffset, 0f);

        // Simulate the pitch and roll based on the wave direction
        float waveTime = Time.time * rotationSpeed;
        float pitch = Mathf.Sin(waveTime) * pitchAmplitude * waveDirection.x;
        float roll = Mathf.Sin(waveTime) * rollAmplitude * waveDirection.z;

        // Apply the rotation to the ship
        transform.rotation = originalRotation * Quaternion.Euler(pitch, 0f, roll);
    }
}