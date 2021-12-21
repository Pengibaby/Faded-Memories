using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCircle : MonoBehaviour
{
    public float offset;

    // Update is called once per frame
    void Update()
    {
        //Direction vector: position vector of mouse - position vector of object.
        Vector3 directionVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //Works out the rotation angle using tan with x and y of the direction vector. Convert to degrees also.
        float rotationZaxis = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
        //Set the rotation of the current gameObject to be the rotationZaxis.
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZaxis + offset);
    }
}
