using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveDown : MonoBehaviour
{
    public float V_Units;
    public float speed;
   public Collider2D col2d;

    //public SpriteRenderer render2d;

    public bool wasCol2dDisabled = false;

    public int soundID;

    public bool wasPressed = false;

    public bool isTapPrefab = false;
    public bool hold = false;
    public float holdSeconds; // change inspector
    public float secondsPressed;
    public float goodScalar = 0.90f;// change inspector

    public int myFingerID = 11;
    public bool wasFingAssigned; 
     
    public bool isEmpty = false;

    //public int AudioSourceIndex;

    public bool isHelper;
    public bool isGuide;

    public Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myStart();
    }

    public void myStart()
    {
        float height = float.Parse(Screen.height.ToString());
        float width = float.Parse(Screen.width.ToString());
        V_Units = height / width * 5;
        col2d = transform.GetComponent<Collider2D>();




        secondsPressed = 0;
    }


    public void beginCoroutine()
    {
        StartCoroutine(DisableGameObject());

    }

    IEnumerator DisableGameObject()
    {

        yield return new WaitForSeconds(3f);

        transform.gameObject.SetActive(false);
    }
    
    void FixedUpdate()
    {
        //Moves Down
        //transform.Translate(0, -speed , 0);
        rb.MovePosition(transform.position  + new Vector3(0f,-0.08f,0f));//new Vector2(0f, (-speed * V_Units * Time.deltaTime))); 

    }
    
    void Update()
    {



        if ((isTapPrefab && !isHelper) || hold)
        {
            if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("HoldPressAnim"))
            {

                gameObject.GetComponent<Animator>().speed = PoolManager.instance.GlobalSpeed / 0.3378f * 1f;


            }

        } 


         



        

        //Moves Down
       // transform.Translate(0, -speed * V_Units * Time.deltaTime, 0);
        



        //COLLIDER SWITCHER

        if (!isGuide && !hold)
        {
            if (wasPressed)
            {
                if (transform.position.x >= -2 && transform.position.x <= 2)
                {
                    col2d.enabled = false;

                }
            }
          

            
                if (Input.touchCount > 0)
                {

                if (transform.position.x >= -2 && transform.position.x <= 2)
                {
                    if (!wasPressed)
                    {
                        col2d.enabled = true;
                    }
                }   

                }
            
        }



        if (Input.touchCount > 0) // IF there is at least one touch
        {

          




            if (!isEmpty) // if is not an empty prefab
            {
              





                //Loop through all Touches
                for (int i = 0; i < Input.touchCount; i++)
                {

                    // Convert Screen Coordinated to WorldPoint
                    Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                    Vector2 touchPos = new Vector2(wp.x, wp.y);


                    if (!hold && isTapPrefab)// Prefab is NOT Hold that Means is TAP
                    {

                        if (!wasCol2dDisabled)// IF the collider is not disabled yet
                        {
                            //Checks if Position is withing Collider and Phase is Began
                            if (col2d == Physics2D.OverlapPoint(touchPos) && Input.GetTouch(i).phase == TouchPhase.Began)
                            {
                                 

                                // Bad Tap
                                if (transform.position.y >= PoolManager.instance.UpperLimitBad || transform.position.y <= PoolManager.instance.DownerLimitBad) // Bad
                                {
                                    /*PoolManager.instance.txt.text = "Bad";
                                    PoolManager.instance.txt.color = new Color(Random.value, Random.value, Random.value, 1.0f);
                                    */PoolManager.instance.ResetCombo();

                                    wasPressed = true;//new
                                    PoolManager.instance.sp.SpawnAudio(soundID); 
                                    

                                    //DO BAD TAP ANIMATION 
                                    gameObject.GetComponent<Animator>().enabled = false;
                                    gameObject.GetComponent<Animator>().enabled = true;
                                    gameObject.GetComponent<Animator>().Play("TapBad");

                                    StartCoroutine(DisableGameObject());
                                }
                                // Excellent Tap
                                else if (transform.position.y > PoolManager.instance.DownerLimitGood && transform.position.y < PoolManager.instance.UpperLimitGood) //Excellent
                                {
                                  /*  PoolManager.instance.txt.text = "Excellent";
                                    PoolManager.instance.txt.color = new Color(Random.value, Random.value, Random.value, 1.0f);
                                    */PoolManager.instance.AddToCombo(1);

                                    wasPressed = true;//new
                                    PoolManager.instance.sp.SpawnAudio(soundID);
                                    //DO EXCELLENT TAP ANIMATION 
                                    gameObject.GetComponent<Animator>().enabled = false;
                                    gameObject.GetComponent<Animator>().enabled = true;
                                    gameObject.GetComponent<Animator>().Play("TapExcellent");

                                    StartCoroutine(DisableGameObject());
                                }
                                else // Good Tap
                                {
                                  /*  PoolManager.instance.txt.text = "Good";
                                    PoolManager.instance.txt.color = new Color(Random.value, Random.value, Random.value, 1.0f);
                                    */PoolManager.instance.AddToCombo(1);

                                    wasPressed = true;//new
                                    PoolManager.instance.sp.SpawnAudio(soundID);

                                    //DO GOOD TAP ANIMATION 
                                    gameObject.GetComponent<Animator>().enabled = false;
                                    gameObject.GetComponent<Animator>().enabled = true;
                                    gameObject.GetComponent<Animator>().Play("TapGood");

                                    StartCoroutine(DisableGameObject());
                                }

                                //Regardless of any result, we will disable the GameObject
                                //DISABLED GAME OBJECT!!!!!! TRIGGER ANIMATION
                               col2d.enabled = false;
                               wasCol2dDisabled = true;
                               ////transform.gameObject.SetActive(false);

                            }
                        }
                        
                    }
                     
                    //-----------------------------------------------------------------------------------------------------

                    /*Some of the prefabs are not pressed yet but triggered so comparasion cannot be made precisely make a defaul as 11 and check if hasbeen asigned to finger id if not then set one
                    * set the default to 11 and we need to check only if has been setted , not if it has been pressed , if not setted yet, then set one to be the actual finger triggering the action , if setted , compare */



                    if (hold) // If IS Hold
                    {

                        if (!wasCol2dDisabled)   // IF the collider is not disabled yet
                        {

                            //IF Touch starts in the Collider and finger was not assigned
                            if (col2d == Physics2D.OverlapPoint(touchPos) && Input.GetTouch(i).phase == TouchPhase.Began && !wasFingAssigned)
                            {
                                myFingerID = Input.GetTouch(i).fingerId;
                                wasFingAssigned = true;

                                //DO START HOLD ANIMATION 
                                gameObject.GetComponent<Animator>().Play("HoldPressAnim");
                            }



                            // If Hold Ended  within collider
                            if (col2d == Physics2D.OverlapPoint(touchPos) && Input.GetTouch(i).phase == TouchPhase.Ended/* || col2d == Physics2D.OverlapPoint(touchPos) && Input.GetTouch(i).phase == TouchPhase.Canceled*/)
                            {

                                // if no finger assigned yet, assign one
                                if (!wasFingAssigned)
                                {
                                    myFingerID = Input.GetTouch(i).fingerId;
                                    wasFingAssigned = true;


                                  

                                }

                                //If is the same finger invocator
                                if (Input.GetTouch(i).fingerId == myFingerID)
                                {



                                    if (secondsPressed < (holdSeconds * goodScalar))
                                    {

                                        ////Debug.Log("Bad Hold Premature Release within collider ");
                                      /*  PoolManager.instance.txt.text = "Bad Hold";
                                        PoolManager.instance.txt.color = new Color(Random.value, Random.value, Random.value, 1.0f);
                                        */PoolManager.instance.ResetCombo();

                                        //DO BAD HOLD ANIMATION 
                                        gameObject.GetComponent<Animator>().enabled = false;
                                        gameObject.GetComponent<Animator>().enabled = true;
                                        gameObject.GetComponent<Animator>().Play("HoldPressBad");

                                        StartCoroutine(DisableGameObject());

                                        //DISABLED GAME OBJECT!!!!!! TRIGGER ANIMATION
                                        col2d.enabled = false;
                                        wasCol2dDisabled = true;
                                        ////transform.gameObject.SetActive(false);
                                    }
                                    else if (secondsPressed >= (holdSeconds * goodScalar) && secondsPressed < holdSeconds)
                                    {
                                       /* PoolManager.instance.txt.text = "Good Hold";
                                        ////Debug.Log("Good Hold");
                                        PoolManager.instance.txt.color = new Color(Random.value, Random.value, Random.value, 1.0f);
                                        */PoolManager.instance.AddToCombo(1);

                                        //DO GOOD HOLD ANIMATION 
                                        gameObject.GetComponent<Animator>().enabled = false;
                                        gameObject.GetComponent<Animator>().enabled = true;
                                        gameObject.GetComponent<Animator>().Play("HoldPressGood");

                                        StartCoroutine(DisableGameObject());

                                        //DISABLED GAME OBJECT!!!!!! TRIGGER ANIMATION
                                        col2d.enabled = false;
                                        wasCol2dDisabled = true;
                                        ////transform.gameObject.SetActive(false);

                                    }


                                }


                            }
                            //If Hold is (Cancelled, Ended, Moved) outside collider (Premature release out of Collider)
                            else if (col2d != Physics2D.OverlapPoint(touchPos) && Input.GetTouch(i).phase == TouchPhase.Ended || /*col2d != Physics2D.OverlapPoint(touchPos) && Input.GetTouch(i).phase == TouchPhase.Canceled ||*/ col2d != Physics2D.OverlapPoint(touchPos) && Input.GetTouch(i).phase == TouchPhase.Moved)
                            {


                                //If is the same finger invocator
                                if (Input.GetTouch(i).fingerId == myFingerID)
                                {
                                    ////Debug.Log("Bad Hold Premature Release outside collider");
                                   /* PoolManager.instance.txt.text = "Bad Hold";
                                    PoolManager.instance.txt.color = new Color(Random.value, Random.value, Random.value, 1.0f);
                                    */PoolManager.instance.ResetCombo();

                                    //DO BAD HOLD ANIMATION 
                                    gameObject.GetComponent<Animator>().enabled = false;
                                    gameObject.GetComponent<Animator>().enabled = true;
                                    gameObject.GetComponent<Animator>().Play("HoldPressBad");

                                    StartCoroutine(DisableGameObject());

                                    //DISABLED GAME OBJECT!!!!!! TRIGGER ANIMATION
                                    col2d.enabled = false;
                                    wasCol2dDisabled = true;
                                    ////transform.gameObject.SetActive(false);
                                }
                                //}
                            }
                            //-----------------------------------------------------------------------------------------------------

                            ///If Hold Stationary or Moved Within Collider
                            if (col2d == Physics2D.OverlapPoint(touchPos) && Input.GetTouch(i).phase == TouchPhase.Stationary || col2d == Physics2D.OverlapPoint(touchPos) && Input.GetTouch(i).phase == TouchPhase.Moved)
                            {

                                //Timer
                                secondsPressed += Time.deltaTime;
                                secondsPressed = secondsPressed % 60;

                                //stop movement
                                V_Units = 0f;


                                //-----------------------------------------------------------------------------------------------------
                                if (transform.position.y >= PoolManager.instance.UpperLimitBadHold || transform.position.y <= PoolManager.instance.DownerLimitBad) // Bad
                                {//Bad Hold Out of Position




                                    // if no finger assigned yet, assign one
                                    if (!wasFingAssigned)
                                    {
                                        myFingerID = Input.GetTouch(i).fingerId;
                                        wasFingAssigned = true;

                                        gameObject.GetComponent<Animator>().Play("HoldPressAnim");
                                    }

                                    //If is the same finger invocator
                                    if (Input.GetTouch(i).fingerId == myFingerID)
                                    {
                                      /*  PoolManager.instance.txt.text = "Bad Hold";
                                        ////Debug.Log("Bad Hold Out of Position");
                                        PoolManager.instance.txt.color = new Color(Random.value, Random.value, Random.value, 1.0f);
                                        */PoolManager.instance.ResetCombo();

                                         wasPressed = true;//new
                                        PoolManager.instance.sp.SpawnAudio(soundID);

                                        //DO BAD HOLD ANIMATION 
                                        gameObject.GetComponent<Animator>().enabled = false;
                                        gameObject.GetComponent<Animator>().enabled = true;
                                        gameObject.GetComponent<Animator>().Play("HoldPressBad");

                                        StartCoroutine(DisableGameObject());

                                        //DISABLED GAME OBJECT!!!!!! TRIGGER ANIMATION
                                        col2d.enabled = false;
                                        wasCol2dDisabled = true;
                                        ////transform.gameObject.SetActive(false);
                                    }

                                }

                                //Excellent Hold
                                else if (transform.position.y > PoolManager.instance.DownerLimitGood && transform.position.y < PoolManager.instance.UpperLimitGoodHold) //Excellent
                                {
                                    // if no finger assigned yet, assign one
                                    if (!wasFingAssigned)
                                    {
                                        myFingerID = Input.GetTouch(i).fingerId;
                                        wasFingAssigned = true;

                                        gameObject.GetComponent<Animator>().Play("HoldPressAnim");
                                    }


                                    if (secondsPressed >= holdSeconds)
                                    {
                                        /*PoolManager.instance.txt.text = "Excellent Hold";
                                        ////Debug.Log("Excellent Hold");
                                        PoolManager.instance.txt.color = new Color(Random.value, Random.value, Random.value, 1.0f);
                                       */ PoolManager.instance.AddToCombo(1);

                                        //DO EXCELLENT HOLD ANIMATION 
                                        gameObject.GetComponent<Animator>().enabled=false;
                                        gameObject.GetComponent<Animator>().enabled = true;
                                        gameObject.GetComponent<Animator>().Play("HoldPressExcellent");

                                        StartCoroutine(DisableGameObject());

                                        //DISABLED GAME OBJECT!!!!!! TRIGGER ANIMATION
                                        col2d.enabled = false;
                                        wasCol2dDisabled = true;
                                        ////transform.gameObject.SetActive(false);
                                    }


                                    if (!wasPressed) // Play Sound once
                                    {
                                        wasPressed = true;
                                        PoolManager.instance.sp.SpawnAudio(soundID);

                                        

                                    }
                                }


                                else //Good Hold
                                {



                                    // if no finger assigned yet, assign one
                                    if (!wasFingAssigned)
                                    {
                                        myFingerID = Input.GetTouch(i).fingerId;
                                        wasFingAssigned = true;

                                        //Play sound
                                        wasPressed = true;
                                        PoolManager.instance.sp.SpawnAudio(soundID);

                                        gameObject.GetComponent<Animator>().Play("HoldPressAnim"); 
                                    }



                                    if (secondsPressed >= holdSeconds)
                                    {
                                       /* PoolManager.instance.txt.text = "Good Hold";
                                        ////Debug.Log("Good Hold"); 
                                        PoolManager.instance.txt.color = new Color(Random.value, Random.value, Random.value, 1.0f);
                                        */PoolManager.instance.AddToCombo(1);

                                        //DO GOOD HOLD ANIMATION 

                                        gameObject.GetComponent<Animator>().enabled = false;
                                        gameObject.GetComponent<Animator>().enabled = true;
                                        gameObject.GetComponent<Animator>().Play("HoldPressGood");

                                        StartCoroutine(DisableGameObject());

                                        //DISABLED GAME OBJECT!!!!!! TRIGGER ANIMATION
                                        col2d.enabled = false;
                                        wasCol2dDisabled = true;
                                        ////transform.gameObject.SetActive(false);
                                    }


                                }
                                //-----------------------------------------------------------------------------------------------------
                            }

                        }
                      
                       
                    }
                }
            }





        }
    }
}