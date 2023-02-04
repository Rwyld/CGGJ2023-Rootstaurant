using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameManager GM;
    public RootsController RC;
    public int greenPoints;
    public int redPoints;
    
    public Transform newRootPos, stackPos;
    public GameObject releaseRoots, destroyRoots;
    public GameObject acceptButtom, declineButtom, confirmButtom, cookingButtom, shopButtom, nextDayButtom;
    public Button confirmBt;

    private string type;
    public int rootQuantity, rootSelecteds, minSelecteds, rootsEliminated;
    [SerializeField] private GameObject currentRoot;
    [SerializeField] private GameObject root;

    public GameObject[] results;
    public TextMeshProUGUI resultText;
    private float porcent;

    private void Start()
    {
        greenPoints = 0;
        redPoints = 0;
        rootQuantity = GM.numberRoots;

    }
    private void Update()
    {
        if (rootQuantity != 0) return;

        Destroy(currentRoot);
        acceptButtom.SetActive(false);
        declineButtom.SetActive(false);
        cookingButtom.SetActive(true);
    }
    

    public void GenerateRoot()
    {
        currentRoot = Instantiate(root, newRootPos.position, Quaternion.identity);
    }

    public void NextDay()
    {
        greenPoints = 0;
        redPoints = 0;
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
                rootSelecteds++;
                Destroy(currentRoot);
                RC.NewRoot();
                GenerateRoot();
                break;
            
            case "Badly":
                
                Instantiate(root, stackPos.position, Quaternion.identity);
                redPoints += 1;
                rootQuantity--;
                rootSelecteds++;
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
        cookingButtom.SetActive(false);
        rootQuantity -= 1;
        minSelecteds = Mathf.Abs(rootSelecteds / 2);
        GM.CookingTime();
        StartCoroutine(Cooking());

    }

    public void ConfirmProcces()
    {
        StartCoroutine(CookingAnimations());
    }

    public void ResultProcces()
    {
        GM.ResultTime();
        shopButtom.SetActive(false);
        StartCoroutine(ResultAnimations());
    }
    
    public void NextDayProcces()
    {
        foreach (var result in results)
        {
            result.SetActive(false);
        }

        resultText.text = " ";
        
        Destroy(currentRoot);
        nextDayButtom.SetActive(false);
        GM.NewDay();
    }

    public void EliminatedRoots(int quantity, string typeRoot)
    {
        rootsEliminated += quantity;

        if (typeRoot == "Healthy" && quantity > 0)
        {
            greenPoints -= 1;
        }
        else if (typeRoot == "Badly" && quantity > 0)
        {
            redPoints -= 1;
        }
        else if (typeRoot == "Healthy" && quantity < 0)
        {
            greenPoints += 1;
        }
        else if (typeRoot == "Badly" && quantity < 0)
        {
            redPoints += 1;
        }
    }
    
    private IEnumerator Cooking()
    {
        yield return new WaitForSeconds(2f);
        
        if (rootsEliminated < minSelecteds)
        {
            confirmButtom.SetActive(true);
            confirmBt.interactable = true;
        }
        else if (rootsEliminated > minSelecteds)
        {
            confirmBt.interactable = false;
        }
    }

    private IEnumerator CookingAnimations()
    {
        releaseRoots.SetActive(false);
        destroyRoots.SetActive(true);
        confirmButtom.SetActive(false);
        yield return new WaitForSeconds(2f);
        shopButtom.SetActive(true);
        releaseRoots.SetActive(true);
        destroyRoots.SetActive(false);
    }

    private void Result()
    {
        if (porcent > 90)
        {
            results[0].SetActive(true);
        }
        if (porcent > 70 &&  porcent <= 90)
        {
            results[1].SetActive(true);
            GM.GameLifes(1);
        }
        if (porcent > 40 &&  porcent <= 70)
        {
            results[2].SetActive(true);
            GM.GameLifes(2);
        }
        if (porcent > 0 &&  porcent <= 40)
        {
            results[3].SetActive(true);
            GM.GameLifes(4);
        }
        
    }

    private IEnumerator ResultAnimations()
    {
        porcent = (greenPoints / (greenPoints + redPoints)) * 100f;
        var value = 0;
        while (value <= porcent)
        {
            value++;
            resultText.text = value + "%";
        }
        
        Result();
        yield return new WaitForSeconds(3f);
        nextDayButtom.SetActive(true);
        
    }
    
}
