using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearGateDeathColliderManager : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.tag == "Player")
        {
           if ( other.transform.parent.GetComponent<PlayerController>().whereAmI<2)
                other.transform.parent.GetComponent<PlayerDeathController>().Discovered();
        }

    }
}
