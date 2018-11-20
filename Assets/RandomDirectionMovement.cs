using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirectionMovement : MonoBehaviour
{
    public float maxVelocity = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3((Random.value-0.5f) * maxVelocity * 2f, (Random.value - 0.5f) * maxVelocity * 2f, (Random.value - 0.5f) * maxVelocity * 2f);
    }
}
