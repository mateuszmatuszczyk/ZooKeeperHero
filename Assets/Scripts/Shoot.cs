using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    //Drag in the Bullet Emitter from the Component Inspector.
    public GameObject Bullet_Emitter;

    //Drag in the Bullet Prefab from the Component Inspector.
    public GameObject Bullet;

    //Enter the Speed of the Bullet from the Component Inspector.
    public float Bullet_Forward_Force;
	
	private bool facingRight;

	public int ammo;
	
	// Use this for initialization
	void Start ()
    {
		facingRight = true;

		ammo = 6; 
	}
	
	// Update is called once per frame
	void Update ()
	{
		

		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			facingRight = false;
			print("facing left");
			}
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			facingRight = true;
			print("facing right");
		}
			
		if(ammo > 0)
		{
			if (Input.GetButtonDown("Fire1"))
        	{
				ammo --;
					
	   			GetComponent<AudioSource>().Play();
	            //The Bullet instantiation happens here.
	            GameObject Temporary_Bullet_Handler;
	            Temporary_Bullet_Handler = Instantiate(Bullet,Bullet_Emitter.transform.position,Bullet_Emitter.transform.rotation) as GameObject;

	            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
	            Rigidbody Temporary_RigidBody;
	            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

	            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force. 
	          	if(facingRight){
	              Temporary_RigidBody.AddForce(transform.right * Bullet_Forward_Force);
	              print ("shoot right");
	              }
	              
				else{
					Temporary_RigidBody.AddForce(-transform.right * Bullet_Forward_Force);
					print ("shoot left");
									
				}
			Temporary_Bullet_Handler.transform.localScale = new Vector3 (facingRight ? 1 : -1,1,1);

            //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
            Destroy(Temporary_Bullet_Handler, 10.0f);
				Debug.Log("Ammo left: " + ammo);
        }
        }
    }

    //This method detects the collision between the player (gun) object and ammunition box. 
    //when player walks into the ammunition box, the ammunition count resets to 6 bullets
    void OnTriggerEnter2D(Collider2D col){

    	if (col.gameObject.tag=="ammo"){
    		print("ammo detected");
    		ammo = 6;
  	}
  	}

}

