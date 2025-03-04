using UnityEngine;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public Vector2 spawnAttackOffset;

    // laser
    public GameObject laser;
    public float laserReload = 0.5f;
    bool laserEnabled = true;

    // fireball
    public GameObject fireball;
    public float fireballReload = 2.5f;
    bool fireballEnabled = true;

    // bomb
    public GameObject bomb;
    public float bombReload = 7f;
    bool bombEnabled = true;
    public BombMove bombScript;

    // block
    private Transform block;
    private SpriteRenderer blockSprite;
    public float minScale = 1f;
    public float scaleSpeed = 0.8f;
    public float maxScale;
    private float currScale;
    public float cooldown = 5f;
    private bool canBlock = true;

    // UI
    public ChooseAttack UIScript;
    int currAttack = 0; // 0: laser, 1: fireball, 2: bomb
    public UnityEngine.UI.Image healthbar;
    private float health = 100f;

    void Start() {
        bombScript.SetPlayer(gameObject);

        block = transform.Find("Block");
        blockSprite = block.GetComponent<SpriteRenderer>();
        currScale = maxScale;
    }

    // Update is called once per frame
    void Update()
    {
        currAttack = UIScript.GetCurrentAttack();

        if (Input.GetKey(KeyCode.A)) { // rotate counterclockwise
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D)) { // rotate clockwise
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }

        // inputs for attacking
        if (Input.GetKeyDown(KeyCode.Space)) { // attacks based on criteria being met
            if (currAttack == 0 && laserEnabled) {
                ShootLaser();
                StartCoroutine(ReloadLaser());
            }
            if (currAttack == 1 && fireballEnabled) {
                ShootFireball();
                StartCoroutine(ReloadFireball());
            }
            if (currAttack == 2 && bombEnabled) {
                ShootBomb();
                StartCoroutine(ReloadBomb());
            }
        }
        // input for blocking
        if (Input.GetKey(KeyCode.LeftShift)) {
            Block();
        }

        if(health <=0){
            Die();
        }
    }

    private void Block() {
        if (!canBlock) {
            return;
        }
        Debug.Log("blocking");
        blockSprite.enabled = true;
        currScale *= (scaleSpeed * Time.deltaTime);
        if (currScale > minScale) {
            block.transform.localScale = new Vector3(currScale, currScale, 0);
        }
        else {
            blockSprite.enabled = false;
        }
    }

    private void ShootLaser() {
        if (UIScript.LaserAttack()) {
            Vector3 spawnPos = transform.TransformPoint(new Vector3(spawnAttackOffset.x, spawnAttackOffset.y, 0));           
            Instantiate(laser, spawnPos, transform.rotation);            
        }
    }

    private void ShootFireball() {
        if (UIScript.FireballAttack()) {
            Vector3 spawnPos = transform.TransformPoint(new Vector3(spawnAttackOffset.x, spawnAttackOffset.y, 0));           
            Instantiate(fireball, spawnPos, transform.rotation);            
        }
    }

    private void ShootBomb() {
        if (UIScript.BombAttack()) {
            Vector3 spawnPos = transform.TransformPoint(new Vector3(spawnAttackOffset.x, spawnAttackOffset.y, 0));           
            Instantiate(bomb, spawnPos, transform.rotation);            
        }
    }

    IEnumerator ReloadLaser() {
        laserEnabled = false;
        yield return new WaitForSeconds(laserReload);
        laserEnabled = true;
    }

    IEnumerator ReloadFireball() {
        fireballEnabled = false;
        yield return new WaitForSeconds(fireballReload);
        fireballEnabled = true;
    }

    IEnumerator ReloadBomb() {
        bombEnabled = false;
        yield return new WaitForSeconds(bombReload);
        bombEnabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy")){
            health -= 10;
            float targetFill = health/100f;
            StartCoroutine(HealthAnimate(targetFill));
        }
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
    private void Die(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy(gameObject);
    }

}
