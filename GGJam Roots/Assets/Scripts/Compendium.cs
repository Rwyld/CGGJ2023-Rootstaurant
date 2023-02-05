using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compendium : MonoBehaviour
{
    public GameObject greenInfo, grayInfo;
    public GameObject nextGray, nextGreen, openCompendium, exitCompendium;


    public void NextPageGreen()
    {
        greenInfo.SetActive(true);
        nextGray.SetActive(true);
        grayInfo.SetActive(false);
        nextGreen.SetActive(false);
    }
    
    public void NextPageGray()
    {
        greenInfo.SetActive(false);
        nextGray.SetActive(false);
        grayInfo.SetActive(true);
        nextGreen.SetActive(true);
    }
    
    public void ExitCompendium()
    {
        greenInfo.SetActive(false);
        grayInfo.SetActive(false);
        nextGray.SetActive(false);
        nextGreen.SetActive(false);
        openCompendium.SetActive(true);
        exitCompendium.SetActive(false);
    }

    public void OpenCompendium()
    {
        exitCompendium.SetActive(true);
        NextPageGreen();
    }

}
