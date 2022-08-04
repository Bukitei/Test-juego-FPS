using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public GameObject player;
    public Animator enemyAnimator;
    public GameManager gameManager;

    public float damage = 20f;
    public float health = 100f;

    public Slider healthBar;
    public bool playerReach = false;
    public float attackDelayTimer;
    public float howMuchEarlierStartAttackAnim;
    public float delayBetweenAttacks;

    private AudioSource enemyAudioSource;
    public AudioClip[] groalAudioClips;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAudioSource = GetComponent<AudioSource>();
        healthBar.maxValue = health;
        healthBar.value = health;
    }


    void Update()
    {

        if(!enemyAudioSource.isPlaying){
            enemyAudioSource.clip = groalAudioClips[Random.Range(0, groalAudioClips.Length)];
            enemyAudioSource.Play();
        }

        GetComponent<NavMeshAgent>().destination = player.transform.position;

        if(GetComponent<NavMeshAgent>().velocity.magnitude > 1)
        {
            enemyAnimator.SetBool("isRunning", true);
        }
        else
        {
            enemyAnimator.SetBool("isRunning", false);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject == player)
        {
            playerReach = true;           
        }
    }

    private void OnCollisionStay(Collision other) {
        if(playerReach){

            attackDelayTimer += Time.deltaTime;

            if(attackDelayTimer >= delayBetweenAttacks - howMuchEarlierStartAttackAnim && attackDelayTimer <= delayBetweenAttacks){
                enemyAnimator.SetBool("isAttacking", true);
            }
            if(attackDelayTimer >= delayBetweenAttacks)
            {
                player.GetComponent<PlayerManager>().Hit(damage);
                attackDelayTimer = 0;
            }
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject == player){
            playerReach = false;
            attackDelayTimer = 0f;
            enemyAnimator.SetBool("isAttacking", false);
        }
    }

    public void Hit(float damage){
        
        health -= damage;
        healthBar.value = health;

        if(health <= 0)
        {
            enemyAnimator.SetTrigger("isDead");
            gameManager.enemiesAlive--;
            gameManager.killed++;
            Destroy(gameObject, 10f);
            Destroy(GetComponent<NavMeshAgent>());
            Destroy(GetComponent<EnemyManager>());
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(transform.GetChild(2).gameObject);
        }
    }
}
