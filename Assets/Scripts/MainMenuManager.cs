using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
   public void OnStartButton(){
       SceneManager.LoadScene(0);
   }

   public void OnExitButton(){
       Debug.Log("Quit");
       Application.Quit();
   }
}


