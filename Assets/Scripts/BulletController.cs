using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletForce;
    public Transform gunProjectileSpawn;
    public int bulletDamage ;
    public GameObject gameManager;

    public GameObject bulletHitParticle;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        bulletDamage = gameManager.GetComponent<CountDownTimer>().bulletDamage;
        //add force to the bullet towards mouse direction
        GetComponent<Rigidbody>().AddForce(transform.right * bulletForce);
    }

    // Update is called once per frame
    void Update()
    { 
    }

    private void OnCollisionEnter(Collision other)
    {
        //destroy bullet on collision with enemy
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<EnemyController>().TakeDamage(bulletDamage);
            //instantiate particle where it hits
            GameObject bulletHitParticleClone = Instantiate(bulletHitParticle, other.transform.position, other.transform.rotation);
            Destroy(bulletHitParticleClone, 1f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
