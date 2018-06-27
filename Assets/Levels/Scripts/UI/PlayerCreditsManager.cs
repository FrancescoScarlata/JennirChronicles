using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCreditsManager : MonoBehaviour {

    public Text creditsT;

	public void AddCredits(int i)
    {
        creditsT.text = "Credits: " + i;
    }
	
}
