using UnityEngine;

public class BombMove : MonoBehaviour
{
    private SpriteRenderer sp;
    private LineRenderer lineRenderer;
    private bool moving = true;
    public GameObject player;
    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        sp.enabled = false;

        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving) {
            transform.position += player.transform.up * speed * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            moving = false;
            Explode();
        }
    }

    private void Explode() {
        sp.enabled = true;
        lineRenderer.enabled = false;
    }

    public void SetPlayer(GameObject player) {
        this.player = player;
    }
}
