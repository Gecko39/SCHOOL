using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;



public class control_ball : MonoBehaviour
{

    // Rigidbody of the player.
    private Rigidbody rb;
    // Barrel counter
    private int count;
    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0;

    public TextMeshProUGUI countText;

    public GameObject EndTextObject;

    public GameObject pickupSFX;
    public GameObject winSFX;
    public GameObject loseSFX;
    public GameObject Soundtrack;
    public GameObject wallSFX;
    public GameObject boxSFX;
    public GameObject collect;
    public GameObject WIN1;
    public GameObject explo1;
    public GameObject explo2;

    private Vector3 targetPos;
    [SerializeField] private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        EndTextObject.SetActive(false);
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    void SetCountText()
    {
        countText.text = "Collected: " + count.ToString();
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);

        if (Input.GetMouseButton(0)) // Check if left mouse button is held down
        {
              Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
              Debug.DrawRay(ray.origin, ray.direction * 50, Color.yellow);
              RaycastHit hit; // Define variable to hold raycast hit information

              // Check if raycast hits an object
              if (Physics.Raycast(ray, out hit)) 

                  if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                  {  
                     targetPos = hit.point; // Set target position
                     isMoving = true; // Start player movement
                  }
              else
              {
                   isMoving = false; // Stop player movement
              }
        }
         
        if (isMoving)
        {
              // Move the player towards the target position
              Vector3 direction = targetPos - rb.position;
              direction.Normalize();
              rb.AddForce(direction * speed);
        }

        if (Vector3.Distance(rb.position, targetPos) < 0.5f)
        {
         isMoving = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            if (count < 3) 
            { 
                pickupSFX.GetComponent<AudioSource>().Play();
                var currentPickupFX = Instantiate(collect, other.transform.position, Quaternion.Euler(-90, 0, 0));
            }
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();

            if (count == 4)
            {
                var currentPickupFX = Instantiate(WIN1, other.transform.position, Quaternion.Euler(-90, 0, 0));
                EndTextObject.SetActive(true);
                Destroy(GameObject.FindGameObjectWithTag("Enemy"));
                Soundtrack.GetComponent<AudioSource>().Stop();
                winSFX.GetComponent<AudioSource>().Play();

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            wallSFX.GetComponent<AudioSource>().Play();
        }
        if (collision.gameObject.CompareTag("Box"))
        {
            boxSFX.GetComponent<AudioSource>().Play();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            gameObject.SetActive(false);
            // Update the winText to display "You Lost..."
            EndTextObject.gameObject.SetActive(true);
            EndTextObject.GetComponent<TextMeshProUGUI>().text = "You Lost...";
            Instantiate(explo1, transform.position, Quaternion.Euler(-90, 0, 0));
            Instantiate(explo2, transform.position, Quaternion.Euler(-90, 0, 0));
            Soundtrack.GetComponent<AudioSource>().Stop();
            loseSFX.GetComponent<AudioSource>().Play();
        }
        if (collision.gameObject.CompareTag("Bar"))
        {
            // Destroy the current object
            gameObject.SetActive(false);
            // Update the winText to display "You Lost..."
            EndTextObject.gameObject.SetActive(true);
            EndTextObject.GetComponent<TextMeshProUGUI>().text = "You Fell...";
            Soundtrack.GetComponent<AudioSource>().Stop();
            loseSFX.GetComponent<AudioSource>().Play();
        }
    } 
}