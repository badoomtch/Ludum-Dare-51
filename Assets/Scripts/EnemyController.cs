using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class EnemyController : MonoBehaviour
{
    public GameObject gameManager;

    private float speed;
    public Transform target;

    public int enemyHealth;
    public int enemyMaxHealth;
    public Slider enemyHealthSlider;

    public TextMeshProUGUI enemyHealthText;
    
    public GameObject player;
    public bool isNearPlayer;
    public bool isHittingPlayer;

    public AudioSource movingSound;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        speed = gameManager.GetComponent<CountDownTimer>().enemySpeed;

        target = GameObject.Find("PlayerTransform").transform;
        player = GameObject.Find("PLAYER");
        enemyMaxHealth = gameManager.GetComponent<CountDownTimer>().enemyMaxHealth;
        enemyHealth = enemyMaxHealth;
        enemyHealthSlider.maxValue = enemyMaxHealth;
        enemyHealthSlider.value = enemyMaxHealth;
        enemyHealthText.text = enemyHealth + "/" + enemyMaxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        float step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        transform.LookAt(target);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target.position) < 3f)
        {
            speed = 0f;
            isNearPlayer = true;
            if (!isHittingPlayer)
            {
                StartCoroutine(HitPlayer());
            }

        } else 
        {
            speed = gameManager.GetComponent<CountDownTimer>().enemySpeed;
            isNearPlayer = false;
            StopCoroutine(HitPlayer());
        }

        enemyHealthSlider.value = enemyHealth;
        enemyHealthText.text = enemyHealth + "/" + enemyMaxHealth;
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            //player score increase by 1
            Destroy(gameObject);
            gameManager.GetComponent<CountDownTimer>().playerScore += 1;
            isNearPlayer = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine(EnemySpeedZero());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            speed = gameManager.GetComponent<CountDownTimer>().enemySpeed;
        }
    }

    //coroutine to hit player every 1 second
    IEnumerator HitPlayer()
    {
        if (isNearPlayer && !isHittingPlayer)
        {
            isHittingPlayer = true;
            player.GetComponent<PlayerHealthManager>().TakeDamage(1);
            yield return new WaitForSeconds(1);
            isHittingPlayer = false;
        }
    }

    IEnumerator EnemySpeedZero()
    {
        speed = 0f;
        yield return new WaitForSeconds(1);
        speed = gameManager.GetComponent<CountDownTimer>().enemySpeed;
    }

    //coroutine to play moving sound between 1 and 3 seconds
    IEnumerator PlayMovingSound()
    {
        movingSound.Play();
        yield return new WaitForSeconds(Random.Range(1, 3));
        //random pitch for moving sound
        movingSound.pitch = Random.Range(0.5f, 1.5f);
        StartCoroutine(PlayMovingSound());
    }
}
