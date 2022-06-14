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

<<<<<<< Updated upstream
=======
    public void Return_To_Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

>>>>>>> Stashed changes
    
    public void Exit_Game()
    {
        Application.Quit();
    }
}
