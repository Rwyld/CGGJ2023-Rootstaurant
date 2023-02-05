using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController PC;
    public GameObject receptionCam, cookingCam, shopCam;
    public TextMeshProUGUI DayText;
    public GameObject dayText, fadeTransition;
    public Animator fade;
    public GameObject tutorialbt1, tutorialbt2, tutorialbt3;
    public GameObject releaseContainer, spliter;

    public int currentLifes;
    private bool EndGame = false;
    public GameObject EndGameInfo, AllButtomsGame;
    
    public int day;
    public int numberRoots;

    private void Start()
    {
        currentLifes = 10;
        day = 0;
        numberRoots = 2;
        StartCoroutine(StartGame());
    }

    public void NewDay()
    {
        StartCoroutine(TransitionDay());
        
    }

    public void CookingTime()
    {
        StartCoroutine(ReleaserStacker());
        receptionCam.SetActive(false);
        cookingCam.SetActive(true);
    }

    public void ResultTime()
    {
        releaseContainer.SetActive(true);
        spliter.SetActive(true);
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

        if (EndGame == true)
        {
            EndGameInfo.SetActive(true);
            AllButtomsGame.SetActive(false);
        }
        
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
        NewDay();
    }

    private IEnumerator ReleaserStacker()
    {
        releaseContainer.SetActive(false);
        yield return new WaitForSeconds(1f);
        spliter.SetActive(false);
    }

    public void GameLifes(int life)
    {
        if (currentLifes <= 0)
        {
            EndGame = true;
        }
        else
        {
            currentLifes -= life;
        }
        
    }

    public void GameOver()
    {
        StartCoroutine(GameOverAnimations());
    }
    
    public IEnumerator GameOverAnimations()
    {
        fade.SetBool("Transition", true);
        yield return new WaitForSeconds(1.5f);

        EndGameInfo.SetActive(false);
        AllButtomsGame.SetActive(true);
        
        fade.SetBool("Transition", false);
        yield return new WaitForSeconds(1f);

        
        day = 1;
        numberRoots = 3;
        dayText.SetActive(true);
        DayText.text = "Day " + day;
        PC.NextDay();
    }
    
}
