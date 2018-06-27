using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleButtonController : MonoBehaviour {

    public GameObject CompletedImage;
    public GameObject FPCamera;
	
	public void Close () {
		
        if(CompletedImage.GetComponent<Image>().IsActive())
        {
            // the player has completed the combination
            this.GetComponent<PuzzleManager>().Player.GetComponent<PlayerController>().NPC_Chankeli.GetComponent<ChankeliController>().setIndex(7);
            this.GetComponent<PuzzleManager>().Player.GetComponent<PlayerCollectiblesManager>().PuzzleSolved();
        }
        this.GetComponent<PuzzleManager>().Player.GetComponent<PlayerController>().active = true;
        this.GetComponent<PuzzleManager>().Player.GetComponent<PlayerBlaster>().active = true;

        FPCamera.GetComponent<SmoothMouseLook>().enabled = true;
        this.gameObject.SetActive(false);
       
    }
}
