using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Press Z to control/uncontrol the mighty beast
 * 
 * 
 */

//Make Pod
//Player -Pod Interact
//Control creature
//Creature control release- Z
//Missiles - Cool things
public enum GameMode
{
    NORMAL,
    FPS
}


public class PodController : MonoBehaviour
{

    //GameMode
    [SerializeField]
    private GameMode currentMode;

    private bool autocontrol = false;

    //Pod Components
    [SerializeField]
    private Collider myCollider;

    private Transform myTransform;

    private bool insidePod = false;

    [SerializeField]
    private Rigidbody myRigidbody;
  

    //Temp
    public GameObject otherTarget;

    //Player
    private GameObject playerGO;

    private string playerTag;
    [SerializeField]
    private Seek playerSeekController;

    [SerializeField]
    private FPSController playerFPSController;

    [SerializeField]
    private Boid playerBoid;

   
    //Camera
    [SerializeField]
    private GameObject FPSCam;
    [SerializeField]
    private GameObject mainCamera;

    //Cool things
    [SerializeField]
    private int playerHP;

    private void OnEnable()
    {
        playerTag = "Player11";
        currentMode = GameMode.FPS;
        FPSCam = GameObject.FindGameObjectWithTag("FPSCam");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        playerGO = GameObject.FindGameObjectWithTag(playerTag);
    }
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponentInChildren<Collider>();
        myRigidbody = GetComponent<Rigidbody>();

        otherTarget = GameObject.FindGameObjectWithTag(playerTag);
        playerSeekController = otherTarget.GetComponent<Seek>();
       

        myTransform = this.gameObject.transform;

        playerFPSController = GameObject.FindGameObjectWithTag(playerTag).GetComponent<FPSController>();
        playerBoid = GameObject.FindGameObjectWithTag(playerTag).GetComponent<Boid>();

        playerHP = 100;

    }

    // Update is called once per frame
    void Update()
    {
        automaticControl();
        if (insidePod == true)
        {
            Debug.Log("moving and forwarding");
            moveToPodCenter();
            faceForwardMovement();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag)
        {
        
            playerBoid.enabled = false;
            insidePod = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == playerTag)
        {
            playerBoid.enabled = true;
            insidePod = false;
        }
    }

    private float rotateSpeed = 45;
    private float moveToCenterSpeed = 10;

    public void givePlayerDamage()
    {
        playerHP -= 10;
        Debug.Log(playerHP + " now");
        if(playerHP <= 0)
        {
            Destroy(this, 5);
        }
    }

    public void givePlayerHealth()
    {
        playerHP += 10;
        Debug.Log(playerHP + " now");
        
    }
    private void moveToPodCenter()
    {
        Transform playerTransform;
        playerTransform = playerGO.transform;
        Vector3 moveTo = myTransform.position * (moveToCenterSpeed * Time.deltaTime);
    }

   
    private void faceForwardMovement()
    {
        playerGO.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    private void switchCamera()
    {
        if(currentMode == GameMode.FPS)
        {
            FPSCam.GetComponent<Camera>().enabled = true;
            mainCamera.GetComponent<Camera>().enabled = false;
        }
        else 
        {
            mainCamera.GetComponent<Camera>().enabled = true;
            FPSCam.GetComponent<Camera>().enabled = false;
        }
    }


    private void automaticControl()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            
            playerFPSController.enabled = true;
            playerBoid.enabled = !playerBoid.enabled;
            if(autocontrol == true) 
                currentMode = GameMode.FPS;
            else
                currentMode = GameMode.NORMAL;
            switchCamera();
            autocontrol = !autocontrol;
        }
    }
}
