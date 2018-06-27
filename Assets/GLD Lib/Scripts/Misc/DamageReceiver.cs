using UnityEngine;
using System.Collections;

public class DamageReceiver : MonoBehaviour {

	public bool active = true;
    public bool isPlayer = false;
	private StatsInfo si;

	void Start() {
		si = GetComponent<StatsInfo> ();
	}

	public void ReceiveDamage(int i) {
		if (active && si != null) {
			si.HP -= i;

            //Be aware that you have been hitted - do something, bot
            if (!isPlayer)
            {
                GetComponent<EnemyChasingBehaviour>().Hitted();
            }
            else
            {
                this.GetComponent<PlayerHealthManager>().TakeDamage(i);
                this.GetComponent<PlayerController>().InCombat();
            }

			if (si.HP <= 0)
            {
                if (!isPlayer)
                {
                    //Reward to the player
                    Reward();

                    // check for the state of the wave
                    if (transform.parent.name == "Waves")
                        transform.parent.parent.GetComponent<AlarmController>().CheckTheWave();

                    Destroy(this.transform.gameObject);
                }
                    
                else
                {
                    if (this.GetComponent<PlayerDeathController>().canDie)
                        this.GetComponent<PlayerDeathController>().Die();
                }   
            }
		}
	}


    private void Reward()
    {
        /* 
            give a reward in any case. 
            for melee and chief is also medikit
        */
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (this.GetComponent<EnemyChasingBehaviour>().melee && !this.name.Contains("Chieftain_Bandit")) // not a ranged/sentry or the cell keeper
        {
            //Put the reward: 100% 5 credit; 5% 1 medikit
            Player.GetComponent<PlayerController>().CollectCredits(5);
            if(Random.Range(1,100)<6)
                Player.GetComponent<PlayerController>().CollectMedikit(1);
        }
        if (!this.GetComponent<EnemyChasingBehaviour>().melee  && !this.name.Contains("Sentry"))
        {
            //Put the Reward : 3 credits
            Player.GetComponent<PlayerController>().CollectCredits(3);
        }
        if (this.name.Contains("Sentry_Bandit"))
        {
            //Put the Reward : 7 credits
            Player.GetComponent<PlayerController>().CollectCredits(7);
        }
        if (this.name.Contains("Chieftain_Bandit"))
        {
            //Put the Reward : 100% 20 credits ; 10% 3 medikit ; 5% 5 medikit
            Player.GetComponent<PlayerController>().CollectCredits(20);
            if (Random.Range(1, 100) < 11)
                Player.GetComponent<PlayerController>().CollectMedikit(3);
            if (Random.Range(1, 100) < 6)
                Player.GetComponent<PlayerController>().CollectMedikit(5);
        }
    }

}
