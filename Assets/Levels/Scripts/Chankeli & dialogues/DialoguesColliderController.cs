using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguesColliderController : MonoBehaviour {

    //The parent of the bandits who have to talk
    public GameObject EmptyParent;
    public bool justOneTime = true;
    private bool firstTime = true;
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.tag == "Player")
        {
            EmptyParent.GetComponent<DialoguesManager>().InsideTheRange(true);
            if (firstTime) // The first time gives the signal to start the conversation
            {
                if(EmptyParent.transform.childCount!=0)
                    EmptyParent.GetComponent<DialoguesManager>().StartAudio();
                firstTime = false;
            }
                
            if(justOneTime)
                this.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.tag == "Player")
        {
            EmptyParent.GetComponent<DialoguesManager>().InsideTheRange(false);
        }

    }


}
