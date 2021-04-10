using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    public GameObject mainMenu;

    public void CloseOpenMenu()
    {
        mainMenu.SetActive(!mainMenu.activeInHierarchy);
        if(mainMenu.activeInHierarchy == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void QuitFromGame()
    {
        Application.Quit();
    }
}
