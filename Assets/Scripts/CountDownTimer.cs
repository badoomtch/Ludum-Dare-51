using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI randomChangeText;
    public TextMeshProUGUI playerScoreText;
    public string randomChangeTextString;

    public int countdownTime;
    public int playerScore;

    public GameObject enemySpawnManager;
    public GameObject gunController;
    public GameObject EnemyController;

    public bool justStartedGame;

    public float enemySpeed = 1f;
    public int enemyMaxHealth = 1;
    public int bulletDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        justStartedGame = true;
        randomChangeText.text = "";
        StartCoroutine(CountdownToStart());
        enemySpawnManager.GetComponent<EnemySpawnManager>().SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        playerScoreText.text = "Score: " + playerScore;
        countdownText.text = countdownTime.ToString();
    }

    //coroutine to count down from 10 to 0
    IEnumerator CountdownToStart()
    {
        if (countdownTime > 0)
        {
            yield return new WaitForSeconds(1f);
            countdownTime--;
            StartCoroutine(CountdownToStart());
        }
        else
        {
            justStartedGame = false;
            enemySpawnManager.GetComponent<EnemySpawnManager>().SpawnEnemy();
            countdownTime = 10;
            StartCoroutine(CountdownToStart());

            if (!justStartedGame)
            {
                //pick random number between 0 and 10
                int randomNum = Random.Range(0, 10);

                switch (randomNum)
                {
                    case 0:
                        //increase gun max ammo by 1 or 2
                        //random number between 1 and 2
                        int randomNum2 = Random.Range(1, 3);
                        gunController.GetComponent<GunController>().gunMaxAmmo += randomNum2;
                        randomChangeTextString = "Gun Max Ammo Increased By " + randomNum2;
                        StartCoroutine(ShowTextForSeconds());
                        break;
                    case 1:
                        //decrease gun max ammo by 1 or 2
                        //random number between 1 and 2
                        int randomNum3 = Random.Range(1, 3);

                        if (gunController.GetComponent<GunController>().gunMaxAmmo < 3)
                        {
                            int randomNum4 = Random.Range(1, 3);
                            gunController.GetComponent<GunController>().gunMaxAmmo += randomNum4;
                            randomChangeTextString = "Gun Max Ammo Increased By " + randomNum4;
                            StartCoroutine(ShowTextForSeconds());
                        }
                        else
                        {
                            gunController.GetComponent<GunController>().gunMaxAmmo -= randomNum3;
                            randomChangeTextString = "Gun Max Ammo Decreased By " + randomNum3;
                            StartCoroutine(ShowTextForSeconds());
                        }
                        break;
                    case 2:
                        //make enemies move faster
                        enemySpeed += 0.5f;
                        randomChangeTextString = "Enemies Move Faster";
                        StartCoroutine(ShowTextForSeconds());
                        break;
                    case 3:
                        //make enemies move slower
                        if (enemySpeed <= 2f)
                        {
                            enemySpeed += 0.5f;
                            randomChangeTextString = "Enemies Move Faster";
                            StartCoroutine(ShowTextForSeconds());
                        }
                        else
                        {
                            enemySpeed -= 0.5f;
                            randomChangeTextString = "Enemies Move Slower";
                            StartCoroutine(ShowTextForSeconds());
                        }
                        break;
                    case 4:
                        //increase enemy health by 1
                        enemyMaxHealth += 1;
                        randomChangeTextString = "Enemies Health Increased By 1";
                        StartCoroutine(ShowTextForSeconds());
                        break;
                    case 5:
                        //decrease enemy health by 1
                        if (enemyMaxHealth < 2)
                        {
                            enemyMaxHealth += 1;
                            randomChangeTextString = "Enemies Health Increased By 1";
                            StartCoroutine(ShowTextForSeconds());
                        }
                        else
                        {
                            enemyMaxHealth -= 1;
                            randomChangeTextString = "Enemies Health Decreased By 1";
                            StartCoroutine(ShowTextForSeconds());
                        }
                        break;
                    case 6:
                        //increase bullet force by 200
                        gunController.GetComponent<GunController>().bulletForce += 200;
                        randomChangeTextString = "Bullet Force Increased By 200";
                        StartCoroutine(ShowTextForSeconds());
                        break;
                    case 7:
                        //decrease bullet force by 200
                        if (gunController.GetComponent<GunController>().bulletForce < 400)
                        {
                            gunController.GetComponent<GunController>().bulletForce += 200;
                            randomChangeTextString = "Bullet Force Increased By 200";
                            StartCoroutine(ShowTextForSeconds());
                        }
                        else
                        {
                            if (gunController.GetComponent<GunController>().bulletForce < 200)
                            {
                                gunController.GetComponent<GunController>().bulletForce += 200;
                                randomChangeTextString = "Bullet Force Increased By 200";
                                StartCoroutine(ShowTextForSeconds());
                            }
                            else
                            {
                                gunController.GetComponent<GunController>().bulletForce -= 200;
                                randomChangeTextString = "Bullet Force Decreased By 200";
                                StartCoroutine(ShowTextForSeconds());
                            }
                            StartCoroutine(ShowTextForSeconds());
                        }
                        break;
                    case 8:
                        //increase bullet damage by 1
                        bulletDamage += 1;
                        randomChangeTextString = "Bullet Damage Increased By 1";
                        StartCoroutine(ShowTextForSeconds());
                        break;
                    case 9:
                        //decrease bullet damage by 1
                        if (bulletDamage < 2)
                        {
                            bulletDamage += 1;
                            randomChangeTextString = "Bullet Damage Increased By 1";
                            StartCoroutine(ShowTextForSeconds());
                        }
                        else
                        {
                            bulletDamage -= 1;
                            randomChangeTextString = "Bullet Damage Decreased By 1";
                            StartCoroutine(ShowTextForSeconds());
                        }
                        break;
                }
            }
        }
    }

    //coroutine to show text for 3 seconds
    IEnumerator ShowTextForSeconds()
    {
        randomChangeText.text = randomChangeTextString;
        yield return new WaitForSeconds(3f);
        randomChangeText.text = "";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
