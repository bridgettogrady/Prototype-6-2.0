using UnityEngine;

public class RenderLine : MonoBehaviour
{
    public Transform startObject;
    public Transform endObject;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Set line properties
        // lineRenderer.startWidth = 0.05f;
        // lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Basic material
        // lineRenderer.startColor = Color.red;
        // lineRenderer.endColor = Color.red;
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (startObject == null) {
        }
        if (endObject == null) {
        }
        // Update the line to match the GameObjects' positions
        if (startObject != null && endObject != null)
        {
            lineRenderer.SetPosition(0, startObject.position);
            lineRenderer.SetPosition(1, endObject.position);
        }
    }
}
