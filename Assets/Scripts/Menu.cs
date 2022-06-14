using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene("MainGame"); 
    }
    
    public void Settings()
    {
        SceneManager.LoadScene("SettingsMenu"); 
    }

    
    public void Exit_Game()
    {
        Application.Quit();
    }
}
