﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombieswalk : MonoBehaviour
{
    public Transform goal;
    public NavMeshAgent agent;
    private Animator anim;
    private Vector3 pos;
    public UI ui;
    public Chancontroller chancontroller;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = goal.position;
        //if (transform.position != goal.position)
        if (Vector3.Distance(transform.position, goal.position) > 1.5f)
        {
            anim.SetInteger("Condition", 1);
        }
        else
        {
            anim.SetInteger("Condition", 2);
            anim.SetInteger("Condition", 0);
            //Destroy(this.gameObject);
            Destroy(this.gameObject);
            ui.Attackplayer();
            chancontroller.hit();
        }
    }
}
