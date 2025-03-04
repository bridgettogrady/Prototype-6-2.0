using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform player;
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public ParticleSystem explosion;

    // Update is called once per frame
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            ParticleSystem particle = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(particle.gameObject, particle.main.duration);
        }
    }
}
