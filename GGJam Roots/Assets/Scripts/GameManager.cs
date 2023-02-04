using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController PC;
    public GameObject receptionCam, cookingCam, shopCam;
    public TextMeshProUGUI DayText;
    public GameObject dayText, fadeTransition;
    public Animator fade;
    public GameObject tutorialbt1, tutorialbt2, tutorialbt3;
    
    
    public int day;
    public int numberRoots;

    private void Start()
    {
        day = 0;
        numberRoots = 3;
        StartCoroutine(StartGame());
    }

    public void NewDay()
    {
        StartCoroutine(TransitionDay());
        
    }

    public void CookingTime()
    {
        receptionCam.SetActive(false);
        cookingCam.SetActive(true);
    }

    public void ResultTime()
    {
        cookingCam.SetActive(false);
        shopCam.SetActive(true);
    }

    private IEnumerator TransitionDay()
    {
        fade.SetBool("Transition", true);
        yield return new WaitForSeconds(1.5f);
        
        dayText.SetActive(false);
        shopCam.SetActive(false);
        receptionCam.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        
        
        fade.SetBool("Transition", false);
        yield return new WaitForSeconds(1f);
        
        numberRoots += 1;
        day += 1;
        dayText.SetActive(true);
        DayText.text = "Day " + day;
        PC.NextDay();
    }

    private IEnumerator StartGame()
    {
        PC.rootQuantity = -1;
        yield return new WaitForSeconds(1f);
        dayText.SetActive(true);
        DayText.text = "Day " + day;
        tutorialbt1.SetActive(true);
    }

    public void TutorialButtom()
    {
        CookingTime();
        tutorialbt1.SetActive(false);
        tutorialbt2.SetActive(true);
    }
    
    public void TutorialButtom2()
    {
        ResultTime();
        tutorialbt2.SetActive(false);
        tutorialbt3.SetActive(true);
    }
    
    public void TutorialButtom3()
    {
        tutorialbt3.SetActive(false);
        PC.NextDayProcces();
    }
    
}
