using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverMenu;

    // Subscribing
    private void OnEnable()
    {
        Player.OnPlayerDeath += EnableGameOverMenu;
    }
    // Unsubscribing
    private void OnDisable()
    {
        Player.OnPlayerDeath -= EnableGameOverMenu;
    }

    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void DisableGameOverMenu()
    {
        gameOverMenu.SetActive(false);
    }

    public void RestartGame()
    {
        Debug.Log("Clicked Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //public void GoToMainMenu()
    //{
    //    SceneManager.LoadScene("MainMenu");
    //}
}