using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float MoveSpeed;
    public float RunSpeed;
    private float applySpeed;

    public float JumpForce;

    private bool isGround = true;

    private CapsuleCollider capsuleCollider;
    private Rigidbody Myrigid;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        Myrigid = GetComponent<Rigidbody>();
        applySpeed = MoveSpeed;
    }

    void Update()
    {
        Running();
        IsGround();
        Jump();
        Moving();
    }


   
    private void Running()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            applySpeed = RunSpeed;
        }
        else 
        {
            applySpeed = MoveSpeed;
        }
    }
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }
    
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Myrigid.velocity = transform.up * JumpForce;
        }
    }
    private void Moving()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate((Vector3.forward).normalized * applySpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate((Vector3.back).normalized * applySpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate((Vector3.left).normalized * applySpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate((Vector3.right).normalized * applySpeed * Time.deltaTime);
        }
    }
}
