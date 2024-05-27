using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManage : MonoBehaviour
{
    public GameObject settingMenu;
    public GameObject extrasMenu;
 public void playButton()
    {
        SceneManager.LoadScene(1);
    }
 public void settingButton()
    {
        settingMenu.SetActive(true);
    }
 public void settingButtonoff()
    {
        settingMenu.SetActive(false);
    }
    public void extrasButton()
    {
        extrasMenu.SetActive(true);
    }
    public void extrasButtonoff()
    {
        extrasMenu.SetActive(false);
    }
    public void exitbutton()
    {
         // If running in the Unity editor
#if UNITY_EDITOR
        // Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If running as a built application
        Application.Quit();
#endif
    }
    public void Shopbutton()
    {
        SceneManager.LoadScene(2);
    }
}
