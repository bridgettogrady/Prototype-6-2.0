using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject enemy;
    public float radius = 10f;
    public float spawncooldown = 3f;
    bool justspawned = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(spawnenemy());
    }
    private IEnumerator spawnenemy(){
        if(justspawned == false){
            justspawned = true; 
            GameObject enemyclone = Instantiate(enemy, transform.position, transform.rotation);
            yield return new WaitForSeconds(spawncooldown);
        }
    }
}
