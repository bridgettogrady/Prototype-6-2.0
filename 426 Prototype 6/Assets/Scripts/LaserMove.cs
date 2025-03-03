using UnityEngine;

public class LaserMove : MonoBehaviour
{
    public float speed = 2f;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;

        // delete if out of bounds
        if (transform.position.x > maxX || transform.position.x < minX
            || transform.position.y > maxY || transform.position.y < minY) {
                Destroy(gameObject);
            }
    }
}
