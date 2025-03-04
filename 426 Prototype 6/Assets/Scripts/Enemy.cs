using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform player; 
    public GameObject projectile;
    public ParticleSystem heal;
    public float cooldown = 3f;
    public float speed = 2f;
    private bool cancast = true;
    private int randability;
    private int health = 5;
    private new Collider collider;
    private MeshRenderer meshRenderer;
    public Material flashmaterial;
    public Material originalmaterial;
    public float flashduration = 2f;

    
    void Start()
    {
        randability = Random.Range(0,4);
        collider = gameObject.GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
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
            Debug.Log("Starting Flash");
            StartCoroutine(Flashroutine());
            yield return new WaitForSeconds(2f);
            Debug.Log("Ending Flash");
            collider.enabled = true;
            yield return new WaitForSeconds(cooldown);
            cancast = true;
        }
    }
    private IEnumerator Dash(){
        if(cancast){
            cancast = false;
            speed = 30f;
            yield return new WaitForSeconds(0.1f);
            speed = 1f;
            yield return new WaitForSeconds(cooldown);
            cancast = true;
        }
    }
    private IEnumerator Heal(){
        if(cancast){
            cancast = false;
            health += 1;
            ParticleSystem particleclone = Instantiate(heal, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(cooldown);
            Destroy(particleclone);
            cancast = true;
        }
    }
    private IEnumerator Projectile(){
        if(cancast){
            cancast = false;
            GameObject projectileclone = Instantiate(projectile, transform.position, transform.rotation);
            yield return new WaitForSeconds(cooldown);
            cancast = true;
        }
    }
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            Destroy(gameObject);
        }
    }

    private IEnumerator Flashroutine(){
        float elapsed = 0f;
        while(elapsed < flashduration){
            meshRenderer.material = flashmaterial;
            yield return new WaitForSeconds(0.1f);
            meshRenderer.material = originalmaterial;
            yield return new WaitForSeconds(0.1f);
            elapsed+=0.2f;
        }
    }
}
