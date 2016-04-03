using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class SixDegreesPlayerController : MonoBehaviour {

    private float horizTranslation;
    private float forwardTranslation;
    private Vector3 horizVelocity;
    private Vector3 forwardVelocity;
    private Vector3 acceleration;
    private Vector3 rotation;
    private float cameraRotationX;

    [SerializeField]
    private Camera cam;

    private float currentCameraRotationX = 0f;

    private Rigidbody rb;

    public float speed = 1f;
    public float maxSpeed = 5f;
    public float mouseSensitivity = 3f;
    public float rollSpeed = 1f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        translate();

        translateVertical();

        rotate();
        
    }

    private void translate()
    {

        horizTranslation = Input.GetAxisRaw("Horizontal");
        forwardTranslation = Input.GetAxisRaw("Vertical");

        horizVelocity = transform.right * horizTranslation;
        forwardVelocity = transform.forward * forwardTranslation;

       
            //horizVelocity = new Vector3(Mathf.Cos(currentCameraRotationX) * (transform.right.x * horizTranslation), Mathf.Sin(currentCameraRotationX) * transform.right.x * horizTranslation, transform.right.z * horizTranslation);
      
            //forwardVelocity = new Vector3(Mathf.Cos(currentCameraRotationX) * (transform.forward.x * forwardTranslation), Mathf.Cos(currentCameraRotationX) * transform.forward.y * forwardTranslation, transform.forward.z * forwardTranslation);
        
        

        //print("forward: " + transform.forward);
        print("camera: " + currentCameraRotationX);//use this

        acceleration = (horizVelocity + forwardVelocity);
        if(acceleration != Vector3.zero)
        {
            acceleration.y = Mathf.Sin(currentCameraRotationX);
        }
        
        acceleration = acceleration.normalized * speed;


        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        rb.AddForce(acceleration);

        
    }

    private void translateVertical()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.up * speed);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            rb.AddForce(transform.up * -speed);
        }
    }

    private void rotate()
    {
        float yRot = Input.GetAxisRaw("Mouse X");
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * mouseSensitivity;

        //rb.AddTorque(rotation);

        transform.Rotate(rotation);

        cameraRotationX = xRot * mouseSensitivity;

        currentCameraRotationX -= cameraRotationX;
        if (currentCameraRotationX < -360)
        {
            currentCameraRotationX = currentCameraRotationX % -360f;
        }
        else if (currentCameraRotationX > 360)
        {

        }

        if (cam != null)
        {
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0, 0);
        }

        //TODO works, but kinda funky effects on look rotation
        if(Input.GetKey(KeyCode.Q))
        {
            
            //rb.AddTorque(transform.forward * rollSpeed);
            transform.Rotate(new Vector3(0f, 0f, rollSpeed));
        }
        if (Input.GetKey(KeyCode.E))
        {
            
            //rb.AddTorque(transform.forward * -rollSpeed);
            //rb.MoveRotation(Quaternion.Euler(new Vector3(0f, 0f, -mouseSensitivity)));
            transform.Rotate(new Vector3(0f, 0f, -rollSpeed));
        }
    }
 
}
