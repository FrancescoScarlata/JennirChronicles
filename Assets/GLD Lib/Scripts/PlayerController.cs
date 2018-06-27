using UnityEngine;

public class PlayerController : MonoBehaviour {

	public bool active = true;

    public GameObject NPC_Chankeli;

    [Range(0.0f, 20.0f)] public float movementSpeed = 4f;
	[Range(0.0f, 180.0f)] public float rotationSensitivity = 90f;

    /*
    //Space declaration
	[Space(12)]
	public bool canJump = true;
	public KeyCode jumpKey = KeyCode.Space;
	[Range(0.0f, 10.0f)] public float jumpHeight = 1.5f;
    */

    //Crouch declaration
    public bool canCrouch = true;
    public KeyCode crouchKey = KeyCode.C;
    public float crouchHeight = 1f;
    public float crouchSpeed = 2f;
    public float notCrouchHeight = 1.8f;
    public float notCrouchSpeed = 4f;
    private bool isCrouched = false;


    float camRayLength = 100f;
    Vector3 movement;

    //Interact declaration
    public KeyCode interactKey = KeyCode.E;

    //medikit user
    public bool canUseMedikit = true;
    public KeyCode HealKey = KeyCode.F;
    public int maxMedikitUser = 7; // max amount of medikit portable
    [Range(1, 7)] public int currentAmountMedikit = 3; 
    [Range(1,10)]  public int amountHeal = 5;
    private int maxHealth;

    

    //interact with Chankeli
    public bool canTalkWithPatner = true;
    public KeyCode TalkKey = KeyCode.T;
    //This way or with a method "status update"
    public int whereAmI = 0;

    // Disappear
    public float timeToReapper = 4;
    private bool inCombat;
    private float combatCooldown = 0;

    private int credits = 0;

    private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
        maxHealth = this.GetComponent<StatsInfo>().HP;
	}
	
	void Update () {
		if (active) {
			if (rb)
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                movement.Set(h, 0f, v);
                movement = movement.normalized * movementSpeed * Time.deltaTime;

                rb.MovePosition(transform.position + transform.rotation* movement);
                rb.MoveRotation(Quaternion.Euler(0.0f, rotationSensitivity * (Input.GetAxis("Horizontal") * Time.deltaTime), 0.0f) * transform.rotation);
            } else
            {
                
                 transform.Translate (Vector3.forward * movementSpeed * (Input.GetAxis ("Vertical") * Time.deltaTime));
                 transform.Rotate (0.0f, rotationSensitivity * (Input.GetAxis ("Horizontal") * Time.deltaTime), 0.0f, Space.World);
            }
            /*
            // Jump part
			if (canJump && Input.GetKeyDown (jumpKey)) {
				if (rb) rb.MovePosition (transform.position + Vector3.up * jumpHeight);
				else transform.Translate (jumpHeight * Vector3.up);
			}
            */
            // Crouch part
            if (canCrouch && Input.GetKeyDown(crouchKey))
            {
                if (!isCrouched)
                {
                    this.gameObject.transform.Find("Collider").localScale = new Vector3(gameObject.transform.Find("Collider").localScale.x, crouchHeight, gameObject.transform.Find("Collider").localScale.z);
                    this.gameObject.transform.Find("Base").localPosition = new Vector3(this.gameObject.transform.Find("Base").localPosition.x, this.gameObject.transform.Find("Base").localPosition.y + 0.4f, this.gameObject.transform.Find("Base").localPosition.z);
                    movementSpeed = crouchSpeed;
                } 
                else
                {
                    this.gameObject.transform.Find("Collider").localScale = new Vector3(this.gameObject.transform.Find("Collider").localScale.x, notCrouchHeight, this.gameObject.transform.Find("Collider").localScale.z);
                    this.gameObject.transform.Find("Base").localPosition = new Vector3(this.gameObject.transform.Find("Base").localPosition.x, this.gameObject.transform.Find("Base").localPosition.y - 0.4f, this.gameObject.transform.Find("Base").localPosition.z);
                    movementSpeed = notCrouchSpeed;
                }
                NPC_Chankeli.GetComponent<ChankeliController>().Crouch(); 
                isCrouched = !isCrouched;
            }

            //Healing part
            if(canUseMedikit && Input.GetKeyDown(HealKey))
                Heal();


            //Talking part
            if (canTalkWithPatner && Input.GetKeyDown(TalkKey))
                Talk();
            

            //Disapper part
            if (inCombat)
            {
                if(combatCooldown>timeToReapper)
                {
                    combatCooldown = 0;
                    bool alarm = GameObject.Find("Bandits").GetComponent<AlarmController>().GetAlarm(); // Da inserire il vero riferimento
                    bool waveDead = GameObject.Find("Bandits").GetComponent<AlarmController>().allWaveDead; // Da inserire il vero riferimento

                    if (!alarm || waveDead)
                    {
                        OutOfCombat();
                       // Debug.Log(alarm + " " + waveDead);
                    }
                        
                }
                combatCooldown += Time.deltaTime;
            }
        }
	}

    void Heal()
    {
        int currentHealth = GetComponent<StatsInfo>().HP;
        if ( currentHealth< maxHealth)
        {
            if (currentAmountMedikit > 0)
            {
                int tmp;
                currentAmountMedikit--;
                tmp = currentHealth + amountHeal;
                if (tmp> maxHealth)
                {
                    GetComponent<StatsInfo>().HP = maxHealth;
                    tmp-= maxHealth;
                }
                else
                {
                    GetComponent<StatsInfo>().HP = currentHealth + amountHeal;
                    tmp = amountHeal;
                }
                GetComponent<PlayerHealthManager>().UseMedikit(tmp);
                GetComponent<PlayerHealthManager>().ChangeMedikitNumber(currentAmountMedikit);

            }
        }
    }

    // Interacts with Chankeli
    void Talk()
    {
        NPC_Chankeli.GetComponent<ChankeliController>().HelpMe(whereAmI);
    }


    public void InCombat() // to tell to chankeli to hide
    {
        inCombat = true;
        NPC_Chankeli.GetComponent<ChankeliController>().Diseppear();
    }

    public void OutOfCombat() // to tell to chankeli to reappear
    {
        inCombat = false;
        NPC_Chankeli.GetComponent<ChankeliController>().Reappear();
    }

    // Take the medikits. Maybe also a image (a +1?)
    public void CollectMedikit(int i)
    {
        if (i + currentAmountMedikit > maxMedikitUser)
        {
            currentAmountMedikit = maxMedikitUser;
        }
        else
            currentAmountMedikit += i;
        GetComponent<PlayerHealthManager>().ChangeMedikitNumber(currentAmountMedikit);
    }

    //Take the credits
    public void CollectCredits(int i)
    {
        credits += i;
        //Debug.Log(i);
        this.GetComponent<PlayerCreditsManager>().AddCredits(credits);
    }
}
