using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public float health = 100f;
    public TextMeshProUGUI healthText;
    public GameManager gameManager;
    public GameObject playerCamera;

    private void Update(){
        CameraShake();
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

    public void CameraShake(){
        playerCamera.transform.localRotation = Quaternion.Euler(Random.Range(-2f, 2f), 0, 0);
    }
}
