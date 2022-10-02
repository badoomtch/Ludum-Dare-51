using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class GunController : MonoBehaviour
{
    public GameObject gunProjectile;
    public Transform gunProjectileSpawn;
    public float bulletForce;

    public Slider gunAmmoSlider;
    public int gunAmmo;
    public int gunMaxAmmo;
    public TextMeshProUGUI gunAmmoText;
    public GameObject shotLight;
    public GameObject gunSmokeParticle;

    public AudioSource gunShotSound;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReloadAmmo());
        gunAmmo = gunMaxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        gunAmmoSlider.maxValue = gunMaxAmmo;
        gunAmmoSlider.value = gunAmmo;
        gunAmmoText.text = gunAmmo + "/" + gunMaxAmmo;

        //on mouse click, instantiate a bullet at the gunProjectileSpawn position
        if (Input.GetMouseButtonDown(0))
        {
            if (gunAmmo > 0)
            {
                StartCoroutine(ShotLight());
                //play gunshot sound with randomised pitch
                gunShotSound.pitch = Random.Range(0.8f, 1.2f);
                gunShotSound.Play();
                //instantiate gunSmokeParticle
                GameObject gunSmokeParticleClone = Instantiate(gunSmokeParticle, gunProjectileSpawn.position, gunProjectileSpawn.rotation);
                Destroy(gunSmokeParticleClone, 2f);
                Instantiate(gunProjectile, gunProjectileSpawn.position, gunProjectileSpawn.rotation);
                gunAmmo--;
            }
        }
    }

    //coroutine to enable shotlight
    IEnumerator ShotLight()
    {
        shotLight.SetActive(true);
        yield return new WaitForSeconds(.1f);
        shotLight.SetActive(false);
    }

    //coroutine that slowly fills up the ammo
    IEnumerator ReloadAmmo()
    {
        yield return new WaitForSeconds(.35f);
        if (gunAmmo < gunMaxAmmo)
        {
            gunAmmo++; 
        }
        StartCoroutine(ReloadAmmo());
    }
}
