using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

//Chankeli chasing behaviour
public class ChasingBehaviour : _VigilantBehaviour {

	[Space(12)]
	public bool goTowardTarget = false;
	[Range(0f, 100f)] public float speed = 4f;
	[Range(0f, 10f)] public float stopAt = 2f;



	new void Start () {
		base.Start ();
		
		SomeoneInside = Chase;
		NooneInside = Teleport;
        transform.root.Find("Chase Sight").gameObject.SetActive(false);
    }

	private void Chase(Transform t) {
		if (t == null)
			return;
        Vector3 dirToLook= (t.position- transform.position);

        Quaternion rot = Quaternion.LookRotation(dirToLook, Vector3.up);
        transform.rotation = Quaternion.Euler(0, rot.eulerAngles.y, 0);

        if (Vector3.Distance(transform.position, t.position) > stopAt)
        {
            if (goTowardTarget)
            {
                agent.SetDestination(t.position);
            }
        }
		
	}

    // it's chankeli... teleport!
    private void Teleport(Transform t) {
        if (t == null)
            t=GetComponent<ChankeliController>().Player.transform;
        
        if (t!=null && Vector3.Distance(t.position, this.transform.position) > sensingRange)
        {
            this.transform.position = (t.position + new Vector3(1, 0, 1));
        }
    }
}