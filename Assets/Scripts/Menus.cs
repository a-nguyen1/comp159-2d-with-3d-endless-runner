using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    //Opens GameScene
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/PlayerTestScene"); //Eventually link back to 'Scenes/GameScene
    }

    //Opens Controls Scene
    public void OpenControls()
    {
        SceneManager.LoadScene("Scenes/ControlsMenuScene");
    }

    //Returns to the Main Menu Scene
    public void ReturnMain()
    {
        SceneManager.LoadScene("Scenes/MainMenuScene");
    }
}
