﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallLoad : MonoBehaviour {
       
    public void Click(int i){
        LevelLoader LL  = (LevelLoader)FindObjectOfType(typeof(LevelLoader));

            LL.LoadLevel(i);
         

    }

    public void ClickToGame(int i)
    {
        LevelLoader LL = (LevelLoader)FindObjectOfType(typeof(LevelLoader));

        CreateProfile CP = (CreateProfile)FindObjectOfType(typeof(CreateProfile));

        // LevelLoader LL2 = Object.FindObjectOfType<LevelLoader>();
        LL.Level = CP.levelNumber; 
        LL.LoadLevel(i);


    }


}
