using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject projectile;
    private bool cancast = true;
    private float cooldown = 3f;
    private int randability;
    private int health;
    private Collider collider;
    
    void Start()
    {
        randability = Random.Range(0,3);
        collider = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Invincibility(){
        if(cancast){
            cancast = false;
            collider.enabled = false;
            yield return new WaitForSeconds(2f);
            collider.enabled = true;
            yield return new WaitForSeconds(cooldown);
            cancast = true;
        }
    }
    private IEnumerator Dash(){
        if(cancast){
            cancast = false;

            yield return new WaitForSeconds(cooldown);
            cancast = true;
        }
    }
    private IEnumerator Heal(){
        if(cancast){
            cancast = false;

            yield return new WaitForSeconds(cooldown);
            cancast = true;
        }
    }
    private IEnumerator Projectile(){
        if(cancast){
            cancast = false;

            yield return new WaitForSeconds(cooldown);
            cancast = true;
        }
    }
}
