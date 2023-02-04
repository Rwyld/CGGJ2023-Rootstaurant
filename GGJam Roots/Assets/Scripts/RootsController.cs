using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RootsController : MonoBehaviour
{
   public GameManager GM;
   public PlayerController PC;
   
   
   public RootsBlueprints[] RootsBP;

   public int day;
   public int maxValue;


   private void Start()
   {
      day = GM.day;
      
      maxValue = RootsBP.Length;
      NewRoot();
      PC.GenerateRoot();
   }

   public void NewRoot()
   {
      switch (day)
      {
         case 0:
            PC.SetRoot(RootsBP[Random.Range(0, 2)]);
            break;
         case 1:
            PC.SetRoot(RootsBP[Random.Range(0, 2)]);
            break;
         case 2:
            PC.SetRoot(RootsBP[Random.Range(2, 5)]);
            break;
         case 3:
            PC.SetRoot(RootsBP[Random.Range(5, 8)]);
            break;
         case 4:
            PC.SetRoot(RootsBP[Random.Range(11, 14)]);
            break;
         case 5:
            PC.SetRoot(RootsBP[Random.Range(15, 17)]);
            break;
         case 6:
            PC.SetRoot(RootsBP[Random.Range(0, 17)]);
            break;
         case 7:
            PC.SetRoot(RootsBP[Random.Range(0, 20)]);
            break;
      }
   }
   
   
}
