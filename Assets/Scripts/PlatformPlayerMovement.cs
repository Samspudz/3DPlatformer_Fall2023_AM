using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlatformPlayerMovement : MonoBehaviour
{
    //---- Move Player ----
    public float moveSpeed = 5;
    public float jumpForce = 12;
    public float turnSmoothing = 0.2f;
    public float zoomedTurnSpeed = 15;
    private float turnSmoothVelocity;

    public Vector3 pGravity; //control player gravity
    public Vector2 moveInput;

    //---- Animate Player ----
    Animator anim;

    //---- Determine Collisions ----
    [SerializeField] Rigidbody rBody;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Collider[] groundCol;
    public Transform groundCheck;
    public LayerMask thisIsGround;

    //---- Play Mode ----
    public bool isZoomed;
    public GameObject _player;
    public Cinemachine.AxisState yAxis;
    public Transform camLookAt;

    [SerializeField] Transform cam;


    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        anim = GetComponentInChildren<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        groundCol = Physics.OverlapSphere(groundCheck.position, 0.2f, thisIsGround);
        Physics.gravity = pGravity;

        //handle Air Animations
        anim.SetFloat("VSpeed", rBody.velocity.y, 0.3f, Time.deltaTime);
        anim.SetBool("Grounded", isGrounded);

        //handle Zoomed in Animation
        anim.SetFloat("XSpeed", moveInput.x);
        anim.SetFloat("ZSpeed", moveInput.y);
        anim.SetBool("Zoomed", isZoomed);

        if (groundCol.Length > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            PlayerJump();
        }

        //---- Shoot Mode ----
        if (Input.GetButton("Fire2"))
        {
            isZoomed = true;
        }
        else
        {
            isZoomed = false;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            float zoomTurnAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, 0.04f);
            transform.rotation = Quaternion.Euler(0, cam.eulerAngles.y, 0);
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        anim.SetFloat("Speed", moveDirection.magnitude);

        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothing);
            if (!isZoomed)
            {
                transform.rotation = Quaternion.Euler(0, _angle, 0);
            }
            Vector3 _moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            rBody.MovePosition(transform.position + _moveDir * moveSpeed * Time.deltaTime);
        }

        if (isZoomed)
        {
            yAxis.Update(Time.fixedDeltaTime);
            camLookAt.localEulerAngles = new Vector3(yAxis.Value, 0, 0);
            
            float turnY = Input.GetAxis("Mouse X") * zoomedTurnSpeed;
            _player.transform.localEulerAngles = new Vector3(0, _player.transform.localEulerAngles.y + turnY, 0);
        }
    }

    void PlayerJump()
    {
        rBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
