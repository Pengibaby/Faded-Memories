using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Collidable
{
    public float speed; //Speed of fireball.
    public float timeBeforeDespawn; //time before despawning the fireball if it doesn't hit anything.

	//Damage struct
	public int damagePoint = 2;
	public float pushForce = 2.0f;

	protected override void Start()
	{
        base.Start();
        //Destroy the fireball after some time if it doesn't hit anything.
        Invoke("DestroyAfterTime", timeBeforeDespawn);
    }

	// Update is called once per frame
	protected override void Update()
	{
        base.Update();
        //Moves the fireball.
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    //A function that can be invoked to destroy the fireball.
    private void DestroyAfterTime() {
        //Destroy the fireball.
        Destroy(gameObject);
    }

	protected override void OnCollide(Collider2D coll)
	{
		if (coll.tag == "Fighter")
		{
			//Checks to make sure the collider is not player or head collider object on the player.
			if (coll.name == "Player" || coll.name == "Head Collider")
			{
				return;
			} 
			else
            {
				//Create a new damage object, then we'll send it to the fighter we've hit
				Damage dmg = new Damage()
				{
					damageAmount = damagePoint,
					origin = transform.position,
					pushForce = pushForce
				};

				coll.SendMessage("ReceiveDamage", dmg);

				//Plays the audio for enemies getting hit by the player sword.
				FindObjectOfType<SoundManager>().Play("FireBallHitEnemy");
				DestroyAfterTime();
			}
		}
		//If the collision is with a wall, play the SwordHitStone sound effect.
		else if (coll.tag == "StoneWall")
		{
			FindObjectOfType<SoundManager>().Play("FireBallHitEnemy");
			DestroyAfterTime();
		}
	}
}
