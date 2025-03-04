using UnityEngine;

public class RenderLine : MonoBehaviour
{
    private Vector2 startPos = new Vector2(0, 0);
    public Transform endObject;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Set line properties
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Basic material
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        // Update the line to match the GameObjects' positions
        if (endObject != null)
        {
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, endObject.position);
        }
    }
}
