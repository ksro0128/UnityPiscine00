using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

	[SerializeField] private float speed = 5.0f;
	[SerializeField] private float jumpForce = 0.3f;
	private Rigidbody rb;
	private bool isGrounded = true;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		// rb.AddForce(movement * speed);


		if (movement.magnitude > 1)
		{
			movement.Normalize();
		}

		Vector3 currentVelocity = rb.velocity;
		Vector3 desiredVelocity = movement * speed;
		rb.velocity = new Vector3(desiredVelocity.x, currentVelocity.y, desiredVelocity.z);

		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
    }

	void OnCollisionStay(Collision collision)
	{
		isGrounded = true;
		if (collision.gameObject.tag == "Lava")
		{
			Debug.Log("Game Over!");
			Destroy(gameObject);
			
		}
	}

	void OnCollisionExit(Collision collision)
	{
		isGrounded = false;
	}
}
