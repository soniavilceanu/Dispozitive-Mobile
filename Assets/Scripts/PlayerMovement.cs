using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

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
    [SerializeField] private Joystick joystick;

    
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

        

        canvas = GameObject.Find("Canvas");



        highscore.text = PlayerPrefs.GetInt("Highscore", 0).ToString();



      
    }

    // Update is called once per frame
    void Update()
    {
       


        var validTouches = Input.touches.Where(touch => !EventSystem.current.IsPointerOverGameObject(touch.fingerId)).ToArray();

        
        //PC CONTROLS FOR JUMP
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");

        /*

        //ANDROID CONTROLS FOR JUMP
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && validTouches.Length == 1 && touch.tapCount == 1)
            {
                jumpKeyWasPressed = true;
            }
        }


        if (joystick.Horizontal >= .2f)
            horizontalInput = joystick.Horizontal;
        else if (joystick.Horizontal <= -.2f)
            horizontalInput = joystick.Horizontal;
        else horizontalInput = 0;

        
      
        */



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
            {

                //PC CONTROLS FOR SUPER JUMP
                
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    anim.SetFloat("jumpSpeed", 0.5f);

                    anim.SetBool("Jump", true);

                    rigidbody.AddForce(Vector3.up * 7f, ForceMode.VelocityChange);

                    jumpKeyWasPressed = false;


                    superJumps--;
                }
                

                /*


                //ANDROID CONTROLS FOR SUPER JUMP
                //DOESN'T WORK
                // WE WILL USE UI BUTTON
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began && validTouches.Length == 2 && touch.tapCount == 2)
                    {
                        anim.SetFloat("jumpSpeed", 0.5f);

                        anim.SetBool("Jump", true);

                        rigidbody.AddForce(Vector3.up * 7f, ForceMode.VelocityChange);

                        jumpKeyWasPressed = false;


                        superJumps--;
                    }
                }*/
              
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

                //highscore.text = PlayerPrefs.GetInt("Highscore", 0);
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
               
            }
            else if (level == 3)
            {
                
                //end of game
                Destroy(other.gameObject);





               
                
            }
            
            
        }
    }


    public bool IsPointerOverUI(int fingerId)
    {
        // EventSystem eventSystem = eventSystem.current;
        //return (eventSystem.IsPointerOverGameObject(fingerId)
        //     && eventSystem.currentSelectedGameObject != null);

        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            //we touched the UI
            return true;
        }
        else return false;

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

   
    public void SuperJump()
    {

        if (superJumps > 0)
        {
            anim.SetFloat("jumpSpeed", 0.5f);

            anim.SetBool("Jump", true);

            rigidbody.AddForce(Vector3.up * 7f, ForceMode.VelocityChange);

            jumpKeyWasPressed = false;


            superJumps--;
        }
          
    }

}
