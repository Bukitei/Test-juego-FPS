using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int enemiesAlive;
    public int round;
    public int killed;
    public GameObject[] spawnPoints;
    public GameObject enemyPrefab;
    public TextMeshProUGUI roundText;
    public AudioClip round14;
    public AudioClip round59;
    public AudioClip round10;
    public AudioSource gameAudioSource; 
    public GameObject gameOverPanel;
    public TextMeshProUGUI roundSurvivedText;
    public TextMeshProUGUI enemiesKilled;
    public GameObject pausePanel;

    public Animator fadePanelAnimator;

    void Start()
    {
        gameAudioSource = GetComponent<AudioSource>();
        gameAudioSource.PlayOneShot(round14);
    }

    void Update()
    {
        if(enemiesAlive == 0)
        {
            round++;
            roundText.text = $"Round: {round}";
            if (round == 5){
                gameAudioSource.Stop();
                gameAudioSource.PlayOneShot(round59);
            }else if (round == 10){
                gameAudioSource.Stop();
                gameAudioSource.PlayOneShot(round10);
            }
            NextWaves(round);
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
                if(Time.timeScale == 1){
                    Pause();
                }else if(Time.timeScale == 0){
                    Resume();
                }
        }
    }

    public void NextWaves(int round)
    {
        for(int i = 0; i < round; i++)
        {
            int randomPos = Random.Range(0, spawnPoints.Length);
            GameObject spawnPoint = spawnPoints[randomPos];

            GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            enemyInstance.GetComponent<EnemyManager>().gameManager = GetComponent<GameManager>();
            enemiesAlive++;
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        roundSurvivedText.text = round.ToString();
        enemiesKilled.text = killed.ToString();

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void OnMenuButton(){
        Time.timeScale = 1;
        AudioListener.volume = 1;
        fadePanelAnimator.SetTrigger("FadeIn");
        Invoke("LoadMainMenuScene", 0.5f);
    }

    public void LoadMainMenuScene(){
        SceneManager.LoadScene(1);
    }

    public void Pause(){
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        AudioListener.volume = 0;
    }

    public void Resume(){
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        AudioListener.volume = 1;
    }
}
