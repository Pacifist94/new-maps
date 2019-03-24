using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideAndSpawn : MonoBehaviour {

    public Spawner sp;
         

    void OnTriggerEnter2D(Collider2D col)
    {

        sp.SpawnAt(col.gameObject.transform.position.y + (sp.spriteHeight *2));
        
        //Disable Guide
        col.gameObject.SetActive(false);

   
    }

}
