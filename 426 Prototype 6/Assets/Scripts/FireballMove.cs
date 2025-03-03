using UnityEngine;
using System.Collections;

public class FireballMove : MonoBehaviour
{
    private GameObject player;
    public float speed;
    public float lifetime = 3f;

    private Vector3 playerFacing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        playerFacing = new Vector3(player.transform.up.x, player.transform.up.y, 0);
        StartCoroutine(SetLife());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += playerFacing * speed * Time.deltaTime;
    }

    IEnumerator SetLife() {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
