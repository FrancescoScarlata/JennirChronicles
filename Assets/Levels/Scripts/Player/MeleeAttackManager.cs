using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackManager : MonoBehaviour
{

    //public AnimationClip AttackAnimation;
    [Range(0f, 10f)]
    public float cooldown;
    private float timeToWait = 0;
    private bool firstAttack;
    public float goBackToIdle = 2f;

    void Start()
    {
     
        firstAttack = true;
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (goBackToIdle > timeToWait)
            timeToWait += Time.deltaTime;
        else
        {
            if (timeToWait > goBackToIdle && !GetComponent<Animation>().IsPlaying("BouncerStance"))
            {
                Idle();
            }               
        }          
    }

    public void Attack()
    {
        this.transform.Find("Saber").GetComponent<Collider>().enabled = true;
        if (timeToWait > cooldown)
        {
          
            if (firstAttack)
                this.GetComponent<Animation>().Play("Attack1");
            else
                this.GetComponent<Animation>().Play("Attack2");

            firstAttack = !firstAttack;
            timeToWait = 0;
        }           
    }

    public void Bounce(bool active)
    {
        if (active)
        {
            this.GetComponent<Animation>().Play("BouncerStance");
        }
        else
            Idle();
    }

    public void Idle()
    {
        this.transform.Find("Saber").GetComponent<Collider>().enabled = false;
        this.GetComponent<Animation>().Play("Idle");
    }
}
