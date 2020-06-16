using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //The character control script And the gesture of the character.
    public Animator anim;
    public Rigidbody rbody;
    private float inputH;
    private float inputV;
    private bool Run;
    private bool Slide;
    public float rotSpeed = 80.0f;
    public float rot = 0.0f;

    void Start()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        Run = false;
    }


    void Update()
    {
        //gesture.
        if (Input.GetKeyDown("1"))
        {
            anim.Play("WAIT01", -1, 0f);
        }
        if (Input.GetKeyDown("2"))
        {
            anim.Play("WAIT02", -1, 0f);
        }
        if (Input.GetKeyDown("3"))
        {
            anim.Play("WAIT03", -1, 0f);
        }
        if (Input.GetKeyDown("4"))
        {
            anim.Play("WAIT04", -1, 0f);
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            Run = true;
        }
        else
        {
            Run = false;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }
        if (Input.GetKey(KeyCode.F))
        {
            anim.SetBool("Slide", true);
        }
        else
        {
            anim.SetBool("Slide", false);
        }
        
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);

        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);
        anim.SetBool("Run", Run);

        float moveX = inputH * 20f * Time.deltaTime;
        float moveZ = inputV * 50f * Time.deltaTime;

        if(moveZ <= 0f)
        {
            moveX = 0f;
        }
        else if(Run)
        {
            moveX *= 3f;
            moveZ *= 3f;
        }
        if (Slide)
        {
            moveX *= 20f;
            moveZ *= 20f;
        }

        rbody.velocity = new Vector3(moveX, 0f, moveZ);

    }
}
