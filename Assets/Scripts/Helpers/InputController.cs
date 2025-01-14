using UnityEngine;

namespace Helpers
{
    public class InputController : MonoBehaviour
    {
        private Vector2 _startTouchPosition;
        private Vector2 _endTouchPosition;
        private bool _isSwipeDetected;

        void Update()
        {
            DetectSwipe();
        }

        private void DetectSwipe()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _startTouchPosition = touch.position;
                        _isSwipeDetected = false;
                        break;

                    case TouchPhase.Moved:
                    case TouchPhase.Ended:
                        _endTouchPosition = touch.position;

                        if (!_isSwipeDetected)
                        {
                            Vector2 swipeDelta = _endTouchPosition - _startTouchPosition;

                            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) // Horizontal swipe
                            {
                                if (swipeDelta.x < 0) // Swiping left
                                {
                                    Debug.Log("swipe left");
                                }
                                else if (swipeDelta.x > 0) // Swiping right
                                {
                                    Debug.Log("swipe right");
                                }
                            }
                        }
                        break;
                }
            }
            
            else if (Input.GetMouseButtonDown(0)) // Left mouse button down
            {
                _startTouchPosition = Input.mousePosition;
                _isSwipeDetected = false;
            }
            else if (Input.GetMouseButton(0)) // Left mouse button held
            {
                _endTouchPosition = Input.mousePosition;

                if (!_isSwipeDetected)
                {
                    Vector2 swipeDelta = _endTouchPosition - _startTouchPosition;

                    if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) // Horizontal swipe
                    {
                        if (swipeDelta.x < 0) // Swiping left
                        {
                            Debug.Log("swipe left");
                        }
                        else if (swipeDelta.x > 0) // Swiping right
                        {
                            Debug.Log("swipe right");
                        }
                    }
                }
            }
        }
    }
}