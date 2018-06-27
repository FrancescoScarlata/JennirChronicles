using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollisionManager : MonoBehaviour {

    private DamageProvider dp;

    void Start()
    {
        dp = GetComponent<DamageProvider>();
    }
    
    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.transform.name);
        if (col.transform.tag == "NPC")
        {
            Debug.Log(col.transform.name);
            if (dp != null)
                dp.ProvideDamage(col.transform);
            Debug.Log("Ho colpito collision");
        }
    }

    void OnTriggerEnter(Collider col)
    { 
        if (col.transform.parent!=null && col.transform.parent.tag == "NPC")// the collider is a child of the bandits
        {
            if (dp != null)
                dp.ProvideDamage(col.transform.parent.transform);
          //  Debug.Log("collider hitted");
        }
    }

}
