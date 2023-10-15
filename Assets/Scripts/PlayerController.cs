using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.TextCore.Text;
using Vector2 = System.Numerics.Vector2;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    //private bool _isOnGround;
    private Rigidbody rb;
    [SerializeField] private float jumpHeight = 0f;
    [SerializeField] private float lowJump = 2.5f;
    [SerializeField] private float gravityScale = 2f;
    private float _velocity;
    [SerializeField] private LayerMask layersToHit;
    private CharacterController _characterController;

    //private Ray _ray;
    
    
    private GameController _gc; //GameController reference
    private Collider _lastTouchedPlatform; //Using to check for the last platform touched
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        //_isOnGround = false;
        
        
        //Initializing _gc and _lastTouchedPlatform
        _gc = FindObjectOfType<GameController>();
        _lastTouchedPlatform = null;
    }

    // Update is called once per frame
    private void Update()
    {
        //CheckForCollision();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = Vector3.up * jumpHeight;
        }
        switch (rb.velocity.y)
        {
            case < 0:
                rb.velocity += (Physics.gravity.y * (gravityScale - 1) * Time.deltaTime) * Vector3.up;
                break;
            case > 0 when !Input.GetKey(KeyCode.Space) || !Input.GetKey(KeyCode.UpArrow):
                rb.velocity += (Physics.gravity.y * (lowJump - 1) * Time.deltaTime) * Vector3.up;
                break;
        }


        /*_velocity += Physics.gravity.y * gravityScale * Time.deltaTime;
        //CheckForCollision();
        if (_isOnGround)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("jump");
                // _velocity = Mathf.Sqrt(jumpHeight * -2 * (Physics.gravity.y * gravityScale));
                _velocity = Mathf.Sqrt(jumpHeight * 2);
            }
        }
        transform.Translate(new Vector3(0, _velocity, 0) * Time.deltaTime);*/
    }

    private void CheckForCollision()
    {
        //_ray = new Ray(transform.position, -transform.up);
        Vector3 p1 = transform.position + _characterController.center +
                     -Vector3.up * (-_characterController.height * 0.5f);
        Vector3 p2 = p1 + -Vector3.up * _characterController.height;
        if (Physics.CapsuleCast(p1, p2, _characterController.radius, 
                -transform.up, out RaycastHit hit, Mathf.Infinity, layersToHit))
        {
            if (hit.collider.gameObject.CompareTag("Platform"))
            {
                Debug.Log("HIT");
               /* _velocity = 0;
                Vector3 surface = Physics.ClosestPoint(hit.collider.gameObject.transform.position, hit.collider,
                    transform.position, transform.rotation); //+ Vector3.up * hit.collider.transform.position.y
                
                //Currently this is the best way I've gotten the floor snapping to work.
                //Still ends up stuck in the ground sometimes.
                transform.position = new Vector3(transform.position.x, surface.y + 0.501f, transform.position.z);*/
                //_isOnGround = true;
            }
        }
        else
        {
            //_isOnGround = false;
        }
    }

    //checks if player has collided with a platform
    //and only increases score when landing on a new platform
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Platform") && collision != _lastTouchedPlatform)
        {
            _gc.IncrementScore();
            _lastTouchedPlatform = collision;
        }
    }
}
