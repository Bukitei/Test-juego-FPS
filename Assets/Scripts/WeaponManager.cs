using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject playerCam;
    public float range = 100f;
    public float damage = 10f;
    public Animator playerAnimator;
    public ParticleSystem flashParticleSystem;
    public GameObject bloodParticleSystem;
    public AudioClip gunShootClip;
    public AudioSource weaponAudioSource;

    void Start()
    {
        weaponAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(playerAnimator.GetBool("isShooting"))
        {
            playerAnimator.SetBool("isShooting", false);
        }
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }

    }

    private void Shoot(){

        playerAnimator.SetBool("isShooting", true);
        flashParticleSystem.Play();
        weaponAudioSource.PlayOneShot(gunShootClip, volumeScale: 1);

        RaycastHit hit;
        if(Physics.Raycast(playerCam.transform.position, transform.forward, out hit, range)){
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
}
