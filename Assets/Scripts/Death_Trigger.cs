using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death_Trigger : MonoBehaviour
{

    public bool is_player;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        is_player = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (is_player)
        {
            SceneManager.LoadScene("DeathScreen");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            is_player = true;
        }
    }
}
