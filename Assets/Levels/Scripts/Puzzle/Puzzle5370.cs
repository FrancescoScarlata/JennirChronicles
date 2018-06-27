using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Puzzle5370 : MonoBehaviour
{
	public Image num70;
	public Image num53;
	public Image num537;
	public Image num0;
	public Image num370;
	public Image num5;
	public Image num5370;
	public float distance1x;
	public float distance1y;
	public float distance2x;
	public float distance2y;
	public float distance3x;
	public float distance3y;
	public float distance1;
	public float distance2;
	public float distance3;


	void Start(){
		num5370.enabled=false;
	}

	void Update(){
		
		
		distance1 = Vector2.Distance(num70.transform.position,num53.transform.position);
		distance2 = Vector2.Distance(num537.transform.position,num0.transform.position);
		distance3 = Vector2.Distance(num370.transform.position,num5.transform.position);


		if(distance1 >= 92 && distance1 <=95 && num70.IsActive() && num53.IsActive()){

			num70.enabled=false;
			num53.enabled=false;
			num5370.enabled=true;
		}

		if(distance2 >=73 && distance2 <=78 && num537.IsActive() && num0.IsActive()){

			num537.enabled=false;
			num0.enabled=false;
			num5370.enabled=true;
		}

		if(distance3 >=84 && distance3 <=87 && num370.IsActive() && num5.IsActive()){

			num370.enabled=false;
			num5.enabled=false;
			num5370.enabled=true;
		}
		
		/*
		distance1x = num53.transform.position.x - num70.transform.position.x;
		distance1y = num53.transform.position.y - num70.transform.position.y;

		if(distance1x <= -92 && distance1x >=-95 && distance1y<=-2.5 && distance1y >=-7.5 && num70.IsActive() && num53.IsActive()){

			num70.enabled=false;
			num53.enabled=false;
			num5370.enabled=true;
		}

		distance2x = num537.transform.position.x - num0.transform.position.x;
		distance2y = num537.transform.position.y - num0.transform.position.y;

		if(distance2x <=-72 && distance2x >=-82 && distance2y>=-7 && distance2y <=-0.5 && num537.IsActive() && num0.IsActive()){

			num537.enabled=false;
			num0.enabled=false;
			num5370.enabled=true;
		}

		distance3x = num370.transform.position.x - num5.transform.position.x;
		distance3y = num370.transform.position.y - num5.transform.position.y;

		if(distance3x <=87 && distance3x >=84 && distance3y<=7 && distance3y >=-0.5 && num370.IsActive() && num5.IsActive()){

			num370.enabled=false;
			num5.enabled=false;
			num5370.enabled=true;
		}*/


	}

}