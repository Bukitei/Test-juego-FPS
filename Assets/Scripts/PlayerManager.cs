using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public float health = 100f;
    public TextMeshProUGUI healthText;
    public GameManager gameManager;
    public GameObject playerCamera;

    private float shakeTime = 1;
    private float shakeDuration = 0.5f;
    private Quaternion playerCameraOriginalRotation;

    private void Start() {
        playerCameraOriginalRotation = playerCamera.transform.localRotation;
    }

    private void Update(){
        if(shakeTime < shakeDuration){
            shakeTime += Time.deltaTime;
            CameraShake();
        }else if(playerCamera.transform.localRotation != playerCameraOriginalRotation){
            playerCamera.transform.localRotation = playerCameraOriginalRotation;
        }
    }

    public void Hit(float damage)
    {
        health -= damage;
        healthText.text = $"{health} HP";
        if(health <= 0)
        {
            gameManager.GameOver();
        }else{
            shakeTime = 0;
        }
    }

    public void CameraShake(){
        playerCamera.transform.localRotation = Quaternion.Euler(Random.Range(-2f, 2f), 0, 0);
    }
}
