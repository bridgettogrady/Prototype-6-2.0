using System.Collections;
using System.Diagnostics;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform player; 
    public GameObject projectile;
    public ParticleSystem heal;
    public TrailRenderer trail;
    public float cooldown = 3f;
    public float speed = 2f;
    private bool cancast = true;
    private int randability;
    private float health = 100f;
    private new Collider collider;
    private MeshRenderer meshRenderer;
    public Material flashmaterial;
    public Material originalmaterial;
    public float flashduration = 2f;

    // for taking damage
    public int laserDamage = 10;
    public int fireballDamage = 30;
    public int bombDamage = 50;
    private ChooseAttack UIScript;
    public UnityEngine.UI.Image healthbar;
    
    void Start()
    {
        randability = Random.Range(0,4);
        collider = gameObject.GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        trail = gameObject.GetComponent<TrailRenderer>();
        UIScript = FindFirstObjectByType<ChooseAttack>();
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
            StartCoroutine(Flashroutine());
            yield return new WaitForSeconds(2f);
            collider.enabled = true;
            yield return new WaitForSeconds(cooldown);
            cancast = true;
        }
    }
    private IEnumerator Dash(){
        if(cancast){
            cancast = false;
            speed = 30f;
            trail.emitting = true;
            yield return new WaitForSeconds(0.1f);
            speed = 1f;
            trail.emitting = false;
            yield return new WaitForSeconds(cooldown);
            cancast = true;
        }
    }
    private IEnumerator Heal(){
        if(cancast){
            cancast = false;
            if(health<=5){
                health += 1;
            }
            ParticleSystem particleclone = Instantiate(heal, transform.position + transform.forward * 1f, Quaternion.identity);
            Destroy(particleclone.gameObject,particleclone.main.duration);
            yield return new WaitForSeconds(cooldown);
            cancast = true;
        }
    }
    private IEnumerator Projectile(){
        if(cancast){
            cancast = false;
            GameObject projectileclone = Instantiate(projectile, transform.position, transform.rotation);
            yield return new WaitForSeconds(cooldown+3f);
            cancast = true;
        }
    }
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Laser")) {
            TakeDamage(laserDamage);
        }
        if (other.gameObject.CompareTag("Fireball")) {
            TakeDamage(fireballDamage);
        }
        if (other.gameObject.CompareTag("Bomb")) {
            TakeDamage(bombDamage);
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

    public void TakeDamage(int damage) {
        health -= damage;
        float targetFill = health/100f;
        StartCoroutine(HealthAnimate(targetFill));
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
        // generate random attack
        int attackType = UnityEngine.Random.Range(0, 3);
        int attackAmount = UnityEngine.Random.Range(2, 6);
        UIScript.SetAttack(attackType, attackAmount);
    }

    private IEnumerator HealthAnimate(float targetFill){
        float elapsed = 0f;
        float duration = 0.2f;
        float startFill = healthbar.fillAmount;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            healthbar.fillAmount = Mathf.Lerp(startFill, targetFill, elapsed / duration);
            yield return null;
        }
        healthbar.fillAmount = targetFill;
    }

}
