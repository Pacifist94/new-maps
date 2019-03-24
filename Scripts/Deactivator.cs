using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator : MonoBehaviour {




    void OnTriggerEnter2D(Collider2D col)
    {


        col.gameObject.SetActive(false);

        if (col.CompareTag("empty"))//Do not reset Combo
        {
            
        }
        else // reset combo
        {
            PoolManager.instance.ResetCombo();
        }

           
    }
}
