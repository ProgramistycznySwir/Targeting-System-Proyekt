using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingComputer : MonoBehaviour
{
    public int delay = 5;
    public GameObject target;
    public float time;

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position = new Vector3(0, 0, 0) + new Vector3(1, 1, 1) * time;
        if (delay == 0)
        {
            Vector3 targetV = target.GetComponent<Rigidbody>().velocity;

            Vector3 time1 = new Vector3(time, time, time);

            Vector3 startP = gameObject.transform.position, startT = target.transform.position;

            Vector3 velocityP = (startT - startP + targetV * time) / time; //new Vector3(targetV.x * time, targetV.y * time, targetV.z * time))

            Debug.Log(velocityP.magnitude);

            gameObject.transform.LookAt(target.transform.position);
            gameObject.GetComponent<Rigidbody>().velocity = velocityP;
            delay = -1;
        }
        else delay--;
    }
}
