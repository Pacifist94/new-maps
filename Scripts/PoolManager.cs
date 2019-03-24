using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolManager : MonoBehaviour {

 

    Dictionary<int, Queue<GameObject>> poolDictionary = new Dictionary<int, Queue<GameObject>>();

    static PoolManager _instance;

    public float GlobalSpeed = 0.4f;

    public Text txt;

    public Text txtCurrentCombo;

    public Text txtMaxCombo;

    public Spawner sp;

    public int CurrentCombo = 0;

    public int maxCombo = 0;

    public int clipToAssign;

    public AudioSource audi;

    public AudioClip[] clips= new AudioClip[10]; //Change in inspector

    //Ranges
    public float UpperLimitBad;
    public float DownerLimitBad;

    public float UpperLimitGood;
    public float DownerLimitGood;


    public float UpperLimitGoodHold;
    public float UpperLimitBadHold;

    public float SHeight;

    public float[] BPM;
    public int[] limit;
     
    void Start() {

        
        SHeight = 1;//sp.spriteHeight;
        //Debug.Log(sp.spriteHeight);
        UpperLimitBad = SHeight - (SHeight * 0.14f) + 1;
        DownerLimitBad = (SHeight * 0.14f);

        UpperLimitGood = SHeight - (SHeight * 0.305f) + 1;
        DownerLimitGood = (SHeight * 0.305f);


        UpperLimitGoodHold = (3 * SHeight) - (SHeight * 0.305f);
        UpperLimitBadHold = (3 * SHeight) - (SHeight * 0.14f);


       // Debug.Log(UpperLimitBad + "UpperLimitBad|" + DownerLimitBad + "DownerLimitBad|" + UpperLimitGood + "UpperLimitGood|" + DownerLimitGood + "DownerLimitGood|" + UpperLimitGoodHold + "UpperLimitGoodHold|" + UpperLimitBadHold + "UpperLimitBadHold|");



     
    }





    public void AddToCombo(int score)
    {
        CurrentCombo++;
        txtCurrentCombo.text = CurrentCombo.ToString();

        if (CurrentCombo> maxCombo)
        {
            maxCombo = CurrentCombo;

            txtMaxCombo.text =  maxCombo.ToString();
        }
    }
     

    public void ResetCombo() {
        CurrentCombo = 0;
        txtCurrentCombo.text = CurrentCombo.ToString();
    }

    public static PoolManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PoolManager>();
            }
            return _instance;
        }
    }


    public void CreatePool(GameObject prefab, int poolSize)
    {
        int poolKey = prefab.GetInstanceID();



        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<GameObject>());


            for (int i = 0; i < poolSize; i++)
            {
                GameObject newObject = Instantiate(prefab) as GameObject;
                newObject.SetActive(false);
                poolDictionary[poolKey].Enqueue(newObject);
            }
        }
    }



    public void ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        int poolKey = prefab.GetInstanceID();

        if (poolDictionary.ContainsKey(poolKey))
        {
            GameObject objectToReuse = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(objectToReuse);



      


            objectToReuse.transform.position = position;
            objectToReuse.transform.rotation = rotation;

            objectToReuse.GetComponent<MoveDown>().speed = GlobalSpeed;

            //Triggers Start
            objectToReuse.GetComponent<MoveDown>().myStart();


            if (objectToReuse.GetComponent<MoveDown>().hold) // is a hold
            {
              
                objectToReuse.GetComponent<MoveDown>().wasPressed = false;
                objectToReuse.GetComponent<MoveDown>().wasFingAssigned = false;
                objectToReuse.GetComponent<MoveDown>().myFingerID = 11;

                objectToReuse.GetComponent<Animator>().Play("HoldPressIdle");
                objectToReuse.GetComponent<Animator>().enabled = true;

                objectToReuse.GetComponent<MoveDown>().soundID = clipToAssign;

                // Reduce Hold Secconds according to speed
                objectToReuse.GetComponent<MoveDown>().holdSeconds = 1/((GlobalSpeed / 0.3378f)/ 0.5f) ;



            }
            if (objectToReuse.GetComponent<MoveDown>().isTapPrefab)// is a tap
            {

                objectToReuse.GetComponent<MoveDown>().wasPressed = false;
                objectToReuse.GetComponent<Animator>().Play("TapIdle");
                objectToReuse.GetComponent<Animator>().enabled = true;

                objectToReuse.GetComponent<MoveDown>().soundID = clipToAssign;

                
            }

            objectToReuse.GetComponent<Collider2D>().enabled = true;
            objectToReuse.GetComponent<MoveDown>().wasCol2dDisabled = false;
      


            objectToReuse.SetActive(true);
        } 
    }

    

    public void ReuseAudio(int AudioClipID, float VolumeScaler)
    {



        audi.PlayOneShot(clips[AudioClipID]);


     

    }



}



