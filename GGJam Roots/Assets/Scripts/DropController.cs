using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{
    public float speed = 2f;
    public float timer;

    private void Movement()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            speed *= -1;
            timer = 5f;
        }

        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void Start()
    {
        timer = 5f;
    }

    private void Update()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        RootMovement RT = col.GetComponent<RootMovement>();

        if (col != null)
        {
            RT.SetMass();
        }
    }
    
}
