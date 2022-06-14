using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Volumes : MonoBehaviour
{

    public static bool is_music_muted = false;
    public GameObject music_mute;
    public GameObject sound_mute;
    public GameObject music_unmute;
    public GameObject sound_unmute;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
    public MusicController instance;
>>>>>>> Stashed changes
=======
    public MusicController instance;
>>>>>>> Stashed changes
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Mute_Music()
    {
        music_mute.SetActive(false);
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
        AudioListener.volume = 0;
>>>>>>> Stashed changes
=======
        AudioListener.volume = 0;
>>>>>>> Stashed changes
        music_unmute.SetActive(true);
    }
    
    public void Unmute_Music()
    {
        music_unmute.SetActive(false);
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
        AudioListener.volume = 1;
>>>>>>> Stashed changes
=======
        AudioListener.volume = 1;
>>>>>>> Stashed changes
        music_mute.SetActive(true);
        
    }
    
    public void Mute_Sound()
    {
        sound_mute.SetActive(false);
        sound_unmute.SetActive(true);
    }
    
    public void Unmute_Sound()
    {
        sound_unmute.SetActive(false);
        sound_mute.SetActive(true);
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
