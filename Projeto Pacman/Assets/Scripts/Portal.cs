using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    void OnTriggerEnter(Collider coll)
    {
        if(this.gameObject.name == "portal_e")
        {
            coll.transform.position = new Vector3(10.91f,coll.transform.position.y,0.1499959f);
        }
        if(this.gameObject.name == "portal_d")
        {
            coll.transform.position = new Vector3(-10.989f,coll.transform.position.y,0.3199995f);
        }
    }
}
