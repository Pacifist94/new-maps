
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{


    public float V_Units;

    public LoadXml xmlLoadMe;

    public GameObject[] AllPrefabs;


    public int counter = 0;
    public int counterLimit = 4; // Change from Inspector!!!!

    public float spriteHeight=1.5f;// Change Inspector


     
   
    void Start()
    {

        //SOUNDS INTACES

        //Create Instances of AllSounds
        /* PoolManager.instance.CreatePool(AllSounds[0], 5);
         PoolManager.instance.CreatePool(AllSounds[1], 5);
         PoolManager.instance.CreatePool(AllSounds[2], 5);
         PoolManager.instance.CreatePool(AllSounds[3], 5);
         PoolManager.instance.CreatePool(AllSounds[4], 5);
         PoolManager.instance.CreatePool(AllSounds[5], 5);
         PoolManager.instance.CreatePool(AllSounds[6], 5);*////
        //--------------------------------

        //Create Instances of Audio 0
       



        //Create Instances of empty6
        PoolManager.instance.CreatePool(AllPrefabs[0], 2);

        //Create Instances of Prefabs
        PoolManager.instance.CreatePool(AllPrefabs[1], 40);

        //PoolManager.instance.CreatePool(AllPrefabs[2], 20);
        //PoolManager.instance.CreatePool(AllPrefabs[3], 20);
        //PoolManager.instance.CreatePool(AllPrefabs[4], 20);
        //PoolManager.instance.CreatePool(AllPrefabs[5], 20);

        //Create Instances of Holds
        PoolManager.instance.CreatePool(AllPrefabs[7], 30);

        //Create Instances of Helpers
        PoolManager.instance.CreatePool(AllPrefabs[8], 40);

        //Create Instances of Guide
        PoolManager.instance.CreatePool(AllPrefabs[6], 3);

        float height = float.Parse(Screen.height.ToString());
        float width = float.Parse(Screen.width.ToString());
        V_Units = height / width * 5; 

        Spawn();
    }


    public void SpawnAudio(int i) {
        //// PoolManager.instance.ReuseAudio(AllSounds[i], Vector3.one, Quaternion.identity);
          PoolManager.instance.ReuseAudio(i,1f);
       
    } 

    void Spawn()
    {
        
        //Guide
        PoolManager.instance.ReuseObject(AllPrefabs[6], new Vector3(3, V_Units * 2, -1f), Quaternion.identity);

        //Guide
        PoolManager.instance.ReuseObject(AllPrefabs[6], new Vector3(3, (V_Units * 2) + spriteHeight, -1f), Quaternion.identity);
    }

    public void SpawnAt(float YDist)
    {
        if (counter > counterLimit) 
        {
            //reset
           // counter = 0;
            transform.gameObject.SetActive(false);
            return;
        }

         PoolManager.instance.ReuseObject(AllPrefabs[6], new Vector3(3, YDist, -1f), Quaternion.identity);
       
        // if Prefab ... Does not contains "," Single
        if (!xmlLoadMe.m_coord[counter].Contains(","))
        {
            //Assign Sound
            PoolManager.instance.clipToAssign = int.Parse(xmlLoadMe.m_sound[counter]);

            //Spawn Prefab
            PoolManager.instance.ReuseObject(AllPrefabs[int.Parse(xmlLoadMe.m_coord[counter])], new Vector3(int.Parse(xmlLoadMe.m_pos[counter]), YDist, -0.01f), Quaternion.identity);
        }

        else
        {  // if Prefab is more than 1000 i.e. 1000212345, 100000100, 2, ...Multiple

          string[] strCoord = xmlLoadMe.m_coord[counter].Split(',');
          string[] strPos = xmlLoadMe.m_pos[counter].Split(',');
          string[] strSound = xmlLoadMe.m_sound[counter].Split(',');

            for (int i = 0; i < strCoord.Length; i++)
            {

                //Assign Sound 
                PoolManager.instance.clipToAssign = int.Parse(strSound[i]);

                PoolManager.instance.ReuseObject(AllPrefabs[int.Parse(strCoord[i])], new Vector3(int.Parse(strPos[i]), YDist, -0.01f), Quaternion.identity);
               
            }

        }

        //increase counter
        counter++;


    }


}