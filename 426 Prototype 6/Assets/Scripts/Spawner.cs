using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject enemy;
    public float radius = 10f;
    public float spawncooldown = 5f;
    public float cooldowndecrease = 0.05f;
    bool justspawned = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(spawnenemy());
        spawncooldown = Mathf.Max(1f, spawncooldown - cooldowndecrease * Time.deltaTime);
    }
    private IEnumerator spawnenemy(){
        if(justspawned == false){
            justspawned = true; 
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad; 
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            Vector3 spawnposition = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
            GameObject enemyclone = Instantiate(enemy, spawnposition, Quaternion.identity);
            yield return new WaitForSeconds(spawncooldown);
            justspawned = false;
        }
    }
}
