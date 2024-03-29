using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public KeyCode moveL;
   public KeyCode moveR;

   public float horizVel=0;
   public int laneNum = 2;

   string controlLocked="n";


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         GetComponent<Rigidbody> ().velocity = new Vector3(horizVel,0,4);

         if((Input.GetKeyDown (moveL))&&(laneNum>1))
         {
            horizVel=-2;
            StartCoroutine(stopslide());
            laneNum -= 1;
            controlLocked="y";
         }
         if((Input.GetKeyDown (moveR))&&(laneNum<3))
         {
            horizVel=2;
            StartCoroutine(stopslide());
            laneNum += 1;
            controlLocked="y";
         }
         void onCollisionEnter(Collision other)
         {
            if (other.gameObject.tag== "lethal")
            {
                Destroy(gameObject);
            }
         }

         IEnumerator stopslide()
         {
            yield return new WaitForSeconds(.5f);
            horizVel=0;
            controlLocked="n";
         }

         
    }
}
