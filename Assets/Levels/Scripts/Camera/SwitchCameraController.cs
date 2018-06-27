using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraController : MonoBehaviour {

    // Deve dire che può interagire e che 
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.tag == "Player")
        {
            other.transform.parent.GetComponent<PerspectiveManager>().view = Perspective.firstPerson;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.tag == "Player")
        {
            other.transform.parent.GetComponent<PerspectiveManager>().view = Perspective.thirdPerson;
        }
    }






}
