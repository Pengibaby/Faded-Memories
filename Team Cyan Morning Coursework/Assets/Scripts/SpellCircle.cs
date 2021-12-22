using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCircle : MonoBehaviour
{
    public float offset;

    //Current cooldown time remaining before the next shot can happen.
    private float currentShotsCooldown;
    //Cooldown time.
    public float shotsCoolDownTime;

    public GameObject fireBall;
    public Transform firePoint;

    private float rotationZaxis = 0f;

    public GameObject player;

    private Coroutine waitBeforeDisableSprite = null;

    // Update is called once per frame
    void Update()
    {
        //Direction vector: position vector of mouse - position vector of object.
        Vector3 directionVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //Works out the rotation angle using tan with x and y of the direction vector. Convert to degrees also.
        rotationZaxis = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
        //Set the rotation of the current gameObject to be the rotationZaxis.
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZaxis + offset);

        //If the current cooldown time is 0 or less, then allow fireball to be shot out.
        if (currentShotsCooldown <= 0)
        {
            //If right mouse button is pressed, spawn fireball projectile.
            if (Input.GetMouseButtonDown(1))
            {
                //Wait for 1 second before shooting the fireball.
                waitBeforeDisableSprite = StartCoroutine(waitForSecondsEnable(1f));
                //Reset the current cooldown time to be the initial cooldown time.
                currentShotsCooldown = shotsCoolDownTime;
            }
        }
        //If the current cooldown time is larger than 0.
        else 
        {
            //Subtract Time.deltaTime from it.
            currentShotsCooldown -= Time.deltaTime;
        }
    }

    private IEnumerator waitForSecondsDisable(float value) {
        //Returns the player's movement back to the default of 8.
        player.GetComponent<Player>().speed = 8;
        yield return new WaitForSeconds(value);
        //disable the sprite renderer after a few seconds.
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private IEnumerator waitForSecondsEnable(float value)
    {
        //Enabled the sprite renderer to show the spell circle.
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //Lowers the player's movement speed to represent casting.
        player.GetComponent<Player>().speed = 2;
        //waits for seconds.
        yield return new WaitForSeconds(value);
        //Plays the fire ball shooting sound.
        GetComponent<AudioSource>().Play();
        //Spawn fireball at firePoint position (child object of SpellCircle), and at the current spell circle rotation (without the offset).
        Instantiate(fireBall, firePoint.position, Quaternion.Euler(0f, 0f, rotationZaxis));
        //Wait for 0.5 seconds and then disable the spell circle sprite.
        waitBeforeDisableSprite = StartCoroutine(waitForSecondsDisable(0.5f));
    }
}
