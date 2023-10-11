using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using Vector2 = System.Numerics.Vector2;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _isOnGround;
    private CapsuleCollider _capsuleCollider;

    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float gravityScale = 10f;
    private float _velocity;
    [SerializeField] private LayerMask layersToHit;

    private Ray _ray;
    
    
    private void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _isOnGround = false;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        _velocity += Physics.gravity.y * gravityScale * Time.deltaTime;
        CheckForCollision();
        if (_isOnGround)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("jump");
                // _velocity = Mathf.Sqrt(jumpHeight * -2 * (Physics.gravity.y * gravityScale));
                _velocity = Mathf.Sqrt(jumpHeight * 2);
            }
        }
        transform.Translate(new Vector3(0, _velocity, 0) * Time.deltaTime);
        
    }

    private void CheckForCollision()
    {
        _ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(_ray, out RaycastHit hit, layersToHit))
        {
            if (hit.collider.gameObject.CompareTag("Platform"))
            {
                Debug.Log("HIT");
                _velocity = 0;
                Vector3 surface = Physics.ClosestPoint(hit.collider.gameObject.transform.position, hit.collider,
                    transform.position, transform.rotation); //+ Vector3.up * hit.collider.transform.position.y
                
                //Currently this is the best way I've gotten the floor snapping to work.
                //Still ends up stuck in the ground sometimes.
                transform.position = new Vector3(transform.position.x, surface.y + 0.501f, transform.position.z);
                _isOnGround = true;
            }
        }
        else
        {
            // _isOnGround = false;
        }
    }
}
