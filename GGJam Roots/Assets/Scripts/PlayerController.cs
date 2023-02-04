using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager GM;
    public RootsController RC;
    public int greenPoints;
    public int redPoints;
    
    public Transform newRootPos, stackPos;
    public GameObject acceptButtom, declineButtom, cookingButtom, shopButtom, nextDayButtom;

    private string type;
    public int rootQuantity;
    [SerializeField] private GameObject currentRoot;
    [SerializeField] private GameObject root;
    

    private void Start()
    {
        greenPoints = 0;
        redPoints = 0;
        rootQuantity = GM.numberRoots;

    }

    public void GenerateRoot()
    {
        currentRoot = Instantiate(root, newRootPos.position, Quaternion.identity);
    }

    public void NextDay()
    {
        acceptButtom.SetActive(true);
        declineButtom.SetActive(true);
        rootQuantity = GM.numberRoots;
        RC.NewRoot();
        GenerateRoot();
    }

    public void SetRoot(RootsBlueprints rb)
    {
        root = rb.rootPrefab;
        type = rb.rootType;
    }

    public void AcceptRoot()
    {
        switch (type)
        {
            case "Healthy":
                
                Instantiate(root, stackPos.position, Quaternion.identity);
                greenPoints += 1;
                rootQuantity--;
                Destroy(currentRoot);
                RC.NewRoot();
                GenerateRoot();
                break;
            
            case "Badly":
                
                Instantiate(root, stackPos.position, Quaternion.identity);
                redPoints += 1;
                rootQuantity--;
                Destroy(currentRoot);
                RC.NewRoot();
                GenerateRoot();
                break;
        }
    }

    public void DeclineRoot()
    {
        rootQuantity--;
        Destroy(currentRoot);
        RC.NewRoot();
        GenerateRoot();
    }

    public void CookingProcces()
    {
        rootQuantity -= 1;
        GM.CookingTime();
        cookingButtom.SetActive(false);
        shopButtom.SetActive(true);
    }

    public void ResultProcces()
    {
        GM.ResultTime();
        shopButtom.SetActive(false);
        nextDayButtom.SetActive(true);
    }
    
    public void NextDayProcces()
    {
        Destroy(currentRoot);
        nextDayButtom.SetActive(false);
        GM.NewDay();
    }

    private void Update()
    {
        if (rootQuantity != 0) return;

        Destroy(currentRoot);
        acceptButtom.SetActive(false);
        declineButtom.SetActive(false);
        cookingButtom.SetActive(true);
    }
}
