using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private bool _canJump;
    private bool _isOnGround;
    private CapsuleCollider _capsuleCollider;
    private Vector3 _jumpHeight = new Vector3(0, 10, 0);

    void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _isOnGround = false;
        _canJump = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_canJump)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            {
                _canJump = false;
                transform.position += _jumpHeight * (10 * Time.deltaTime);
            }
        }
        Debug.Log(_isOnGround);
        
    }
    
    
    private void OnCollisionEnter(Collision other)
    {
        GameObject temp = other.gameObject;
        if (temp.CompareTag("Platform"))
        {
            _isOnGround = true;
            _canJump = true;
        }
    }


}
