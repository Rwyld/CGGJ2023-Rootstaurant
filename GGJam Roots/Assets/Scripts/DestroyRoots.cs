using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRoots : MonoBehaviour
{
    public bool isMovable;
    public bool isCookpot;
    private float speed = 5f;


    private void Update()
    {
        Move();
    }
    
    
    private void Move()
    {
        if (isMovable)
        {
            transform.position += Vector3.down * Time.deltaTime * speed;

            if (transform.position.y < 0)
            {
                transform.position = new Vector2(16.5f, 4.5f);
            }
        }
    }

    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Root"))
        {
            Destroy(col.gameObject);
        }
    }
    
    
}
