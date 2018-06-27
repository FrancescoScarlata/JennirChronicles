using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChankeliController : MonoBehaviour {

    //Player
    public GameObject Player;

    //Message Text
    public Text MessageText;

    //Crouch declaration 
    public float crouchHeight = 1f;
    public float crouchSpeed = 2f;
    public float notCrouchHeight = 1.7f;
    public float notCrouchSpeed = 4f;

    private bool isCrouched = false;
   
    //Strings with the dialogues
    public string[] dialoguesInteractions; // the ones used when the Player interacts with Chankeli
    public string[] plotDialogues; // The ones Chankeli tells without asking
    public int charPerSec=8;

    // evoked from the player when he crouch
    public void Crouch() {
        if (!isCrouched)
        {
            this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, crouchHeight, gameObject.transform.localScale.z);
            this.GetComponent<ChasingBehaviour>().speed = crouchSpeed;
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, notCrouchHeight, gameObject.transform.localScale.z);
            this.GetComponent<ChasingBehaviour>().speed = notCrouchSpeed;
        }
        isCrouched = !isCrouched;
    }

    // When the combat starts
    public void Diseppear()
    {
        this.transform.Find("Collider").gameObject.SetActive(false);
    }

    // When the combat ends
    public void Reappear()
    {
        this.transform.Find("Collider").gameObject.SetActive(true);
    }


    // Write whe message when the player interacts with Chankeli
    public void HelpMe(int i) 
    {
        if (MessageText.text == "")
            MessageText.text= "Chankeli: "+ dialoguesInteractions[i];
        else
        {
            if(MessageText.text.Contains("Chankeli:")) // magari da migliorare
                MessageText.text = "Chankeli: " + dialoguesInteractions[i];
            else
                MessageText.text += '\n'+"Chankeli: " + dialoguesInteractions[i];
        }
        switch (i) // non so se serva ancora
        {          
            //Casi in base dove e' arrivato nell'ascolto
            default:
                MessageText.GetComponent<TextManager>().Refresh(TimeToTalk(i));
                break;
        }
    }

    public void SayPlotThings(int i)
    {
        //Talk but update the player progress too
        if(MessageText.text=="")
            MessageText.text = "Chankeli: " + plotDialogues[i];
        else
        {
            if (MessageText.text.Contains("Chankeli:")) // magari da migliorare
                MessageText.text = "Chankeli: " + plotDialogues[i];
            else
                MessageText.text += '\n' + "Chankeli: " + plotDialogues[i];
        }        
        switch (i)
        {
            case 1:
                MessageText.GetComponent<TextManager>().Refresh(TimeToTalk(i));
                Player.GetComponent<PlayerController>().whereAmI = 1;
                break;
            case 2:
                MessageText.GetComponent<TextManager>().Refresh(TimeToTalk(i));
                Player.GetComponent<PlayerController>().whereAmI = 2;
                break;
            case 6:
                MessageText.GetComponent<TextManager>().Refresh(TimeToTalk(i));
                Player.GetComponent<PlayerController>().whereAmI = 6;

                break;
            default:
                MessageText.GetComponent<TextManager>().Refresh(TimeToTalk(i));
                break;
        }        
    }

    float TimeToTalk(int i)
    {
        if ((plotDialogues[i].Length) / charPerSec < 1)
            return 1;
        else
            return (plotDialogues[i].Length )/ charPerSec;
    }

    public void setIndex(int i) //Put the direction in what has to say chankeli when interacted
    {
        if(Player.GetComponent<PlayerController>().whereAmI<i)
            Player.GetComponent<PlayerController>().whereAmI = i;

    }
}
