using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform player; 
    public GameObject projectile;
    public float cooldown = 3f;
    public float speed = 2f;
    private bool cancast = true;
    private int randability;
    private int health = 5;
    private new Collider collider;
    
    void Start()
    {
        randability = Random.Range(0,4);
        collider = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
        if(randability == 0){
            StartCoroutine(Invincibility());
        }else if(randability == 1){
            StartCoroutine(Dash());
        }else if(randability == 2){
            StartCoroutine(Heal());
        }else if(randability == 3){
            StartCoroutine(Projectile());
        }
    }

    private void Follow(){
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
    }

    // Abilities ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private IEnumerator Invincibility(){
        if(cancast){
            cancast = false;
            collider.enabled = false;
            Debug.Log("Invincible");
            yield return new WaitForSeconds(2f);
            collider.enabled = true;
            Debug.Log("Not Invincible");
            yield return new WaitForSeconds(cooldown);
            cancast = true;
        }
    }
    private IEnumerator Dash(){
        if(cancast){
            cancast = false;
            speed = 30f;
            Debug.Log("Dashing");
            yield return new WaitForSeconds(0.1f);
            speed = 2f;
            Debug.Log("Not Dashing");
            yield return new WaitForSeconds(cooldown);
            cancast = true;
        }
    }
    private IEnumerator Heal(){
        if(cancast){
            cancast = false;
            health += 1;
            Debug.Log("Healed");
            yield return new WaitForSeconds(cooldown);
            cancast = true;
        }
    }
    private IEnumerator Projectile(){
        if(cancast){
            cancast = false;
            GameObject projectileclone = Instantiate(projectile, transform.position, transform.rotation);
            Debug.Log("Shooting");
            yield return new WaitForSeconds(cooldown);
            cancast = true;
        }
    }
}
