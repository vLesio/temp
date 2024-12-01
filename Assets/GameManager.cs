using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject _canvasGameObject;
    private void Start()
    {
        _canvasGameObject = GameObject.Find("GameOverScreen"); 
    }

    public void PlayerDied() {
        _canvasGameObject.SetActive(true);
    }
    
    public void RestartGame() {
        SceneManager.LoadScene("Act_1");
    }
    
    public void QuitGame() {
        Application.Quit();
    }
}
