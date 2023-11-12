using UnityEngine;

public class ZoomCameraInOnClick : MonoBehaviour
{
    public Transform objectToZoom; // Assign the object you want to zoom into.
    public float zoomSpeed = 1.0f; // Adjust this value to control the zoom speed.
    public float zoomDistance = 200.0f; // Adjust this value to control the zoom distance.

    private Vector3 originalCameraPosition;
    private bool isZoomed = false;

    private void Start()
    {
        originalCameraPosition = Camera.main.transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsMouseOverObject())
        {
            if (isZoomed)
            {
                // Zoom out
                Camera.main.transform.position = originalCameraPosition;
            }
            else
            {
                // Zoom in
                Vector3 targetPosition = objectToZoom.position - (objectToZoom.forward * zoomDistance);
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, Time.deltaTime * zoomSpeed);
            }

            isZoomed = !isZoomed;
        }
    }

    private bool IsMouseOverObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject == objectToZoom.gameObject;
        }

        return false;
    }
}
