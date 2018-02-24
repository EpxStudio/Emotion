using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour {
    
    public float Damage = 10;
    public LayerMask whatToHit;

    public Transform EnergyBallPrefab;
    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;
    public bool facingRight;
    private bool firing = false;
    
    Transform firePoint;
    private Animator anim;
    private float shotDelay = 0;

    // Use this for initialization
    void Awake () {
        firePoint = transform.Find("FirePoint");
        anim = GetComponent<Animator>();
        if (firePoint == null) {
            Debug.LogError("No firepoint my dude");
        }
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("Energy", firing);
        if (transform.position.x < firePoint.position.x) {
            facingRight = true;
        }
        else {
            facingRight = false;
        }
        if (Input.GetButtonDown("Fire1") && anim.GetBool("Ground") && Time.time >= shotDelay) {
            firing = true;
            anim.SetBool("Energy", firing);
            Shoot();
            firing = false;
            shotDelay = Time.time + .25f;
        }
	}

    IEnumerator ExecuteAfterTime(float time) {
        yield return new WaitForSeconds(time);
        if (firing) {
            Shoot();
            firing = false;
        }
    }

    void Shoot() {
        Vector2 firePointPos = new Vector2(firePoint.position.x, firePoint.position.y);
        Vector2 rayDir = new Vector2(1,0);
        if (facingRight) {
            rayDir.x = 1;
        }
        else {
            rayDir.x = -1;
        }
        RaycastHit2D hit = Physics2D.Raycast(firePointPos, rayDir, 100, whatToHit);
        Debug.DrawLine(firePointPos, rayDir * 100, Color.cyan);
        if (Time.time >= timeToSpawnEffect) {
            Effect();
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
        if (hit.collider != null) {
            Debug.DrawLine(firePointPos, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage.");
        }
    }

    void Effect() {
        if (facingRight) {
            Instantiate(EnergyBallPrefab, firePoint.position, firePoint.rotation);
        }
        else {
            Instantiate(EnergyBallPrefab, firePoint.position, Quaternion.Euler(0, 180, 0));
        }
    }
}
