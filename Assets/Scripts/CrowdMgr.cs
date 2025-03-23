using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdMgr : MonoBehaviour
{
    public float pauseTime;
    public float pTimer;
    public bool once = false;
    public List<NpcMover> npcMover;
    public bool setVal;
    public bool positionOverride;
    public float walkSpeed;
    public Vector3 moveVect;


    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        pTimer = pauseTime;
        if (npcMover.Count == 0)
        {
            var p = GameObject.FindGameObjectsWithTag("person");
            foreach (var pe in p)
            {
                
                npcMover.Add(pe.GetComponent<NpcMover>());
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (setVal)
        {
            
            foreach (NpcMover m in npcMover)
            {
                m.walkSpeed = walkSpeed;
                m.gameObject.GetComponent<CapsuleCollider>().radius = radius;
            }
            
            
            setVal = false;
        }
        if (positionOverride)
        {
            if(pTimer>0)
            {
                pTimer-=Time.deltaTime;
                foreach (NpcMover m in npcMover)
                {
                m.overidee = true;
                m.movevect = new Vector3(0,0,0);
                }
            }
            else
            {
            once = true;
            foreach (NpcMover m in npcMover)
            {
                m.overidee = true;
                m.movevect = moveVect;
            }
            }

        }
        else
        {

            if (once)
            {
                once = false;
                foreach (NpcMover m in npcMover)
                {
                    m.overidee = false;

                }
            }
        }
    }
}
