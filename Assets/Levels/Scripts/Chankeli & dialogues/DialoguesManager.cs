using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguesManager : MonoBehaviour {

    //Approach 1: one array with who has to talk and one with the dialogues
    public int[] orderToTalk;
    public string[] Dialogues;
    public Text Message;

    public int charPerSec = 8;
    private int i=0;

    public bool interactsWithC = false;
    public GameObject Chankeli;

    private bool interrupted = false;
    private bool inRange;
     
    public void StartAudio () {
        //Starts the dialogues between the bandits
        if (i < Dialogues.Length)
        {
            if (!interrupted)
            {
                Chankeli.GetComponent<ChankeliController>().setIndex(3);
                if (Message.text == "")
                    Message.text = "" + this.transform.GetChild(orderToTalk[i]).name + " : " + Dialogues[i];
                else
                    Message.text += "\n" + this.transform.GetChild(orderToTalk[i]).name + " : " + Dialogues[i];

                Message.GetComponent<TextManager>().Refresh(TimeToTalk(), this.gameObject);
                i++;
            }
        }
                
    }
	
    public void continueDialogue() // to make stop also if the player is found, the alarm raise or the npc is hitted
    {
        if (i < Dialogues.Length)
        {
            if (!interrupted)
            {
                if (inRange && this.transform.childCount!=0) // player can listen
                {
                    if (Message.text == "")
                        Message.text = "" + this.transform.GetChild(orderToTalk[i]).name + " : " + Dialogues[i];
                    else
                        Message.text += "\n" + this.transform.GetChild(orderToTalk[i]).name + " : " + Dialogues[i];

                    if (interactsWithC) // interacts with a plot phrase of chankeli
                    {
                        switch (i)
                        {
                            case 5: // case about pieces of paper
                                Chankeli.GetComponent<ChankeliController>().SayPlotThings(5);
                                Chankeli.GetComponent<ChankeliController>().setIndex(4);
                                break;

                            case 9: // when they say where to find the pieces
                                Chankeli.GetComponent<ChankeliController>().setIndex(5);
                                break;
                        }
                    }
                }

                if (i + 1 < orderToTalk.Length)
                {
                    Message.GetComponent<TextManager>().Refresh(TimeToTalk(), this.gameObject);
                    i++;
                }
                else
                    Message.GetComponent<TextManager>().Refresh(TimeToTalk());
            }
           
        }
    }

    float TimeToTalk()
    {
        if (Dialogues[i].Length / charPerSec < 1)
            return 1;
        else
            return Dialogues[i].Length / charPerSec;
    }

    public void InsideTheRange(bool range)
    {
        inRange = range;
    }

    public void StopTheConversation()
    {
        interrupted = true;

    }

}
