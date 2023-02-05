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
            PC.SetRoot(RootsBP[Random.Range(0, 3)]);
            break;
         case 2:
            PC.SetRoot(RootsBP[Random.Range(2, 6)]);
            break;
         case 3:
            PC.SetRoot(RootsBP[Random.Range(5, 9)]);
            break;
         case 4:
            PC.SetRoot(RootsBP[Random.Range(8, 12)]);
            break;
         case 5:
            PC.SetRoot(RootsBP[Random.Range(11, 15)]);
            break;
         case 6:
            PC.SetRoot(RootsBP[Random.Range(0, 15)]);
            break;
         case 7:
            PC.SetRoot(RootsBP[Random.Range(7, 15)]);
            break;
         case 8:
            var newmin = Random.Range(0, 8);
            PC.SetRoot(RootsBP[Random.Range(newmin, 15)]);
            break;
      }
   }
   
   
}
