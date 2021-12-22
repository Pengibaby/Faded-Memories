using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed; //Speed of fireball.
    public float timeBeforeDespawn; //time before despawning the fireball if it doesn't hit anything.

    void Start() {
        //Destroy the fireball after some time if it doesn't hit anything.
        Invoke("DestroyAfterTime", timeBeforeDespawn);
    }

    // Update is called once per frame
    void Update()
    {
        //Moves the fireball.
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    //A function that can be invoked to destroy the fireball.
    private void DestroyAfterTime() {
        //Destroy the fireball.
        Destroy(gameObject);
    }
}
