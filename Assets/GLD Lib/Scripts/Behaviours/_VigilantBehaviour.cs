using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public delegate void VigilanceAction (Transform t);

public class _VigilantBehaviour : MonoBehaviour {

	public bool active = false;
	[HideInInspector] public bool paused = false;

	[Header("Looking for")]
	public GameObject target = null;
	public string targetTag = "Player";

	[Space(12)]
	[Range(0.01f, 200f)] public float sensingRange = 10f;
	private float oldRange = 0f;
	[Range(0f, 5f)] public float reactionTime = 0f;
	public bool needLineOfSight = true;
	[Range(0f, 1f)] public float heightOfSight = .95f;
	public Color circleColor = new Color(0f, 0f, 1f, .5f);

    public float fieldOfViewAngle = 140f;

    protected VigilanceAction SomeoneInside = null;
	protected VigilanceAction NooneInside = null;
	protected VigilanceAction EveryUpdate = null;

	private GameObject circle;
	private SightCircleManager scm;
	private Vector3 PoV;
	private float reactionAt = 0f;
    protected bool fov = false; // what is this?

    protected NavMeshAgent agent;


	protected void Start () {
        
		circle = (GameObject)Instantiate (Resources.Load ("Internals/SightCircle"), transform.position + new Vector3(0f, 0.03f, 0f), transform.rotation * Quaternion.Euler (new Vector3 (90f, 0f, 0f)));
		circle.name = "Chase Sight";
        circle.transform.parent = this.transform; // if taken away, for some reason the circle is instantiated not under the npc.
		scm = circle.GetComponent<SightCircleManager> ();
		scm.SetColor (circleColor);
		scm.SetScale (sensingRange);

        agent = GetComponent<NavMeshAgent>();

		HideInfo[] hii = transform.root.GetComponentsInChildren<HideInfo> (true);
		foreach (HideInfo hi in hii) {
			if (hi.hideMe && !hi.transform.gameObject.activeInHierarchy)
				scm.active = false;
		}
		PoV = new Vector3 (0f, transform.root.localScale.y * heightOfSight, 0f);
	}

	protected void LateUpdate () {
		if (active && (target != null || targetTag != "")) {
			if (EveryUpdate != null)
				EveryUpdate (null);
			RangeOn ();
			if (sensingRange != oldRange) {
				scm.SetScale (sensingRange);
				oldRange = sensingRange;
			}

			Transform t = ClosestInRange (sensingRange);
			if (t != null) {
				if (reactionAt == 0f)
                    reactionAt = Time.time + reactionTime;
				if (SomeoneInside != null && Time.time >= reactionAt)
                    SomeoneInside (t);
			}
            else {
				reactionAt = 0f;
				if (NooneInside != null) {
					NooneInside (null);
				}
			}
		} else {
			RangeOff ();
		}
	}

	public Transform ClosestInRange(float r) { // to fix
		RaycastHit h;
		Transform target = null;
		float distance = r;
		Collider[] cc = Physics.OverlapSphere (transform.position, r);
		foreach (Collider c in cc) {
			Transform t = c.transform;
            Vector3 direction = t.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
          
            if (( angle < fieldOfViewAngle * 0.5f) && (t.root.tag == targetTag || t.root == target)) { // it was (fov|| angle...) see below
               
               // fov = true;         //problem : it stays true even if he's too far or the angle is wrong
				if (!needLineOfSight || Physics.Linecast (transform.position + PoV,t.position /*direction.normalized*/, out h) && h.transform == t.root) {
					float d = (transform.position - t.position).magnitude;
                    
                    if (target == null || distance > d) {
						target = t.root.Find("Base").transform;
						distance = d;
					}
                    else
                    {
                        if (distance < d)
                            fov = false;
                    }
				}
			}
            /*if (t.root.tag == targetTag || t.root == target) {
				if (!needLineOfSight || Physics.Linecast (transform.position + PoV, t.position, out h) && h.transform == t.root) {
					float d = (transform.position - t.position).magnitude;
					if (target == null || distance > d) {
						target = t.root;
						distance = d;
					}
				}
			}*/
        }
        return target;
	}

	private bool rangeIsVisible = false;

	private void RangeOn() {
		if (!rangeIsVisible) {
			scm.On ();
			scm.SetScale (sensingRange);
			rangeIsVisible = true;
		}
	}

	private void RangeOff() {
		if (rangeIsVisible) {
			scm.Off ();
			rangeIsVisible = false;
		}
	}
}