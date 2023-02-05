using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameManager GM;
    public RootsController RC;
    public float greenPoints;
    public float redPoints;
    
    public Transform newRootPos, stackPos, resultPos;
    public GameObject backTomorrow, nextRootBase, releaseRoots, destroyRoots, cuttingTable;
    public GameObject acceptButtom, declineButtom, confirmButtom, cookingButtom, shopButtom, nextDayButtom;
    public Button acceptBt, declineBt, confirmBt;
    public Animator anin_accept, anin_decline, anin_confirm, anin_cooking, anin_shop, anin_next;
    public GameObject GreenLights, RedLights;

    private string type;
    public int rootQuantity, rootSelecteds, minSelecteds, rootsEliminated;
    [SerializeField] private GameObject currentRoot;
    [SerializeField] private GameObject root;
    private GameObject advice;

    public GameObject[] results;
    public TextMeshProUGUI resultText;
    public Animator anin_resultText;
    [SerializeField] private float porcent;

    private void Start()
    {
        greenPoints = 0;
        redPoints = 0;
        rootQuantity = GM.numberRoots;

    }
    private void Update()
    {
        if (rootQuantity != 0) return;
        
        NextStepCooking();
    }

    public void SetRoot(RootsBlueprints rb)
    {
        root = rb.rootPrefab;
        type = rb.rootType;
    }
    public IEnumerator GenerateRoot()
    {
        acceptBt.interactable = false;
        declineBt.interactable = false;
        yield return new WaitForSeconds(2f);
        
        nextRootBase.SetActive(true);
        currentRoot = Instantiate(root, newRootPos.position, Quaternion.identity);
        currentRoot.transform.localScale *= new Vector2(2, 2);
        yield return new WaitForSeconds(2f);
        
        acceptBt.interactable = true;
        declineBt.interactable = true;
    }
    
    
    private void NextStepCooking()
    {
        Destroy(currentRoot, 2.5f);
        nextRootBase.SetActive(false);
        StartCoroutine(AnimationsButtoms(anin_decline));
        StartCoroutine(AnimationsButtoms(anin_accept));

        root = backTomorrow;
        StartCoroutine(GenerateRoot());
        cookingButtom.SetActive(true);
        rootQuantity -= 1;
    }
    
    public void NextDay()
    {
        greenPoints = 0;
        redPoints = 0;
        minSelecteds = 0;
        rootSelecteds = 0;
        rootsEliminated = 0;
        acceptButtom.SetActive(true);
        declineButtom.SetActive(true);
        rootQuantity = GM.numberRoots;
        RC.NewRoot();
        StartCoroutine(GenerateRoot());
    }
    

    private IEnumerator AnimationsButtoms(Animator bt)
    {
        bt.SetBool("Start", true);
        yield return new WaitForSeconds(1.5f);
    }

    private IEnumerator GreenLightsButtons()
    {
        GreenLights.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        GreenLights.SetActive(false);

    }
    
    private IEnumerator RedLightsButtons()
    {
        RedLights.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        RedLights.SetActive(false);

    }
    
    public void AcceptRoot()
    {
        StartCoroutine(GreenLightsButtons());
        nextRootBase.SetActive(false);
        
        switch (type)
        {
            case "Healthy":
                
                Instantiate(root, stackPos.position, Quaternion.identity);
                greenPoints += 1;
                rootQuantity--;
                rootSelecteds++;
                Destroy(currentRoot, 2f);
                if (rootQuantity == 0) break;
                RC.NewRoot();
                StartCoroutine(GenerateRoot());
                break;
            
            case "Badly":
                
                Instantiate(root, stackPos.position, Quaternion.identity);
                redPoints += 1;
                rootQuantity--;
                rootSelecteds++;
                Destroy(currentRoot, 2f);
                if (rootQuantity == 0) break;
                RC.NewRoot();
                StartCoroutine(GenerateRoot());
                break;
        }
    }

    public void DeclineRoot()
    {
        StartCoroutine(RedLightsButtons());
        nextRootBase.SetActive(false);
        rootQuantity--;
        Destroy(currentRoot, 2f);
        if (rootQuantity == 0) return;
        RC.NewRoot();
        StartCoroutine(GenerateRoot());
    }

    public void CookingProcces()
    {
        acceptButtom.SetActive(false);
        declineButtom.SetActive(false);
        StartCoroutine(AnimationsButtoms((anin_cooking)));
        rootQuantity -= 1;
        minSelecteds = Mathf.Abs(rootSelecteds / 2);
        GM.CookingTime();
        cookingButtom.SetActive(false);
        confirmButtom.SetActive(true);

    }

    public void ConfirmProcces()
    {
        StartCoroutine(AnimationsButtoms(anin_confirm));
        StartCoroutine(CookingAnimations());
    }

    public void ResultProcces()
    {
        StartCoroutine(AnimationsButtoms(anin_shop));
        GM.ResultTime();
        shopButtom.SetActive(false);
        StartCoroutine(ResultAnimations());
    }
    
    public void NextDayProcces()
    {
        StartCoroutine(AnimationsButtoms((anin_next)));
        anin_resultText.SetBool("Show", false);
        
        Destroy(advice, 2f);
        Destroy(currentRoot);
        nextDayButtom.SetActive(false);
        GM.NewDay();
    }

    public void EliminatedRoots(int quantity, string typeRoot)
    {
        rootsEliminated += quantity;
        Cooking();
     
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
    
    private void Cooking()
    {
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
        cuttingTable.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        releaseRoots.SetActive(false);
        yield return new WaitForSeconds(1f);
        
        confirmButtom.SetActive(false);
        cuttingTable.transform.rotation = Quaternion.Euler(-45f, 0f, 0f);
        yield return new WaitForSeconds(2f);

        destroyRoots.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        shopButtom.SetActive(true);
        releaseRoots.SetActive(true);
        destroyRoots.SetActive(false);
        

    }

    private void Result()
    {
        if (porcent > 100)
        {
            advice = Instantiate(results[0], resultPos.position, Quaternion.identity);
            GM.GameLifes(3);
        }
        if (porcent > 90)
        {
            advice = Instantiate(results[1], resultPos.position, Quaternion.identity);
        }
        if (porcent > 70 &&  porcent <= 90)
        {
            advice = Instantiate(results[2], resultPos.position, Quaternion.identity);
            GM.GameLifes(1);
        }
        if (porcent > 40 &&  porcent <= 70)
        {
            advice = Instantiate(results[3], resultPos.position, Quaternion.identity);
            GM.GameLifes(2);
        }
        if (porcent > 0 &&  porcent <= 40)
        {
            advice = Instantiate(results[4], resultPos.position, Quaternion.identity);
            GM.GameLifes(4);
        }

        if (porcent == 0)
        {
            advice = Instantiate(results[5], resultPos.position, Quaternion.identity);
            GM.GameLifes(5);
        }
        
    }

    private IEnumerator ResultAnimations()
    {
        anin_resultText.SetBool("Show", true);

        if (greenPoints == 0 && redPoints == 0)
        {
            resultText.text = "No soup today";
            porcent = 101;
            Result();
            yield return new WaitForSeconds(3f);
            nextDayButtom.SetActive(true);
            yield break;
        }
        
        porcent = (greenPoints / (greenPoints + redPoints)) * 100f;
        resultText.text = Mathf.Floor(porcent) + "%";
        
        
        Result();
        yield return new WaitForSeconds(3f);
        nextDayButtom.SetActive(true);
        
    }
    
}
