using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{

	public GameObject Player;
    public GameObject Camera;
    bool blaster;
    // Show the UI of the puzzle, blocks the player and blocks the camera
    public void ShowThePuzzle(bool weapon)
    {
        gameObject.SetActive(true);

        Player.GetComponent<PlayerController>().active = false;
        if (weapon)
            Player.GetComponent<PlayerBlaster>().active = false;
        else
        {
            Player.GetComponentInChildren<LightsaberManager>().active=false;
        }
            
        Camera.GetComponent<SmoothMouseLook>().enabled = false;

    }
    
}