using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public float Speed;
    public TextMeshProUGUI counttext;
    public GameObject WinTextObject;
    public GameObject Clone;


    private Rigidbody rb;
    private int count;

    private float InputX;
    private float InputY;
    private Vector3 oldPosition;

    public static List<Vector3> PlayerPositions = new List<Vector3>();

    private static GameObject thePlayer;
    public static GameObject GetPlayer() 
    {
        return thePlayer;
    }







    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        NumberOfFixedSteps = 1;

        SetCountText();
        WinTextObject.SetActive(false);

        thePlayer = gameObject;
        oldPosition = transform.position;
    }

    private void OnMove(InputValue MovementValue)
    {
        Vector2 MovementVector = MovementValue.Get<Vector2>();
        InputX = MovementVector.x;
        InputY = MovementVector.y;

    }

    void SetCountText()
    {
        counttext.text = "Count: " + count.ToString();
        if(GameObject.FindGameObjectWithTag("Pickup") == null)
        {
            WinTextObject.SetActive(true);
        }

        

    }



    private int NumberOfFixedSteps;
 
    private void FixedUpdate()
    {
        PlayerPositions.Add(rb.transform.position);

        float playerSpeed = Vector3.Distance(transform.position, oldPosition) / Time.deltaTime;
        Vector3 Movement = InputX != 0 || InputY != 0 ? new Vector3(InputX, 0.0f, InputY).normalized : -Mathf.Min(playerSpeed/10,1)*((transform.position - oldPosition).normalized) ;
        Vector3 ResistanceForce = Movement;
        rb.AddForce(Movement * Speed - ResistanceForce * playerSpeed);
        
        if(NumberOfFixedSteps % 500 == 0)
        {
            Object.Instantiate(Clone);
        }

        oldPosition = transform.position;
        NumberOfFixedSteps++;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();

        }


    if(other.gameObject.CompareTag("Clone"))
        {
            other.gameObject.SetActive(false);
        }

    }



    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Clone"))
        {
            this.gameObject.SetActive(false);
        }
    }










}












/*
public class Death
{
    private int timeOfDeath;
    private static int numberOfDeathObjects;

    public static void nothing() {
        var x = numberOfDeathObjects
    }

    public void movey() {
    
    }



}


*/