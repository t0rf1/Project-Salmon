

using System.Collections.Generic;
using UnityEngine;

public class NpcMover : MonoBehaviour
{
    public GameObject[] bodies;
    public bool once;

    public bool overidee = false;
    public Vector3 movevect = Vector3.zero;
    public Animator animer;

    int choice = 0;
    public CurveMgr curvMgr;

    public Rigidbody rb;

    public RaycastHit hit;

    public float walkSpeed;

    public float hoverHeight;
    public float spring;
    public float damp;
    public float repelLength;
    public float replForce;
    public LayerMask repelLayer;

    public GameObject bodl;
    public float groundRayHeight;
    public float groundRayRadius;
    public float groundRayLenght;
    public LayerMask groundLayer;

    public bool neib;
    // Start is called before the first frame update
    void Start()
    {


        GetrANDOME();

        int randoBody = Random.Range(0, 4);

        switch (randoBody)
        {//animer.Play("Walker1", 0 , Random.Range(0,24));
            case 0:
                bodies[0].SetActive(true);
                break;
            case 1:
                bodies[1].SetActive(true);
                break;
            case 2:
                bodies[2].SetActive(true);
                break;
            case 3:
                bodies[3].SetActive(true);
                break;
        }



        float closest = 100;
        for (int i = 0; i < curvMgr.cPoints.Length; i++)
        {
            if (i < curvMgr.cPoints.Length - 1)
            {
                Vector3 a = curvMgr.cPoints[i].transform.position;
                Vector3 b = curvMgr.cPoints[i + 1].transform.position;
                a = new Vector3(a.x, 0, a.z);
                b = new Vector3(b.x, 0, b.z);
                Vector3 ab = b - a;
                Vector3 c = transform.position;
                c = new Vector3(c.x, 0, c.z);

                Vector3 perpe = Vector3.Dot(ab, c) * c;
                Debug.DrawRay(ab - perpe, Vector3.up * 5, Color.green);
            }

            //float tt = Vector3.Dot(transform.position,a -b)/Vector3.Dot(transform.position,ab);
            // tt = Mathf.Clamp01(tt);

            //Vector3 closestp= a + tt * ab;



            float di = Vector3.Distance(curvMgr.cPoints[i].position, transform.position);
            if (di < closest && di < 1)
            {

                choice = i;
                closest = di;

            }



        }
    }

    void GetrANDOME ()
    {
int rando = Random.Range(0, 3);

        switch (rando)
        {//animer.Play("Walker1", 0 , Random.Range(0,24));
            case 0:
                animer.SetTrigger("a");
                break;
            case 1:
                animer.SetTrigger("b");
                break;
            case 2:
                animer.SetTrigger("c");
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*for (int i = 0; i < curvMgr.cPoints.Length; i++)
        {
            if (i<curvMgr.cPoints.Length-1)
            {
                Vector3 a = curvMgr.cPoints[i].transform.position;
                Vector3 b = curvMgr.cPoints[i + 1].transform.position;
                a = new Vector3(a.x, 0, a.z);
                b = new Vector3(b.x, 0, b.z);
                
                Vector3 ab = b - a;

                Vector3 c = transform.position;
                c = new Vector3(c.x, 0, c.z);



                //Vector3 perpe = Vector3.Dot(ab,c);
                //Debug.DrawRay(perpe, Vector3.up * 5, Color.green);
            }
        }*/


        /*var oveerlap = Physics.OverlapSphere(transform.position, repelLength, repelLayer);
        Debug.Log("overlap" + oveerlap.Length);


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
                rb.AddForce((transform.position - rep.gameObject.transform.position) * replForce);
                Debug.Log(rep);
            }
        }*/


        if (Grounded())
        {
            Bounce(hoverHeight, spring, damp);
        }


        //

        //detect prox

        if (choice < curvMgr.cPoints.Length - 1)
        {


            if (Vector3.Distance(curvMgr.cPoints[choice + 1].position, transform.position) < 5)
            {
                //Debug.Log("reached");
                choice++;
               // Debug.Log(choice);

            }
        }
        else
        {
            if (Vector3.Distance(curvMgr.cPoints[0].position, transform.position) < 5)
            {//
                //Debug.Log("reached");
                choice = 0;
                //Debug.Log(choice);

            }
        }

        /* for (int i = 0; i < curvMgr.cPoints.Length; i++)
         {
             float di = Vector3.Distance(curvMgr.cPoints[i].position, transform.position);
             if (di < closest && di < 1)
             {

                 choice = i;
                 closest = di;

             }



         }*/

        Debug.DrawRay(curvMgr.cPoints[choice].position, Vector3.up);

        if (!overidee)
        {
            if (choice < curvMgr.cPoints.Length - 1)
            {
                movevect = curvMgr.cPoints[choice + 1].transform.position - transform.position;



                // Gizmos.DrawLine(cPoints[i].transform.position,cPoints[i+1].transform.position);
            }
            else if (curvMgr.telep)
            {
                transform.position = curvMgr.cPoints[0].transform.position;
            }
            else
            {
                movevect = curvMgr.cPoints[0].transform.position - transform.position;
            }
        }

        rb.AddForce(movevect * walkSpeed);

        RotateTowardsVector(movevect, 2);

        if(rb.velocity.magnitude>0.5f)
        {   
            if(once)
            {
                animer.SetBool("idli",false);
                once = false;
                GetrANDOME();
            }
            
        }else
        {
            animer.SetBool("idli",true);
            once = true;
            
        }


    }

    public void RotateTowardsVector(Vector3 _vect, float _speed)
    {
        bodl.transform.rotation = Quaternion.Lerp(bodl.transform.rotation, Quaternion.Euler(0, (float)Mathf.Atan2(_vect.x, _vect.z) * Mathf.Rad2Deg, 0), _speed * Time.deltaTime);
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
