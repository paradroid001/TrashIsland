using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class MonoMobileTapInput : MonoMobileInput
    {
        public static SwipeDelegate OnTapHeld;
        [SerializeField]
        protected float tapHeldThreshold = 0.1f; //how long until 'held' as opposed to tapped.

        protected SwipeData tapData;

        protected void Start()
        {
            tapData = new SwipeData();
            tapData.Reset();
        }

        protected void Update()
        {
            if (!controlsEnabled)
                return;

            //1. Do this first touch
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); //first touch only!
                if (touch.phase == TouchPhase.Began)
                {
                    tapData.swipeTime = 0.0f;
                    tapData.posStart = TouchPosition(touch.position);
                    tapData.posCurrent = TouchPosition(touch.position);
                }
                else if (touch.phase == TouchPhase.Stationary || //allow holding down
                         touch.phase == TouchPhase.Moved)
                {
                    tapData.swipeTime += Time.deltaTime;
                    tapData.posCurrent = TouchPosition(touch.position);
                    if (tapData.swipeTime > tapHeldThreshold)
                    {
                        OnTapHeld?.Invoke(tapData);
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    // for a "tap" to count, the swipetime has to be 
                    // less than the threshold
                    tapData.swipeTime += Time.deltaTime;
                    if (tapData.swipeTime <= tapHeldThreshold)
                    {
                        OnTap?.Invoke(tapData.posCurrent);
                    }
                    else
                    {
                        OnTapHeld?.Invoke(tapData);
                    }
                    tapData.Reset(); //end the touch.
                }
                else if (touch.phase == TouchPhase.Canceled)
                {
                    tapData.Reset(); //end the touch
                }
            }
        }
    }
}