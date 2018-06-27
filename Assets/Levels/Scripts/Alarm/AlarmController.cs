using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmController : MonoBehaviour {

    public bool alarm=false; // became private and put a raiseAlarm()?

    public GameObject waveSpawnPoint; // where to spawn the waves

    public GameObject bossSpawnPoint; // where to spawn the boss with no alarm raised

    public GameObject[] banditsToSpawn; // where to get the bandits prefab

    public Vector3 lastPointSeen; // where to put the position of the bandits who gave the alarm

    public RectTransform alarmImage;
   
    //variables o know when the waves are finished
    int waveNumber=0;

    public bool allWaveDead=false; //They can open the cell if the waves are dead
    bool prisonersFreed=false;

	// Use this for initialization
	void Start () {	
        /* 
            To do:
            sentry
                - before the alarm, kills oneshot the player. (outside the gate)
                - after, they have the normal dmg and they'll look for the player
            cell guardian
                - wake up when the alarm is raised ( range augmented)

        */


	}
	
	// Update is called once per frame
	void LateUpdate () { //To know when if the player is still on sight
		
	}

    public void RaiseTheAlarm(Vector3 position)
    {
        if (!prisonersFreed) // if the prisoners are already free but without alarm, then you can't give the alarm
        {
           // Debug.Log("Alarm raised");
            EnemyChasingBehaviour[] bandits = this.GetComponentsInChildren<EnemyChasingBehaviour>();
            if (!alarm)
            {      
                Alarm();
                lastPointSeen = position;
                SpawnTheWaves();

                //Stop the talking around
                DialoguesManager [] dialogues= GetComponentsInChildren<DialoguesManager>();
                for (int i = 0; i < dialogues.Length; i++)
                    dialogues[i].StopTheConversation();
              
                // cell keeper
                if (this.transform.Find("Cells").Find("Cell_Keeper")!=null)
                    this.transform.Find("Cells").Find("Cell_Keeper").GetComponent<CellKeeperBehaviour>().WakeUp();
                
              //  Debug.Log(bandits.Length);
                for (int i = 0; i < bandits.Length; i++)
                {
                    bandits[i].AlarmRaised(lastPointSeen);
                  //  Debug.Log(bandits[i].name);
                }
            }
            else
            {
                if (Vector3.Distance(lastPointSeen, position) > 1)
                {
                    lastPointSeen = position;
                    // Update the bandits
                    for (int i = 0; i < bandits.Length; i++)
                    {
                        bandits[i].UpdatePosition(lastPointSeen);
                      //  Debug.Log(bandits[i].name);
                    }
                }
            }
        }
    }
    // return the alarm
    public bool GetAlarm()
    {
        return alarm;
    }

    //Spawn the enemies when the alarm is raised
    public void SpawnTheWaves() 
    {
        switch (waveNumber)
        {
            case 0:
                GameObject.Instantiate(banditsToSpawn[0], waveSpawnPoint.transform.position + (new Vector3(1, 0, 1)), new Quaternion(0, 90, 0, 1), this.transform.Find("Waves"));
                GameObject.Instantiate(banditsToSpawn[0], waveSpawnPoint.transform.position + (new Vector3(0, 0, 0)), new Quaternion(0, 90, 0, 1), this.transform.Find("Waves"));

                break;

            case 1:
                GameObject.Instantiate(banditsToSpawn[0], waveSpawnPoint.transform.position + (new Vector3(1, 0, 1)), new Quaternion(0, 90, 0, 1), this.transform.Find("Waves"));
                GameObject.Instantiate(banditsToSpawn[0], waveSpawnPoint.transform.position + (new Vector3(0, 0, 0)), new Quaternion(0, 90, 0, 1), this.transform.Find("Waves"));
                GameObject.Instantiate(banditsToSpawn[1], waveSpawnPoint.transform.position + (new Vector3(+1, 0, -1)), new Quaternion(0, 90, 0, 1), this.transform.Find("Waves"));

                break;

            case 2:
                GameObject.Instantiate(banditsToSpawn[0], waveSpawnPoint.transform.position + (new Vector3(1, 0, 1)), new Quaternion(0, 90, 0, 1), this.transform.Find("Waves"));
                GameObject.Instantiate(banditsToSpawn[0], waveSpawnPoint.transform.position + (new Vector3(0, 0, 0)), new Quaternion(0, 90, 0, 1), this.transform.Find("Waves"));
                GameObject.Instantiate(banditsToSpawn[1], waveSpawnPoint.transform.position + (new Vector3(2, 0, 2)), new Quaternion(0, 90, 0, 1), this.transform.Find("Waves"));
                GameObject.Instantiate(banditsToSpawn[1], waveSpawnPoint.transform.position + (new Vector3(+1, 0, -1)), new Quaternion(0, 90, 0, 1), this.transform.Find("Waves"));
                break;

            case 3:
                SpawnTheBoss();
                break;
        }
    }

    public void SpawnTheBoss() // spawn the boss when no alarm is raised
    {
        // Istantiate the boss and the 2 enemies
        if (prisonersFreed)
        {
            GameObject.Instantiate(banditsToSpawn[2], bossSpawnPoint.transform.position, new Quaternion(0, -30, 0, 1), this.transform.Find("Waves"));
            GameObject x = GameObject.Instantiate(banditsToSpawn[0], bossSpawnPoint.transform.position + (new Vector3(1, 0, 1)), new Quaternion(0, -30, 0, 1), this.transform.Find("Waves"));
            GameObject y = GameObject.Instantiate(banditsToSpawn[1], bossSpawnPoint.transform.position + (new Vector3(-1, 0, -1)), new Quaternion(0, -30, 0, 1), this.transform.Find("Waves"));
            x.GetComponent<EnemyChasingBehaviour>().Alerted();
            y.GetComponent<EnemyChasingBehaviour>().Alerted();
        }
        else
        {
            GameObject.Instantiate(banditsToSpawn[2], waveSpawnPoint.transform.position, new Quaternion(0, -30, 0, 1), this.transform.Find("Waves"));
            GameObject.Instantiate(banditsToSpawn[0], waveSpawnPoint.transform.position + (new Vector3(1, 0, 1)), new Quaternion(0, -30, 0, 1), this.transform.Find("Waves"));
            GameObject.Instantiate(banditsToSpawn[1], waveSpawnPoint.transform.position + (new Vector3(-1, 0, -1)), new Quaternion(0, -30, 0, 1), this.transform.Find("Waves"));
        }
    }

     /*
        If are all dead (meaning the are all null), then you have to call the reinforcements
        the check is done before a bandit is going to die
     */
    public void CheckTheWave() // if are all dead (meaning the are all null), then you have to call the reinforcements
    {
        //if =1 it means that the last one is dying
        if (this.transform.Find("Waves").childCount > 1)
            return;
        //all dead, next wave
        if (waveNumber < 3) //the last is the boss
        {
            waveNumber++;
            SpawnTheWaves();
        }
        else
        {
            alarmImage.gameObject.SetActive(false);
            allWaveDead = true; //  
        }
              
    }

    public void FreeThePrisonersUndetected()
    {
        prisonersFreed = true;
        waveNumber = 3;
            SpawnTheBoss();
    }

    public void Alarm()
    {
        alarm = true;
        alarmImage.gameObject.SetActive(true);
    }

}
