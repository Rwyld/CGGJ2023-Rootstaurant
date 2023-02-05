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

   private void Update()
   {
      day = GM.day;
   }

   public void NewRoot()
   {
      switch (day)
      {
         case 0:
            PC.SetRoot(RootsBP[Random.Range(0, 1)]);
            break;
         case 1:
            PC.SetRoot(RootsBP[Random.Range(0, 7)]);
            break;
         case 2:
            PC.SetRoot(RootsBP[Random.Range(5, 13)]);
            break;
         case 3:
            PC.SetRoot(RootsBP[Random.Range(12, 18)]);
            break;
         case 4:
            PC.SetRoot(RootsBP[Random.Range(17, 24)]);
            break;
         case 5:
            PC.SetRoot(RootsBP[Random.Range(22, 30)]);
            break;
         case 6:
            PC.SetRoot(RootsBP[Random.Range(0, 15)]);
            break;
         case 7:
            PC.SetRoot(RootsBP[Random.Range(15, 30)]);
            break;
         case 8:
            var min_A = Random.Range(0, 7);
            PC.SetRoot(RootsBP[Random.Range(min_A, 20)]);
            break;
         case 9:
            var min_B = Random.Range(0, 14);
            PC.SetRoot(RootsBP[Random.Range(min_B, 25)]);
            break;
         case 10:
            var min_C = Random.Range(0, 20);
            PC.SetRoot(RootsBP[Random.Range(min_C, 30)]);
            break;
         case 11:
            //TODO Win
            break;
         
      }
   }
   
   
}
