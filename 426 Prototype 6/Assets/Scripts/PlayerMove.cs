using UnityEngine;
using System.Collections;

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

    // UI
    public ChooseAttack UIScript;
    int currAttack = 0; // 0: laser, 1: fireball, 2: bomb

    void Start() {
        bombScript.SetPlayer(gameObject);
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
                // StartCoroutine(ReloadBomb());
            }
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
        // if (UIScript.BombAttack()) {
            Vector3 spawnPos = transform.TransformPoint(new Vector3(spawnAttackOffset.x, spawnAttackOffset.y, 0));           
            Instantiate(bomb, spawnPos, transform.rotation);            
        // }
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

}
