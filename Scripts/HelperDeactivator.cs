using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperDeactivator : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {


        col.GetComponent<MoveDown>().beginCoroutine();
         
      //  col.GetComponent<MoveDown>().col2d.enabled = false;
        PoolManager.instance.sp.SpawnAudio(col.GetComponent<MoveDown>().soundID);


      


    }
    

}
