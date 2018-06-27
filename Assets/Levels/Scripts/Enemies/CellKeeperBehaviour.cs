using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellKeeperBehaviour : EnemyChasingBehaviour {



	// Use this for initialization
	new void Start () {
        base.Start();
    }
	
	// The cell keeper is not sleeping anymore
	public void WakeUp()
    {
        sensingRange = 5f;
        this.GetComponent<NPCBlaster>().sensingRange = 2f;
        this.GetComponent<StatsInfo>().HP = 14;
    }
}
