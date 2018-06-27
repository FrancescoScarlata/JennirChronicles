using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChankeliPlotColliderManager : MonoBehaviour {
    //From this, Chankeli will say something
    public int whatIHaveReached;

    public void OnTriggerEnter(Collider other)
    {
       if(other.transform.parent != null && other.transform.parent.tag == "Player")
        {
            ChankeliController ChankeliControl = other.transform.parent.GetComponent<PlayerController>().NPC_Chankeli.GetComponent<ChankeliController>();
            switch (whatIHaveReached)
            {
                case 4:
                    if (GameObject.Find("Drunk_bandits").transform.childCount!=0 && !GameObject.Find("Bandits").GetComponent<AlarmController>().GetAlarm())
                        ChankeliControl.SayPlotThings(whatIHaveReached);
                    break;

                case 8:
                    if (!GameObject.Find("Player").GetComponent<PlayerCollectiblesManager>().GetPrisonerFreed())
                        ChankeliControl.SayPlotThings(whatIHaveReached);
                    else
                        ChankeliControl.SayPlotThings(9);
                    break;

                default:
                    ChankeliControl.SayPlotThings(whatIHaveReached);
                    break;
            }
      
            this.gameObject.SetActive(false);
        }
    }

}
