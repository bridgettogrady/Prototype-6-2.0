using UnityEngine;
using System.Collections;

public class BombMove : MonoBehaviour
{
    private SpriteRenderer sp;
    private LineRenderer lineRenderer;
    private bool moving = true;
    public GameObject player;
    private Transform explosionRadius;
    private SpriteRenderer explosionRenderer;
    public float speed = 5f;
    public float bombTime = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        sp.enabled = false;

        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.enabled = true;

        explosionRadius = transform.Find("Explosion Radius");
        explosionRenderer = explosionRadius.GetComponent<SpriteRenderer>();
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
        explosionRenderer.enabled = true;
        StartCoroutine(BombTick());
    }

    public void SetPlayer(GameObject player) {
        this.player = player;
    }

    private IEnumerator BombTick() {
        yield return new WaitForSeconds(bombTime);
        Destroy(gameObject);
    }
}
