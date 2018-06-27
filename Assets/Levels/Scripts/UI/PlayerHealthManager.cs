using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthManager : MonoBehaviour {

    const int maxHealth = 500;
    const int onePointDamage = 25; //500/25

    public RectTransform healthBar;
    public Text numberMedikit;
    public Image MedikitImage;

    // Use this for initialization
    public void TakeDamage (int i) {
        healthBar.offsetMax -=new Vector2( (float)(25 * i),0);
	}

    public void UseMedikit(int i)
    {
        healthBar.offsetMax += new Vector2((float)(25 * i), 0);
    }

    public void ChangeMedikitNumber(int i)
    {
        if (numberMedikit.text.Contains( "0"))
        {
            ChangeMedikitImage(false);
        }

        numberMedikit.text = ""+i;

        if (i == 0)
        {
            ChangeMedikitImage(true);
 
        }
    }


    private void ChangeMedikitImage(bool isEmpty)
    {
        if (isEmpty)
            MedikitImage.color = Color.red;
        else
            MedikitImage.color = Color.green;
    
    }

}
