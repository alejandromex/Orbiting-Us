using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotMove : MonoBehaviour
{
    private Rigidbody _rb;
    public float moveSpeed = 5f, rotateSpeed = 30f;
    public float hInput, vInput;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;
        if(Mathf.Abs(Input.GetAxisRaw("Vertical")) > .2f)
        {
            _animator.SetBool("walk", true);
        }
        else
        {
            _animator.SetBool("walk", false);
            vInput = 0;
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(this.transform.position +
                       this.transform.forward *
                      vInput * Time.fixedDeltaTime);

        //rotacion del robot
        Vector3 rotation = Vector3.up * hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);
    }
}
