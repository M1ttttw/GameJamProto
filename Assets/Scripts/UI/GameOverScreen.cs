using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverScreen : MonoBehaviour
{
    public void mainMenu() { SceneManager.LoadScene("Start"); }
    public void playAgain() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    public void quitGame() { Application.Quit(); }
}
