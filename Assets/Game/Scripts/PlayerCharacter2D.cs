using UnityEngine;
using System.Collections;


namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerCharacter2D : MonoBehaviour
    {
        [SerializeField]
        private float maxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField]
        private float jumpForce = 400f;                  // Amount of force added when the player jumps.
        [SerializeField]
        private bool airControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField]
        private LayerMask whatIsGround;                  // A mask determining what is ground to the character

        private bool isGrounded;            // Whether or not the player is grounded.
        private Rigidbody2D rb2D;
        private bool facingRight = true;  // For determining which way the player is currently facing.

        Transform groundCheck;
        const float groundCheckRadius = 0.2f;
        

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            groundCheck = transform.Find("GroundCheck");
        }


        private void FixedUpdate()
        {
            GroundCheck();

            Debug.Log("Is Grounded " + isGrounded);
        }


        public void Move(float move, bool jump)
        {
            //only control the player if grounded or airControl is turned on
            if (isGrounded || airControl)
            {
                // Move the character
                rb2D.velocity = new Vector2(move * maxSpeed, rb2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !facingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && facingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (isGrounded && jump)
            {
                isGrounded = false;
                // Add a vertical force to the player.
                rb2D.AddForce(new Vector2(0f, jumpForce));
                Debug.Log("Jumping " + jump);
            }
        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        private void GroundCheck()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    isGrounded = true;
            }
        }
    }
}

