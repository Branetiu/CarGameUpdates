using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveplayer : MonoBehaviour
{
    ///public KeyCode moveLeft;
    ////public KeyCode moveRight;
  
    public float horizVel = 0;
    public int laneNum = 2;
    public string controllocked = "n";
    public Transform BoomObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(horizVel, trackstatic.vertVel, 4);
     
       
        TouchMove();



    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Lethal")
        {
            Destroy(this.gameObject);
            trackstatic.zVelAdjustment = 0;
            Instantiate(BoomObj, transform.position, BoomObj.rotation);
            trackstatic.gameCompStatus = "You Failed";
          
        }
        if (other.gameObject.name == "Capsule")
        {
            Destroy(other.gameObject);
        }


    }


void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.name == "Coins")
        {
            Destroy(other.gameObject);
            trackstatic.coinsTotal += 1;
        }
        if (other.gameObject.name == "Exit")
        {
            if (trackstatic.coinsTotal <= 3)   //If total coins is less or equal with 3 do this
            {
                
                trackstatic.gameCompStatus = "You Failed";
                SceneManager.LoadScene("CompleteLevel");
            }
            else  // else if its bigger show success and move to complete level
            {
               trackstatic.gameCompStatus = "Success!";
                SceneManager.LoadScene("CompleteLevel");
            }
        }
        if (other.gameObject.name == "Exit2")  //same situation for Level2
        {
            if (trackstatic.coinsTotal <= 3) // if total coins is smallar than 3 u fail)
            {

                trackstatic.gameCompStatus = "You Failed";
                SceneManager.LoadScene("CompleteLevel");
            }
            else
            {
                trackstatic.gameCompStatus = "Success! ";
                SceneManager.LoadScene("CompleteLevel");
            }
        }

    }

    void TouchMove()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float middle = Screen.width/2;

            if (touch.position.x < middle && touch.phase == TouchPhase.Began && (laneNum > 1) && (controllocked == "n"))
            {
                MoveLeft();
               
            }
            else if (touch.position.x > middle && touch.phase == TouchPhase.Began && (laneNum < 3) && (controllocked == "n"))
            {
                MoveRight();
                
            }

        }
      
    }
    public void MoveLeft()
    {
        
       horizVel = -2;
        StartCoroutine(stopSlide());
        laneNum -= 1;
        controllocked = "y";

    }
    public void MoveRight()
    {
       
        horizVel = 2;
        StartCoroutine(stopSlide());
        laneNum += 1;
      controllocked = "y";
    }
 



    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(.5f);
        horizVel = 0;
        controllocked = "n";
    }



}
