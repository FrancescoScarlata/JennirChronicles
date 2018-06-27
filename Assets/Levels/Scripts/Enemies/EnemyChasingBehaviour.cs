using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingBehaviour : _VigilantBehaviour
{

    [Space(12)]
    public bool goTowardTarget = false;
    [Range(0f, 100f)]
    public float speed = 4f;
    [Range(0f, 10f)]
    public float stopAt = 2f;

    [Range(0f, 10f)]
    public float waitForAlarm = 3;
    public float alertedWaitForAlarm = 1.5f;
    public bool melee;
    public float alertedRange= 15;
    public float alertedAngle = 200;
    public float alertedBlasterRange = 8;

    bool alarmRaised = false;

    bool hitted=false;

    private float alarmCooldown = 0;
    private Vector3 lastSeenPoint;

    private PatrolBehaviour pb;

    new void Start()
    {
        base.Start();
        pb = GetComponent<PatrolBehaviour>();
        SomeoneInside = Chase;
        NooneInside = NoChase;

        // 2 case: 1 at start and it's false. 2 when a wave is spawn and could be the boss near the cell or the waves for the alarm.
        CheckTheAlarm();
    }

    private void Chase(Transform t)
    {
        if (t == null)
            return;
        if (pb != null) pb.paused = true;
        if (sensingRange > 30)
        {
            if (lastSeenPoint != new Vector3())
            {
                if(transform.GetComponent<CellKeeperBehaviour>() != null)
                    NooneInside = GoToTheLastSeenPoint;
                sensingRange = alertedRange;
            }
            else
            {
                this.transform.Rotate(0, 30 * Time.deltaTime, 0);
                sensingRange -= 5 * Time.deltaTime;
            }
  
        }
        transform.LookAt(t);
        if (goTowardTarget && (transform.position - t.position).magnitude > stopAt)
        {
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }
        //Timer for the alarm raised
        if (alarmRaised)
        {
            if (alarmCooldown < alertedWaitForAlarm)
                alarmCooldown += Time.deltaTime;
            else
            {
                transform.root.Find("Bandits").GetComponent<AlarmController>().RaiseTheAlarm(t.position);
                alarmCooldown = 0;
                if (!alarmRaised)
                    AlarmRaised(t.position);
                else
                    lastSeenPoint = t.position;
            }
        }
        else
        {
            if (alarmCooldown < waitForAlarm)
                alarmCooldown += Time.deltaTime;
            else
            {
                transform.root.Find("Bandits").GetComponent<AlarmController>().RaiseTheAlarm(t.position);
                alarmCooldown = 0;
                if (!alarmRaised)
                    AlarmRaised(t.position);
                else
                    lastSeenPoint = t.position;
            }
        }      
    }

    private void GoToTheLastSeenPoint(Transform t)
    {
        if (pb != null)
            pb.paused = true;

        if(t!=null)
            if (goTowardTarget)
                agent.SetDestination(lastSeenPoint);
    }

    /*
      When the player is spotted by someone the alarm will be raised.
      Also, here can be put the switch to the Different behaviour
  */
    private void LookAround(Transform t)
    {
            this.transform.Rotate(0, 30 * Time.deltaTime, 0);
            sensingRange += 2 * Time.deltaTime;
     
    }

    private void NoChase(Transform t)
    {
        if (pb != null) pb.paused = false;
        fov = false;
    }

    private void CheckTheAlarm()
    {
        Transform t = transform.root.Find("Bandits");
        alarmRaised = t.GetComponent<AlarmController>().GetAlarm();
        if (alarmRaised)
        {
            AlarmRaised(t.GetComponent<AlarmController>().lastPointSeen);
        }
    }

    // should augment the sensing range and the angle
    public void Alerted()
    {
        sensingRange = alertedRange;
        fieldOfViewAngle = alertedAngle;
        if (lastSeenPoint == new Vector3())
        {
            if (transform.GetComponent<CellKeeperBehaviour>() == null)
            {
                NooneInside = LookAround;
            }  
            else
            {
                NooneInside = NoChase;
              //  Debug.Log("I should stay still." + transform.name);
            }   
        }    
        else
            if (transform.GetComponent<CellKeeperBehaviour>() == null)
                NooneInside = GoToTheLastSeenPoint;

        if (!melee)
        {
            this.GetComponent<NPCBlaster>().sensingRange = alertedBlasterRange;
        }
    }

    public void AlarmRaised(Vector3 position)
    {
        alarmRaised = true;
        Alerted();
       // Debug.Log("alarm!");
        this.lastSeenPoint = position;
        if (this.GetComponent<CellKeeperBehaviour>() == null)
        {
            // the cell keeper should stay at the cell
          //  Debug.Log("alarm raised"+transform.name);
            NooneInside = GoToTheLastSeenPoint;
            if(agent!=null)
                agent.destination = lastSeenPoint;
        
        }
        if(pb!=null)
            pb.paused = true;
        // switch to look for the player instead to NoChase
    }

    public void UpdatePosition(Vector3 position){
       // Debug.Log("Updated");
        if(agent!=null && this.GetComponent<CellKeeperBehaviour>() == null)
            agent.destination = lastSeenPoint;
        lastSeenPoint = position;
    }

    // hai to be aware he's been hitted and should look around
    public void Hitted()
    {
        hitted = true;
        //Debug.Log("Hitted");
        Alerted();
        if (pb != null)
            pb.paused = true;
    }
}