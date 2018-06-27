using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Puzzle37 : MonoBehaviour
{
	public Image num1;
	public Image num2;
	public Image num37;
	public float distancex;
	public float distancey;
	public float distance;


	void Start(){
		num37.enabled=false;
	}

	void Update(){



		
		distance = Vector2.Distance(num1.transform.position,num2.transform.position);

		if(distance >= 40 && distance <=45.8 && num1.IsActive() && num2.IsActive()){

			num1.enabled=false;
			num2.enabled=false;
			num37.transform.position=num1.transform.position;
			num37.enabled=true;
		}
		

		
		/*distancex = num1.transform.position.x - num2.transform.position.x;
		distancey = num1.transform.position.y - num2.transform.position.y;

		if(distancex >= -48 && distancex <=-40 && distancey>=-5.5 && distancey <=3.5 && num1.IsActive() && num2.IsActive()){

			num1.enabled=false;
			num2.enabled=false;
			num37.transform.position=num1.transform.position;
			num37.enabled=true;
		}*/
			
			

		

	}

}