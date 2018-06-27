using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairPosition : MonoBehaviour {
    Camera camera;

    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>();
    }


    // Update is called once per frame
    void Update()
    {

        /*Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        transform.LookAt(hit.transform);
        transform.position = hit.transform.position;*/

        Vector3 tmp = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3f);
        transform.position = camera.ScreenToWorldPoint(tmp);
        transform.LookAt(camera.ScreenToViewportPoint(tmp));

    }
}
