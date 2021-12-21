using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCircle : MonoBehaviour
{
    public float offset;

    public GameObject fireBall;
    public Transform firePoint;

    private Coroutine waitBeforeDisableSprite = null;

    // Update is called once per frame
    void Update()
    {
        //Direction vector: position vector of mouse - position vector of object.
        Vector3 directionVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //Works out the rotation angle using tan with x and y of the direction vector. Convert to degrees also.
        float rotationZaxis = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
        //Set the rotation of the current gameObject to be the rotationZaxis.
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZaxis + offset);

        //If right mouse button is pressed, spawn fireball projectile.
        if (Input.GetMouseButtonDown(1))
        {
            //Wait for 1 second before shooting the fireball.
            waitBeforeDisableSprite = StartCoroutine(waitForSecondsEnable(1f));
        }
    }

    private IEnumerator waitForSecondsDisable(float value) {
        yield return new WaitForSeconds(value);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private IEnumerator waitForSecondsEnable(float value)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(value);
        //Spawn fireball at firePoint position (child object of SpellCircle), and at the current spell circle rotation.
        Instantiate(fireBall, firePoint.position, transform.rotation);
        //Wait for 0.5 seconds and then disable the spell circle sprite.
        waitBeforeDisableSprite = StartCoroutine(waitForSecondsDisable(0.5f));
    }
}
