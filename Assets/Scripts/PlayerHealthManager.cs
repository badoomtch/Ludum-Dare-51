using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public int playerHealth;
    public int playerMaxHealth;
    public Slider playerHealthSlider;
    public TextMeshProUGUI playerHealthText;
    public GameObject bloodParticles;

    public GameObject deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        deathScreen.SetActive(false);
        playerHealth = playerMaxHealth;
        playerHealthSlider.maxValue = playerMaxHealth;
        playerHealthSlider.value = playerMaxHealth;
        playerHealthText.text = playerHealth + "/" + playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthText.text = playerHealth + "/" + playerMaxHealth;
        playerHealthSlider.value = playerHealth;
    }

    public void TakeDamage(int damage)
    {
        GameObject bloodParticlesClone = Instantiate(bloodParticles, transform.position, transform.rotation);
        Destroy(bloodParticlesClone, 1f);
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Destroy(gameObject);
            deathScreen.SetActive(true);
        }
    }
}
