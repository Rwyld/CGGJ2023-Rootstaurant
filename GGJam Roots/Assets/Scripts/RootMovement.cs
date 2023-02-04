using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    public void SetMass()
    {
        rb.gravityScale = 1f;
        
    }


}
