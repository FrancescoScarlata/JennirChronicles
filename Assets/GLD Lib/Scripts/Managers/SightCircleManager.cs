using UnityEngine;
using System.Collections;

public class SightCircleManager : MonoBehaviour {

	public bool active = true;

	private SpriteRenderer sr;

	void Start() {
		sr = GetComponent<SpriteRenderer> ();
        //active = true; // it doesn't stay true when it starts
	}

	public void SetScale(float f) {
		if (active) {
			Vector3 refScale = transform.parent.localScale;
           transform.localScale = new Vector3 (f * 2f / refScale.x, f * 2f / refScale.z, 0f);
		}
	}

	public void On() {
		if (active && sr) sr.enabled = true;
	}

	public void Off() {
		if (active && sr) sr.enabled = false;
	}

	public void SetColor(Color c) {
		GetComponent<SpriteRenderer> ().color = c; // FIXME ?
	}
}