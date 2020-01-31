using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private float gameOverDelayInSeconds = 1.0f; 
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0); 
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game"); 
    }

    public void LoadGameOver()
    {
        StartCoroutine(GameOver()); 
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(gameOverDelayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
