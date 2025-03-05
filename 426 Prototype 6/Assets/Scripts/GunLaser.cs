using UnityEngine;

public class GunLaser : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public LineRenderer laser;
    public Transform firepoint;
    public LayerMask enemies;
    public GameObject reticle;
    public float range;
    void Start()
    {
        laser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
