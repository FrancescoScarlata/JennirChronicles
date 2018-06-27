using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Puzzle370 : MonoBehaviour
{
	public Image num70;
	public Image num37;
	public Image num3;
	public Image num0;
	public Image num370;
	public float distance1x;
	public float distance1y;
	public float distance2x;
	public float distance2y;
	public float distance1;
	public float distance2;


	void Start(){
		num370.enabled=false;
	}

	void Update(){

		
		distance1 = Vector2.Distance(num70.transform.position,num3.transform.position);
		distance2 = Vector2.Distance(num37.transform.position,num0.transform.position);

		if(distance1 >= 52 && distance1 <=55 && num70.IsActive() && num3.IsActive()){

			num70.enabled=false;
			num3.enabled=false;
			num370.transform.position=num3.transform.position;
			num370.enabled=true;
		}

		if(distance2 >=59 && distance2 <=63 && num37.IsActive() && num0.IsActive()){

			num37.enabled=false;
			num0.enabled=false;
			num370.transform.position=num0.transform.position;
			num370.enabled=true;
		}

		/*
		distance1x = num70.transform.position.x - num3.transform.position.x;
		distance1y = num70.transform.position.y - num3.transform.position.y;

		if(distance1x >= 56 && distance1x <=63 && distance1y>=2.5 && distance1y <=7.5 && num70.IsActive() && num3.IsActive()){

			num70.enabled=false;
			num3.enabled=false;
			num370.transform.position=num3.transform.position;
			num370.enabled=true;
		}

		distance2x = num37.transform.position.x - num0.transform.position.x;
		distance2y = num37.transform.position.y - num0.transform.position.y;

		if(distance2x >=-65 && distance2x <=-58 && distance2y>=-7 && distance2y <=0.5 && num37.IsActive() && num0.IsActive()){

			num37.enabled=false;
			num0.enabled=false;
			num370.transform.position=num0.transform.position;
			num370.enabled=true;
		}

		*/

	}

}