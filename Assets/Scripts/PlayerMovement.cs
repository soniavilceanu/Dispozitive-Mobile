using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheckTransform;

    [SerializeField] private TextMeshProUGUI scoreGUI;
    [SerializeField] private TextMeshProUGUI superJumpsGUI;
    [SerializeField] private TextMeshProUGUI colliderTextGUI;
    [SerializeField] private GameObject part;
    [SerializeField] private TextMeshProUGUI coord;
    [SerializeField] private TextMeshProUGUI highscore;
    
    public static int scoreValue = 0;

    bool jumpKeyWasPressed;
    bool leftShiftKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbody;
    private int superJumps = 0;
    private int level = 1;


    private Animator anim;
    private GameObject model;
    private string axis;
    private GameObject canvas;
    private int highscoreValue;
    
    


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
 
        superJumps = 0;

        anim = GetComponentInChildren<Animator>();
        model = GameObject.Find("Model");

        axis = "Horizontal";

        canvas = GameObject.Find("Canvas");



        highscore.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
        




    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }


        horizontalInput = Input.GetAxis(axis);

        if (horizontalInput != 0)
        {
            if(horizontalInput < 0)
                model.transform.rotation = Quaternion.Euler(model.transform.rotation.x, 180, model.transform.rotation.z);
            else model.transform.rotation = Quaternion.Euler(model.transform.rotation.x, 0, model.transform.rotation.z);
            anim.SetFloat("Speed", 1f, 0.1f, Time.deltaTime); //we are moving
        }
            
        else anim.SetFloat("Speed", 0f, 0.1f, Time.deltaTime); //we are idle

        //if the player is grounded
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, groundMask).Length != 0)
        {


            if (jumpKeyWasPressed)
            {
                anim.SetFloat("jumpSpeed", 1f);
                anim.SetBool("Jump", true);
                rigidbody.AddForce(Vector3.up * 5f, ForceMode.VelocityChange);
                jumpKeyWasPressed = false;

            }


            if (superJumps > 0)
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {

                    anim.SetFloat("jumpSpeed", 0.5f);

                    anim.SetBool("Jump", true);

                    rigidbody.AddForce(Vector3.up * 7f, ForceMode.VelocityChange);

                    jumpKeyWasPressed = false;


                    superJumps--;


                }
        }

        else anim.SetBool("Jump", false);

        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, horizontalInput * 3);



        scoreGUI.text = "Score: " + scoreValue;
        
        

        superJumpsGUI.text = "Super Jumps: " + superJumps;

        coord.text = "Coord.: " + "(" + System.Math.Round(this.gameObject.transform.position.x,1) + "," + System.Math.Round(this.gameObject.transform.position.y,1) + "," + System.Math.Round(this.gameObject.transform.position.z,1) + ")";
        
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 7)
        {
            //collected a coin
            Destroy(other.gameObject);

            colliderTextGUI.gameObject.SetActive(true);

            
            colliderTextGUI.text = "You collected a coin!";
            
            scoreValue++;

            if (scoreValue > PlayerPrefs.GetInt("Highscore", 0))
            {
                PlayerPrefs.SetInt("Highscore", scoreValue);
                highscore.text = scoreValue.ToString();
            }

            scoreGUI.color = Color.red;
            StartCoroutine(wait2(scoreGUI));
            StartCoroutine(wait());



        }

        if (other.gameObject.layer == 8)
        {
            //collected a super jump cube
            Destroy(other.gameObject);

            colliderTextGUI.gameObject.SetActive(true);

            
            colliderTextGUI.text = "You collected a SUPER coin!";

            superJumps++;
            superJumpsGUI.color = Color.red;
            StartCoroutine(wait2(superJumpsGUI));

            StartCoroutine(wait());

            

        }

        if (other.gameObject.layer == 9)
        {
            //we go to next level
            level++;

            

            if (level == 2)
            {
               
                //level 2
                Destroy(other.gameObject);

                this.gameObject.transform.position = new Vector3(0, 0, 51);
                //this.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                //axis = "Horizontal"; ???
            }
            else if (level == 3)
            {
                
                //end of game
                Destroy(other.gameObject);





               
                
            }
            
            
        }
    }


    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);

        colliderTextGUI.gameObject.SetActive(false);
    }

    IEnumerator wait2(TextMeshProUGUI gui)
    {
        yield return new WaitForSeconds(0.5f);

        gui.color = Color.white;
    }

   

    /*
    public void hideCanvas()
    {
        
        if(canvas.active == true)
        canvas.SetActive(false);

        else canvas.SetActive(true);
        
        //Destroy(canvas);

    }

    public void showCanvas()
    {
        //canvas.SetActive(true);
        //Instantiate(canvas);
    }*/
}
