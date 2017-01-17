using UnityEngine;
using System.Collections;

	public class samPlayer : MonoBehaviour {

		private Rigidbody2D myRigidBody;
		[SerializeField]
		private float movementSpeed;
		private bool facingRight;
		[SerializeField]
		private Transform[] groundPoints;
		[SerializeField]
		private float groundRadius;
		[SerializeField]
		private LayerMask whatIsGround;
		private bool isGrounded;
		private bool jump;
		private bool slide;
		private Animator myAnimator;
		[SerializeField]
		private float jumpForce;
		private float jumpSpeed;
		public GameObject shootObject;	
		private Shoot shootScript; 

		// Use this for initialization

		void Awake(){
		shootObject = GameObject.FindWithTag("shooter");
			shootScript = shootObject.GetComponent<Shoot>();

		}


		void Start () 
		{	

			facingRight=true;
			myRigidBody=GetComponent<Rigidbody2D>();
			myAnimator=GetComponent<Animator>();
		}
		
		// Update is called once per frame
		void FixedUpdate () 
		{
			HandleInput();
			float horizontal = Input.GetAxis("Horizontal");
			isGrounded=IsGrounded();
			HandleMovement(horizontal);
			flip(horizontal);
			ResetValues();
			HandleLayer();
		}

		private void HandleMovement(float horizontal)
		{
				if(myRigidBody.velocity.y < 0 )
				{
				myAnimator.SetBool("land", true);
				}
				myRigidBody.velocity = new Vector2(horizontal * movementSpeed, myRigidBody.velocity.y);

				if (slide && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
				{
				myAnimator.SetBool("slide", true);
				}
				else if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
				{
				myAnimator.SetBool("slide", false);
				}

				if(isGrounded && jump)
				{
					isGrounded = false;
					myRigidBody.AddForce(new Vector2(0,jumpForce));
					myAnimator.SetTrigger("jump");
				}

			myAnimator.SetFloat("speed",Mathf.Abs(horizontal));

		}

		private void HandleInput(){
			if (Input.GetKeyDown(KeyCode.Space))
			{
				jump=true;
			}

			if(Input.GetKeyDown(KeyCode.LeftShift))
			{
				slide=true;
			}

		}

		private void flip(float horizontal)
		{
		if(horizontal>0 && !facingRight || horizontal <0 && facingRight)
			{
				facingRight=!facingRight;
				Vector3 theScale=transform.localScale;
				theScale.x *= -1;
				transform.localScale=theScale;
			}
		}

		private void ResetValues()
		{
		jump=false;
		slide=false;
		}

		private bool IsGrounded()
		{
			if(myRigidBody.velocity.y <=0)
			{
				foreach (Transform point in groundPoints)
				{
					Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
					for(int i =0; i < colliders.Length; i++ )
					{
						if(colliders[i].gameObject != gameObject)
						{
							myAnimator.ResetTrigger("jump");
							myAnimator.SetBool("land", false);
							return true;
						}
					}
				}
			}
			return false;
		}

		private void HandleLayer(){
		if(!isGrounded){
			myAnimator.SetLayerWeight(1,1);
		}
		else{
			myAnimator.SetLayerWeight(1,0);
		}
	}

	//void OnTriggerEnter(Collider other){
      //  	if(other.tag == "ammo"){
        //		shootScript.ammo += 6;
        //	}	
}