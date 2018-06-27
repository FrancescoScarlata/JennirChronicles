using UnityEngine;
using System.Collections;
using UnityEngine.UI;


//Class where the information about the objects is kept
public class ObjectInteractedController : MonoBehaviour {
    // Typo of object interaction: es. "paper","weaponSafe", "chiefSafe"... "cell"?
    public string typeOfObject;

    public string stringToSayWhenInteractable="Interact";

    //First if just 1 interaction, +1 if there are multiple messages
    public string[] stringToSayWhenInteracted;


    //Object interactable
    public string CanInteract()
    {
        return stringToSayWhenInteractable;
    }

    //Interact with the object
    public string Interact(Collider other)
    {
        //Interaction for the paper
        if (typeOfObject == "paper")
        {
            other.transform.parent.GetComponent<PlayerCollectiblesManager>().CollectAPieceOfPaper();
            transform.parent.gameObject.SetActive(false);
        }
        // Interaction for the weaponSafe
        if (typeOfObject == "weaponSafe")
        {
            other.transform.parent.GetComponent<PlayerCollectiblesManager>().CollectAPieceOfWeapon();
            transform.parent.gameObject.SetActive(false);
        }
  
        if (typeOfObject == "puzzle")
        {
            int index = other.transform.parent.GetComponent<PlayerCollectiblesManager>().StartThePuzzle();
            //when interacts needs to finish the puzzle
            if (index == 0) // maybe to change
            {
                GetComponent<InteractibleObjectManager>().multipleInteractions = false; //non elegante ma smette di accedere all'interazione
                transform.parent.gameObject.SetActive(false);
            }
            return stringToSayWhenInteracted[index];
        }

        if (typeOfObject == "chiefSafe")
        {
            int index = other.transform.parent.GetComponent<PlayerCollectiblesManager>().OpenTheSafe();
            if (index == 0) //aperto la cassaforte
            {
                GetComponent<InteractibleObjectManager>().multipleInteractions = false; //non elegante ma smette di accedere all'interazione
                transform.parent.gameObject.SetActive(false);
            }
            return stringToSayWhenInteracted[index];
        }

        if (typeOfObject == "cell")
        {
            int index = other.transform.parent.GetComponent<PlayerCollectiblesManager>().OpenTheCell();
            Debug.Log(index);
            if (index == 0) //freed the prisoners
            {
                if(!transform.root.Find("Bandits").GetComponent<AlarmController>().GetAlarm())
                    transform.root.Find("Bandits").GetComponent<AlarmController>().FreeThePrisonersUndetected(); // tell the alarm that it's time to boss fight
                stringToSayWhenInteractable = "Talk to the prisoners";
                return stringToSayWhenInteracted[0];
            }
            if (index == 1)
            {
                if(other.transform.parent.GetComponent<PlayerCollectiblesManager>().GetPrisonerFreed())
                    return stringToSayWhenInteracted[0];

            }
            if (transform.root.Find("Bandits").GetComponent<AlarmController>().allWaveDead)
            {
                if (index == 3) //after freed the prisoners
                {
                    Debug.Log(index);

                    GameObject.Find("Messages").GetComponent<Text>().text = stringToSayWhenInteracted[index];
                    GameObject.Find("Messages").GetComponent<TextManager>().Refresh(stringToSayWhenInteracted[index].Length / 8);
                    Debug.Log(GameObject.Find("Messages"));
                   // GetComponent<InteractibleObjectManager>().multipleInteractions = false; //non elegante ma smette di accedere all'interazione
                    transform.parent.gameObject.SetActive(false);
                    GameObject.Find("Chankeli").GetComponent<ChankeliController>().setIndex(9);
                    return "You have talked to the prisoners";

                }
            }
            
            return stringToSayWhenInteracted[index];
        }
        if (typeOfObject == "cave")
        {
            int index = other.transform.parent.GetComponent<PlayerCollectiblesManager>().EnterInTheCave();
            if (index == 0) 
            {
                GetComponent<InteractibleObjectManager>().multipleInteractions = false; //non elegante ma smette di accedere all'interazione
                transform.parent.gameObject.SetActive(false);
                
            }
            return stringToSayWhenInteracted[index];
        }
        
        //If it hasn't other messages it is the only one needed
        return stringToSayWhenInteracted[0];
    }

}
