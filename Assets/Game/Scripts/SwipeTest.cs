using UnityEngine;
using System.Collections;

public class SwipeTest : MonoBehaviour 
{
    Vector2 touchPosition;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchPosition = Input.GetTouch(0).position;
                Debug.Log("Touch initial position " + touchPosition);
            }
            // Get movement of the finger since last frame
           Vector2 touchDeltaPosition = touchPosition - Input.GetTouch(0).position;
           Debug.Log("Magnitude " + touchDeltaPosition.magnitude);
            int taps = Input.GetTouch(0).tapCount;
           // Debug.Log("Tap Count " + taps);

            
        }
	}
}
