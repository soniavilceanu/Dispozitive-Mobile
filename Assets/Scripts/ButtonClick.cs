using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonClick : MonoBehaviour
{

    [SerializeField] private GameObject part;
    [SerializeField] private TextMeshProUGUI coord;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOnFireworks()
    {


        if (GameObject.Find("Fireworks(Clone)") == true)
        {
            GameObject fireworks = GameObject.Find("Fireworks(Clone)");
            Destroy(fireworks);
        }
        else Instantiate(part);


        if (coord.gameObject.active == true)
            coord.gameObject.SetActive(false);

        else coord.gameObject.SetActive(true);

    }
}
