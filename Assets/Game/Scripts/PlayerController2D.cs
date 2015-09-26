using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;


namespace Player
{
    [RequireComponent(typeof(Player.PlayerCharacter2D))]
    public class PlayerController2D : MonoBehaviour
    {
        private bool jump;
        private bool dash;
        private bool bodySlam;
        private bool bounce;
        private bool pause;

        private PlayerCharacter2D playerCharacter2D;

        Vector2 initialTouch; 

        private void Awake()
        {
            playerCharacter2D = GetComponent<PlayerCharacter2D>();
        }

        private void Update()
        {
            if (!jump)
                /*jump = CrossPlatformInputManager.GetButtonDown("Jump");*/ JumpInput();
            Debug.Log("Jumping " + jump);

            if (!bodySlam)
                jump = CrossPlatformInputManager.GetButtonDown("BodySlam");

            if (!dash)
                jump = CrossPlatformInputManager.GetButtonDown("Dash");

            if (CrossPlatformInputManager.GetButtonDown("Pause"))
                pause = !pause;
        }

        private void FixedUpdate()
        {
            float horizontal = /*CrossPlatformInputManager.GetAxis("Horizontal");*/ MoveInput();
            Debug.Log("Horizontal " + horizontal);

            playerCharacter2D.Move(horizontal, jump);

            jump = false;
            dash = false;
            bodySlam = false;
            bounce = false;
        }
  
        private float MoveInput()
        {
            float x = new float();

            if (Input.touchCount > 0)
            {

                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    initialTouch = Input.GetTouch(0).position;
                }

                Vector2 touchDeltaPosition = initialTouch - Input.GetTouch(0).position;
                x = Mathf.Clamp(touchDeltaPosition.x, -1, 1);
                x = x * -1;
            }
            return x;
        }

        private void JumpInput()
        {
            if (Input.touchCount > 1)
            {
                if (Input.GetTouch(1).tapCount < 2)
                {
                    jump = true;
                }
            }
        }
    }
}
