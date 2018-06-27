using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public class PlayerDeathController : MonoBehaviour {


    // Death - when the player's character reaches 0 hp, he dies. When he dies, he'll start again the level
    public bool canDie = true;
    public Text deathMessage;
    public Button retry;

    //// Die-  When he dies, he'll start again the level
    public void Die()
    {
        deathMessage.text = "You have been defeated." + '\n' + "Would you like to retry?";
        
        retry.gameObject.SetActive(true);
        // non si può muovere più
        this.GetComponent<PlayerController>().active = false;
    }

    public void LevelFinished()
    {
        deathMessage.text = "You finished the Level! Congrats." + '\n' + "Would you like to restart the level?";

        retry.gameObject.SetActive(true);
        // non si può muovere più
        this.GetComponent<PlayerController>().active = false;
    }

    public void Discovered()
    {
        deathMessage.text = "You have been spotted before enter the camp!" + '\n' + "Would you like to restart the level?";
        retry.gameObject.SetActive(true);
        // non si può muovere più
        this.GetComponent<PlayerController>().active = false;
    }


}
