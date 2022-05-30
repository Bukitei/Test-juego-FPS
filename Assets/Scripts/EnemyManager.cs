using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public GameObject player;
    public Animator enemyAnimator;
    public GameManager gameManager;
    public AudioClip continuous;
    public AudioClip hitted;
    public AudioClip death;
    public AudioSource enemyAudioSource;

    public float damage = 20f;
    public float health = 50f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAudioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
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
            player.GetComponent<PlayerManager>().Hit(damage);
            
        }
    }

    public void Hit(float damage){
        health -= damage;
        if(health <= 0)
        {
            gameManager.enemiesAlive--;
            gameManager.killed++;
            Destroy(gameObject);
        }
    }
}
