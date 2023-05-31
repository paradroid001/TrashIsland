using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class MonoMobileTapInput : MonoMobileInput
    {
        void Update()
        {
            if (!controlsEnabled)
                return;
            bool tapped = false;
            Vector2 tappos = Vector2.zero;

            //1. Do this first touch
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); //first touch only!
                if (touch.phase == TouchPhase.Began ||
                    touch.phase == TouchPhase.Stationary || //allow holding down
                    touch.phase == TouchPhase.Moved)        //allow holding down
                {
                    tapped = true;
                    tappos = touch.position;
                }
            }


            if (tapped)
            {
                if (worldSpaceTouchPositions)
                {
                    tappos = Camera.main.ScreenToWorldPoint(tappos);
                }
                OnTap?.Invoke(tappos);
            }
        }
    }
}