using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMgr : MonoBehaviour
{
    public bool telep = false;
    public Transform[] cPoints;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < cPoints.Length; i++)
        {
            //Gizmos.DrawRay(cPoints[i].transform.position, cPoints[i].transform.forward);
            
                if(i<cPoints.Length-1)       
                {
                    Gizmos.DrawRay(cPoints[i].transform.position,cPoints[i+1].transform.position-cPoints[i].transform.position);
                   // Gizmos.DrawLine(cPoints[i].transform.position,cPoints[i+1].transform.position);
                } 
            
        }


    }
}
