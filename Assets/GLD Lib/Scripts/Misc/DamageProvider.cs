﻿using UnityEngine;
using System.Collections;

public class DamageProvider : MonoBehaviour {

	public bool active = true;

	public int multiplicity = 1;
	public int dice = 6;
	public int bonus = 0;
    //Da modificare con anche il valore del miss (tac0 ecc)
	public void ProvideDamage(Transform t) {
		if (active) {
			DamageReceiver dr = t.GetComponentInChildren<DamageReceiver> ();
            
			if (dr != null) {
				int dmg = 0;

				for (int i = 0; i < multiplicity; i += 1)
					dmg += Random.Range (1, dice);
				dmg += bonus;
				dr.ReceiveDamage (dmg);
			}
		}
	}

}
