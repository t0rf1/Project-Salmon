using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlayerMoover : MonoBehaviour
{
    public GameObject camerro;

    public Animator animer;
    public GameObject bodl;
    public float speed;
    public float repelLength;
    public float replForce;
    public bool neib;
    public LayerMask repelLayer;

    public Rigidbody rb;

    public RaycastHit hit;

    public float hoverHeight;
    public float spring;
    public float damp;

    public float groundRayHeight ;
    public float groundRayRadius;
    public float groundRayLenght;
    public LayerMask groundLayer;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(Grounded())
        {
        Bounce(hoverHeight,spring,damp);
        }

        var oveerlap = Physics.OverlapSphere(transform.position, repelLength, repelLayer);
        //Debug.Log("overlap" + oveerlap.Length);


        if (oveerlap.Length > 1)
        {
            neib = true;
        }
        else
        {
            neib = false;
        }

        foreach (var rep in oveerlap)
        {
            if (rep.gameObject != this.gameObject)
            {
            
                rep.GetComponent<Rigidbody>().AddForce((rep.gameObject.transform.position - transform.position) * replForce);
                //Debug.Log(rep);
            }
        }
        
        Vector3 inputdIR = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));



        rb.AddForce(((inputdIR.z * camerro.transform.forward) + (inputdIR.x * camerro.transform.right)) *speed);

        var skibidi = ((inputdIR.z * camerro.transform.forward) + (inputdIR.x * camerro.transform.right));
        
        if(skibidi.magnitude>0)
        {RotateTowardsVector(skibidi,2);
        animer.SetBool("Skib",true);

        }else
        {
        animer.SetBool("Skib",false);
        }

        /*if(rb.velocity.magnitude>0.2f)
        {
            
        }else
        {
            
        }*/


    }

    /*void Bounce()
    {

        Vector3 vel = rb.velocity;
        //Vector3 dir = transform.TransformDirection(Vector3.down);
        Vector3 dir = Vector3.down;
        float dirVel = Vector3.Dot(dir, vel);
        float x = hit.distance - hoverHeight;
        float springForce = (x * spring) - (dirVel * damp);

        rb.AddForce(dir * springForce);
    }*/



    public void Bounce(float _hoverHeight, float _spring, float _damp)
    {

        Vector3 vel = rb.velocity;
        //Vector3 dir = transform.TransformDirection(Vector3.down);
        Vector3 dir = Vector3.down;
        float dirVel = Vector3.Dot(dir, vel);
        float x = hit.distance - _hoverHeight;
        float springForce = (x * _spring) - (dirVel * _damp);

        rb.AddForce(dir * springForce);


    }


    public void RotateTowardsVector(Vector3 _vect, float _speed)
    {
        bodl.transform.rotation = Quaternion.Lerp(bodl.transform.rotation, Quaternion.Euler(0, (float)Mathf.Atan2(_vect.x, _vect.z) * Mathf.Rad2Deg, 0), _speed * Time.deltaTime);
    }

    public bool Grounded()
    {
        return Physics.SphereCast(transform.position + new Vector3(0, groundRayHeight + groundRayRadius, 0), groundRayRadius, Vector3.down, out hit, groundRayLenght, groundLayer);
        
    }


    private void OnDrawGizmos()
    {
        
        Debug.DrawRay(transform.position + new Vector3(0, groundRayHeight, 0), Vector2.down * groundRayLenght, Color.red);
        if (Physics.SphereCast(transform.position + new Vector3(0, groundRayHeight + groundRayRadius, 0), groundRayRadius, Vector3.down, out hit, groundRayLenght, groundLayer))
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(hit.point, groundRayRadius);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + new Vector3(0, groundRayHeight - groundRayLenght, 0), groundRayRadius);

        }

        if (neib)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.yellow;
        }
        Gizmos.DrawWireSphere(transform.position, repelLength);
    }

}
