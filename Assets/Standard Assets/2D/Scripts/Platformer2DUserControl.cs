using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private int jumpCount = 0;
        private bool bodySlam;
        private int bodySlamCount = 0;
        private bool dash;
        private int dashCount = 0;

        Vector2 moveInputInitialTouchPosition = new Vector2();
        




        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            ActionInput();
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = new float();
            h = MoveInput(h);

            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump, bodySlam, dash);
            m_Jump = false;
            bodySlam = false;
            dash = false;
        }

        //Move Input Function
        private float MoveInput(float h)
        {
            if (Input.touchCount > 0)
            {

                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    moveInputInitialTouchPosition = Input.GetTouch(0).position;
                }

                Vector2 touchDeltaPosition = Input.GetTouch(0).position - moveInputInitialTouchPosition;
                touchDeltaPosition.x = Mathf.Clamp(touchDeltaPosition.x, -1, 1);
                h = touchDeltaPosition.x;
            }
            return h;
        }

        //Action Input Function
         private void ActionInput()
        {
            if (Input.touchCount > 1)
            {
                if (Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    if (Input.GetTouch(1).deltaPosition.y > 25f && Input.GetTouch(1).deltaPosition.magnitude > 30f && jumpCount < 1 && !m_Jump)
                    {
                        m_Jump = true;
                        jumpCount++;
                        Invoke("JumpReset", 0.5f);
                        Debug.Log("Input Delta Position: " + Input.GetTouch(1).deltaPosition);
                    }

                    if (Input.GetTouch(1).deltaPosition.y < -25f && Input.GetTouch(1).deltaPosition.magnitude > 30f && bodySlamCount < 1 && !bodySlam)
                    {
                        bodySlam = true;
                        bodySlamCount++;
                        Invoke("BodySlamReset", 0.5f);
                    }

                    if (Input.GetTouch(1).deltaPosition.x >25f || Input.GetTouch(1).deltaPosition.x < -25f && Input.GetTouch(1).deltaPosition.magnitude > 30f && dashCount < 1 && !dash)
                    {
                        dash = true;
                        dashCount++;
                        Invoke("DashReset", 0.5f);
                    }
                }
            }
        }

         private void JumpReset()
         {
             jumpCount = 0;
         }

        private void BodySlamReset()
         {
             bodySlamCount = 0;
         }

        private void DashReset()
        {
            dashCount = 0;
        }
    }
}
