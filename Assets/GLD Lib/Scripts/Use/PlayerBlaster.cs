using UnityEngine;
using System.Collections;

public class PlayerBlaster : MonoBehaviour {

	public bool active = true;
	public KeyCode key = KeyCode.Mouse0;
    public float reloadTime = 1f;
	private LinearFireGenerator lfg = null;
    private float cooldown;
    private Camera camera;

	void Start () {
        camera = GetComponentInChildren<Camera>();
		lfg = GetComponentInChildren<LinearFireGenerator> ();
        cooldown = 0;
	}
	
	void Update () {
        cooldown += Time.deltaTime;
		if (lfg && active && Input.GetKeyDown (key)&&cooldown>reloadTime) {

            lfg.Fire(GameObject.FindGameObjectWithTag("CrossHair").transform);   
            cooldown = 0;
            this.GetComponent<PlayerController>().InCombat();
        }
	}
}
