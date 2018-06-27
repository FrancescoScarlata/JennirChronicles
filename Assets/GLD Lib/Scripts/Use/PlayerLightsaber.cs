using UnityEngine;
using System.Collections;

public class PlayerLightsaber : MonoBehaviour {

	public bool active = true;
	public KeyCode lightsaberActivate = KeyCode.L;
    private KeyCode attackKey;

    //Bouncer
    public bool canBouncer = true;
    public KeyCode bouncerKey = KeyCode.Mouse1;
    public float maxTimeBouncer = 5;
    public float timeToRecharge = 10;
    private float bouncerCooldown=10;


    private LightsaberManager saber = null;
    private MeleeAttackManager attack = null;
    private BulletBouncer bouncer = null;

	void Start () {
		saber = GetComponentInChildren<LightsaberManager> ();
        attack= GetComponentInChildren<MeleeAttackManager>();
        bouncer = GetComponent<BulletBouncer>();
        attackKey = this.GetComponent<PlayerBlaster>().key;
	}
	
	void LateUpdate () {
        //activate /deactivate the saber
		if (saber && active && Input.GetKeyDown (lightsaberActivate)) {
			saber.active = !saber.active;
            this.GetComponent<PlayerBlaster>().active = !this.GetComponent<PlayerBlaster>().active;
            if (this.GetComponent<BulletBouncer>().active && saber.active == false)
                this.GetComponent<BulletBouncer>().active = false;
        }

        if (saber && active && saber.active && Input.GetKeyDown(attackKey))
        {
            attack.Attack();
            this.GetComponent<PlayerController>().InCombat();
        }
        // bouncer activated
        if (saber && active && saber.active && Input.GetKeyDown(bouncerKey) && bouncerCooldown>2)
        {
            //Also insert the visual
            bouncer.active=true;
            GetComponentInChildren<MeleeAttackManager>().Bounce(true);
        }

        if (saber && active && saber.active && Input.GetKey(bouncerKey) && bouncerCooldown>0.5f)
        {
            //GetComponentInChildren<MeleeAttackManager>().Bounce(true);
            // Has to check the time inside
            bouncerCooldown -= (2*Time.deltaTime);
            this.GetComponent<PlayerBouncerbarManager>().UseBouncer();
            if (bouncerCooldown <= 0)
            {
                bouncer.active = false;
                GetComponentInChildren<MeleeAttackManager>().Bounce(false);
            }
                
        }
        if (saber && active && saber.active && !Input.GetKey(bouncerKey) && bouncerCooldown<10)
        {
            // Also to insert the visual
            bouncerCooldown += Time.deltaTime;
            GetComponent<PlayerBouncerbarManager>().ChargeBouncer();
        }

            if (saber && active && saber.active && Input.GetKeyUp(bouncerKey))
        {
            bouncer.active = false;
            GetComponentInChildren<MeleeAttackManager>().Bounce(false);
        }








        }
}
