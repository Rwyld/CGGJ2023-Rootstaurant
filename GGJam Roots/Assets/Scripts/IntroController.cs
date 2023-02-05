using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class IntroController : MonoBehaviour
{
    public GameObject[] fallingRoots;
    public Transform dropRoots1, dropRoots2;
    public float timerDrops = 0.5f;
    
    
    public GameObject Fade;
    public Animator fadeAnim;

    private void Update()
    {
        timerDrops -= Time.deltaTime;

        if (timerDrops < 0)
        {
            var root1 = Instantiate(fallingRoots[Random.Range(0, 7)], dropRoots1.position, Quaternion.identity);
            var root2 = Instantiate(fallingRoots[Random.Range(0, 7)], dropRoots2.position, Quaternion.identity);
            Destroy(root1, 2f);
            Destroy(root2, 2f);
            timerDrops = 0.5f;
        }
    }


    public void NextScene()
    {
        StartCoroutine(ChangeScene());

    }




    public void Exit()
    {
        Application.Quit();
    }




    IEnumerator ChangeScene()
    {
        fadeAnim.SetBool("Transition", true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("01_Gameplay");
    }
}
