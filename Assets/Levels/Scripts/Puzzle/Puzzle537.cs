using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Puzzle537 : MonoBehaviour
{
	public Image num53;
	public Image num37;
	public Image num5;
	public Image num7;
	public Image num537;
	public float distance1x;
	public float distance1y;
	public float distance2x;
	public float distance2y;
	public float distance1;
	public float distance2;

	void Start(){
		num537.enabled=false;
	}

	void Update(){

		
		distance1 = Vector2.Distance(num53.transform.position,num7.transform.position);
		distance2 = Vector2.Distance(num37.transform.position,num5.transform.position);

		if(distance1 >= 78 && distance1 <=80 && num53.IsActive() && num7.IsActive()){

			num53.enabled=false;
			num7.enabled=false;
			num537.transform.position=num7.transform.position;
			num537.enabled=true;
		}

		if(distance2 >= 86 && distance2 <=91 && num37.IsActive() && num5.IsActive()){

			num37.enabled=false;
			num5.enabled=false;
			num537.transform.position=num5.transform.position;
			num537.enabled=true;
		}

		/*

		distance1x = num53.transform.position.x - num7.transform.position.x;
		distance1y = num53.transform.position.y - num7.transform.position.y;

		if(distance1x >= -80 && distance1x <=-73 && distance1y>=-5 && distance1y <=3 && num53.IsActive() && num7.IsActive()){

			num53.enabled=false;
			num7.enabled=false;
			num537.transform.position=num7.transform.position;
			num537.enabled=true;
		}

		distance2x = num37.transform.position.x - num5.transform.position.x;
		distance2y = num37.transform.position.y - num5.transform.position.y;

		if(distance2x >= 86 && distance2x <=91 && distance2y>=-1 && distance2y <=4.3 && num37.IsActive() && num5.IsActive()){

			num37.enabled=false;
			num5.enabled=false;
			num537.transform.position=num5.transform.position;
			num537.enabled=true;
		}

		*/


	}

}