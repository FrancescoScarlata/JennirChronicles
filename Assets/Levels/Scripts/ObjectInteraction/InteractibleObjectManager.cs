using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractibleObjectManager : MonoBehaviour {

    //Text where is written when he can interact
    public Text InteractText;
    public Text Notifier;
    public bool multipleInteractions=false;
    private bool firstInteraction = true;
    private KeyCode interactKey;

    // Deve dire che può interagire e che 
    void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent!=null&&other.transform.parent.tag == "Player")
        {      
            if(firstInteraction||multipleInteractions)
            {
                if(interactKey!=KeyCode.E)
                {
                    interactKey=other.transform.parent.GetComponent<PlayerController>().interactKey;
                }
                //TO modify with the currect interaction
                InteractText.text = this.GetComponent<ObjectInteractedController>().CanInteract();
                
            }
        }
    }
   
    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.tag == "Player")
        {
            InteractText.text = "";
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.tag == "Player")
        {
            if (Input.GetKeyDown(interactKey)&&(firstInteraction))
            {
                //Fai qualcosa - to do
                firstInteraction = false;
                InteractText.text = "";
                Notifier.text = this.GetComponent<ObjectInteractedController>().Interact(other);
                //Debug.Log("ho interagito");
                Notifier.GetComponent<TextManager>().Refresh();
            }
            // Example Safe
            if(Input.GetKeyDown(interactKey) && (multipleInteractions))
            {
                //Fai qualcosa - to do
                InteractText.text = "";
                Notifier.text = this.GetComponent<ObjectInteractedController>().Interact(other);
                Notifier.GetComponent<TextManager>().Refresh();
            }
        }
    }

}
