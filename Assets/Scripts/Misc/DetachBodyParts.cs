using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetachBodyParts : MonoBehaviour
{
    public string joint;
    public bool hasHit;
    HingeJoint2D hj;
    FixedJoint2D fj;

    [SerializeField] float threshold;
    // Start is called before the first frame update
    void Start()
    {
        if(joint == "Fixed")
        {
            fj = gameObject.GetComponent<FixedJoint2D>();
        }
        else if(joint == "Hinge")
        {
            hj = gameObject.GetComponent<HingeJoint2D>();
        }

        hasHit = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Arrow"))
        {
            if (joint == "Hinge")
            {
                if(hj.reactionForce.magnitude > threshold)
                {
                    hasHit = true;
                }
            }
            else if (joint == "Fixed")
            {
                if (fj.reactionForce.magnitude > threshold)
                {
                    hasHit = true;
                }
            }
                
        }
    }
}
