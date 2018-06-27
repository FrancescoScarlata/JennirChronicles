using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextManager : MonoBehaviour {
    //Default cooldown
    public float refreshCooldown = 3f;

    public float cooldown=0;
    private bool needRefresh=false;
    private float tempRefreshCooldown=3;
    public GameObject[] dialogues=new GameObject[10];
    private int dialogueQueue = 0;
    private bool oneforLate = true;

	// Update is called once per frame
	void LateUpdate () {
        if (needRefresh)
        {
            if (tempRefreshCooldown != refreshCooldown)
            {
                cooldown += Time.deltaTime;
                if (cooldown > tempRefreshCooldown)
                {
                    this.GetComponent<Text>().text = "";
                    cooldown = 0;
                    needRefresh = false;

                    tempRefreshCooldown = refreshCooldown;
                    if (oneforLate && dialogues[dialogueQueue] != null)
                    {
                        oneforLate = false;                    
                        dialogues[dialogueQueue].GetComponent<DialoguesManager>().continueDialogue();
                        dialogues[dialogueQueue] = null;
                        dialogueQueue = (dialogueQueue + 1) % dialogues.Length;
                    }
                }
            }
            else
            {
                cooldown += Time.deltaTime;
                if (cooldown > refreshCooldown)
                {
                    this.GetComponent<Text>().text = "";
                    cooldown = 0;
                    needRefresh = false;
                }
            }       
        }
	}

    public void Refresh()
    {
        cooldown = 0;
        needRefresh = true;
    }

    public void Refresh(float i)
    {      
        if (!needRefresh)
        {
            tempRefreshCooldown = i;
            cooldown = 0;
            needRefresh = true;
        }
        else
        {
            //The remaining time of cooldown is lesser than the new cool down time, so it has to wait, but not add to the old cooldown
            // if the new is instead lesser than the old, it has to do nothing, because at the end it will finish before the other.
            if((tempRefreshCooldown-(int)cooldown)<i)
            {
                cooldown = 0;
                tempRefreshCooldown = i;
            }
        }
    }

    public void Refresh (float i,GameObject element)
    {
        Refresh(i);
        int j=dialogueQueue;
        while (dialogues[j] != null)
        {    
            j = (int)((j + 1) % dialogues.Length);
        }
           
        if (dialogues[j] == null)
        {
            dialogues[j] = element;
            oneforLate = true;
        }
    }
}
