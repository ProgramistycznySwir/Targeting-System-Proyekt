using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingComputer : MonoBehaviour
{
    public int delay = 5;
    public GameObject target;
    public float projectileSpeed;
    public Transform meetPointT;
    public KeyCode fire = KeyCode.Space;

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position = new Vector3(0, 0, 0) + new Vector3(1, 1, 1) * time;
        if (Input.GetKey(fire))
        {
            Vector3 meetPoint = FindMeetPoint3D(transform.position, target.transform.position, target.GetComponent<Rigidbody>().velocity, projectileSpeed);
            transform.LookAt(meetPoint);
            gameObject.GetComponent<Rigidbody>().velocity = transform.forward/*.normalized*/ * projectileSpeed;
            meetPointT.position = meetPoint;

            //Vector3 targetV = target.GetComponent<Rigidbody>().velocity;

            //Vector3 time1 = new Vector3(time, time, time);

            //Vector3 startP = gameObject.transform.position, startT = target.transform.position;

            //Vector3 velocityP = (startT - startP + targetV * time) / time; //new Vector3(targetV.x * time, targetV.y * time, targetV.z * time))

            //Debug.Log(velocityP.magnitude);

            //gameObject.transform.LookAt(target.transform.position);
            //gameObject.GetComponent<Rigidbody>().velocity = velocityP;
            //delay = -1;
        }
        //else delay--;
    }
    /// <summary>
    /// Even more optimised MeetPointForProjectile function (Function that calculates point where turret have to shoot in order to hit target with given velocity); probably a bit less accurate
    /// </summary>
    /// <param name="A"> PositionOfShooter</param>
    /// <param name="B"> TargetPosition</param>
    /// <param name="tv"> TargetVelocity</param>
    /// <param name="v"> ProjectileVelocity</param>
    /// <returns></returns>
    public static Vector3 FindMeetPoint3D(Vector3 A, Vector3 B, Vector3 tv, float v)
    {
        Vector3 D = B - A;
        float sqrt = Mathf.Sqrt((D.z*D.z + D.y*D.y + D.x*D.x)*v*v + ((-D.y*D.y)-D.x*D.x)*tv.z*tv.z + (2*D.y*D.z*tv.y + 2*D.x*D.z*tv.x)*tv.z + ((-D.z*D.z)-D.x*D.x)*tv.y*tv.y + 2*D.x*D.y*tv.x*tv.y + ((-D.z*D.z)-D.y*D.y)*tv.x*tv.x);
        float divider = v*v - tv.z*tv.z - tv.y*tv.y - tv.x*tv.x;

        if (divider == 0)
        {
            Debug.LogError("FindMeetPoint3D() failed cause divider=0 for params: " + A + "; " + B + "; " + tv + "; " + v);
            return new Vector3(0, 0, 0);
        }

        float t = (sqrt + D.z*tv.z + D.y*tv.y + D.x*tv.x) / divider;
        if (t < 0)
        {
            t = -(sqrt - D.z * tv.z - tv.y * D.y - D.x * tv.x) / divider;
        }
        Vector3 C = B + tv * t;
        return C;
    }
    /// <summary>
    /// Fun version of FindMeetPoint3D()
    /// </summary>
    /// <param name="A"> PositionOfShooter</param>
    /// <param name="B"> TargetPosition</param>
    /// <param name="tv"> TargetVelocity</param>
    /// <param name="v"> ProjectileVelocity</param>
    /// <returns></returns>
    public static Vector3 FindMeetPoint3DFunVersion(Vector3 A, Vector3 B, Vector3 tv, float v)
    {
        Vector3 D = B - A;
        float sqrt = Mathf.Sqrt((D.z * D.z + D.y * D.y + D.x * D.x) * v * v + ((-D.y * D.y) - D.x * D.x) * tv.z * tv.z + (2 * D.y * D.z * tv.y + 2 * D.x * D.z * tv.x) * tv.z + ((-D.z * D.z) - D.x * D.x) * tv.y * tv.y + 2 * D.x * D.y * tv.x * tv.y + ((-D.z * D.z) - D.y * D.y) * tv.x * tv.x);
        float divider = v * v - tv.z * tv.z - tv.y * tv.y - tv.x * tv.x;

        if (divider == 0)
        {
            Debug.LogError("FindMeetPoint3D() failed (divider=0) for params: " + A + "; " + B + "; " + tv + "; " + v);
            return new Vector3(0, 0, 0);
        }

        float t = (sqrt + D.z * D.z + D.y * D.y + D.x * D.x) / divider;
        if (t < 0)
        {
            t = -(sqrt - D.z * D.z - D.y * D.y - D.x * D.x) / divider;
        }
        return B + tv * t;
    }
}
