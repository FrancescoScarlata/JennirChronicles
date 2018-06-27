using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerBouncerbarManager : MonoBehaviour {

    public RectTransform BouncerBar;

    const int maxBouncer = 400;
    const int bouncePerSec = 40;


    public void UseBouncer () {
        BouncerBar.offsetMax -= new Vector2((float)(bouncePerSec*2*Time.deltaTime), 0);
    }
	
	// Update is called once per frame
	public  void ChargeBouncer () {
        BouncerBar.offsetMax += new Vector2((float)(bouncePerSec*Time.deltaTime), 0);
    }
}
