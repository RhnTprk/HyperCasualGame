using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool jump = false;
    private bool isDraging = false;
    private Vector2 startTouchPosition, endTouchPosition, swipeDelta;

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        #region Mouse Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDraging = true;
            startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = Input.mousePosition;
            if (endTouchPosition.y > startTouchPosition.y && Mathf.Abs(endTouchPosition.y) > Mathf.Abs(startTouchPosition.y) * 2.5f)
            {
                Debug.Log("END :" + endTouchPosition.y + "Start :" + startTouchPosition.y);
                jump = true;
            }

            tap = false;
            isDraging = false;
            Reset();
        }
        #endregion

        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDraging = true;
                startTouchPosition = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended ||
                    Input.touches[0].phase == TouchPhase.Canceled)
            {
                endTouchPosition = Input.touches[0].position;
                if (endTouchPosition.y > startTouchPosition.y && Mathf.Abs(endTouchPosition.y) > Mathf.Abs(startTouchPosition.y) * 2.5f)
                {
                    jump = true;
                }

                tap = false;
                isDraging = false;

                Reset();
            }
        }
        #endregion

        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouchPosition;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouchPosition;
        }

        if (swipeDelta.magnitude > 800)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Left or right
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                //Up or down
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }

            Reset();
        }
    }

    private void Reset()
    {
        startTouchPosition = endTouchPosition = swipeDelta = Vector2.zero;
        isDraging = false;
    }

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool Jump { get { return jump; } set { jump = value; } }
    public bool Tap { get { return tap; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
}
