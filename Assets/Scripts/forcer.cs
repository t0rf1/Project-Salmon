using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forcer : MonoBehaviour
{

    public float repelLength;
    public LayerMask repelLayer;
    public float replForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        var oveerlap = Physics.OverlapSphere(transform.position, repelLength, repelLayer);
        //Debug.Log("overlap" + oveerlap.Length);


  
        foreach (var rep in oveerlap)
        {
            if (rep.gameObject != this.gameObject)
            {
            
                rep.GetComponent<Rigidbody>().AddForce((rep.gameObject.transform.position - transform.position) * replForce);
                //Debug.Log(rep);
            }
        }
    }
}
