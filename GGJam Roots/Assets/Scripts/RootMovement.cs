using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RootMovement : MonoBehaviour
{
    public PlayerController PC;
    private Rigidbody2D rb;
    private Collider2D col;
    private Renderer rend;

    public string typeRoot;
    public Color highlight, defMaterial, selected;
    private bool rootSelected, isStarter;
    [SerializeField] private int minRoots;
    

    private void Start()
    {
        col = GetComponent<PolygonCollider2D>();
        PC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody2D>();
        isStarter = true;
    }

    private void Update()
    {
        minRoots = (Mathf.Abs(PC.rootSelecteds / 2));
    }

    public void SetMass()
    {
        rb.gravityScale = 1f;
        isStarter = false;
    }

    private void OnMouseEnter()
    {
        if(rootSelected == true || isStarter == true) return;

        rend.material.color = highlight;
    }

    private void OnMouseExit()
    {
        if(rootSelected == true || isStarter == true) return;
        
        rend.material.color = defMaterial;
    }

    private void OnMouseDown()
    {
        if(isStarter == true) return;
        
        if (rootSelected == false && minRoots > PC.rootsEliminated)
        {
            PC.EliminatedRoots(1, typeRoot);
            rend.material.color = selected;
            rootSelected = true;
            rb.gravityScale = 0f;
            col.isTrigger = true;
        }

        else if(rootSelected == true)
        {
            PC.EliminatedRoots(-1, typeRoot);
            rend.material.color = defMaterial;
            rootSelected = false;
            rb.gravityScale = 1f;
            col.isTrigger = false;
        }
        
    }
    
    
    
}
