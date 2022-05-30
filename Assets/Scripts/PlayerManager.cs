using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public float health = 100f;
    public TextMeshProUGUI healthText;
    public GameManager gameManager;
    void Start()
    {
        
    }

    void Update()
    { 

    }

    public void Hit(float damage)
    {
        health -= damage;
        healthText.text = $"{health} HP";
        if(health <= 0)
        {
            gameManager.GameOver();
        }
    }
}
