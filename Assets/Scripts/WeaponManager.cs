using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject crossHair;
    public float range = 100f;
    public float damage = 10f;
    public Animator playerAnimator;
    public ParticleSystem flashParticleSystem;
    public GameObject bloodParticleSystem;
    public AudioClip gunShootClip;
    public AudioSource weaponAudioSource;
    public WeaponSway weaponSway;
    public float swaySensitivity;
    public TextMeshProUGUI reserveAmmoText;
    public TextMeshProUGUI currentAmmoText;

    public float currentAMMO;
    public float maxxAMMO;
    public float reloadTime;
    public bool isReloading;
    public float reserveAMMO;

    void Start()
    {
        weaponAudioSource = GetComponent<AudioSource>();
        swaySensitivity = weaponSway.swaySensitivity;
        reserveAmmoText.text = $"{reserveAMMO}";
    }

    void Update()
    {
        currentAmmoText.text = $"{currentAMMO}";
        if(currentAMMO <= 0 && !isReloading)
        {
            Debug.Log("No tienes balas");
            StartCoroutine(Reload(reloadTime));
            return;
        }
        if(isReloading){
            return;
        }
        if(Input.GetKeyDown(KeyCode.R) && currentAMMO > 0){
            Debug.Log("Recarga manual de las balas");
            StartCoroutine(Reload(reloadTime));
            return;
        }
        if(playerAnimator.GetBool("isShooting"))
        {
            playerAnimator.SetBool("isShooting", false);
        }
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
        if(Input.GetButtonDown("Fire2")){
            Aim();
        }

        if(Input.GetButtonUp("Fire2")){
            playerAnimator.SetBool("isAiming", false);
            crossHair.SetActive(true);
            weaponSway.swaySensitivity = swaySensitivity;
        }

    }

    private void Shoot(){

        currentAMMO--;

        playerAnimator.SetBool("isShooting", true);
        flashParticleSystem.Play();
        weaponAudioSource.PlayOneShot(gunShootClip, volumeScale: 1);

        RaycastHit hit;
        if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range)){
            if(hit.transform.tag == "Enemy"){
                hit.transform.GetComponent<EnemyManager>().Hit(damage);
                GameObject particleInstance = Instantiate(bloodParticleSystem, hit.point, Quaternion.LookRotation(hit.normal));
                particleInstance.transform.parent = hit.transform;
            }
            else{
                Debug.Log("Hitted another thing");
            }
        }
    }

    private void Aim(){
        playerAnimator.SetBool("isAiming", true);
        crossHair.SetActive(false);
        weaponSway.swaySensitivity = swaySensitivity / 3;
    }

    public IEnumerator Reload(float reloadTime){
        if(currentAMMO == maxxAMMO){
            yield break;
        }   
        isReloading = true;
        playerAnimator.SetBool("isReloading", true);
        if(playerAnimator.GetBool("isAiming") == true){
            playerAnimator.SetBool("isAiming", false);
            crossHair.SetActive(true);
            weaponSway.swaySensitivity = swaySensitivity;
        }
        yield return new WaitForSeconds(reloadTime);
        playerAnimator.SetBool("isReloading", false);
        float missingAMMO = maxxAMMO - currentAMMO;
        if(reserveAMMO > missingAMMO){
            currentAMMO += missingAMMO;
            reserveAMMO -= missingAMMO;
        }else{
            currentAMMO += reserveAMMO;
            reserveAMMO = 0;
        }
        reserveAmmoText.text = $"{reserveAMMO}";
        isReloading = false;
    }
}
