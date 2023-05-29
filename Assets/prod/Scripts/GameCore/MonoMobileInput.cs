using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class Direction
    {
        public enum Dir
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        public static Vector2 DirToVec2(Dir dir)
        {
            Vector2 retval = Vector2.right;
            switch (dir)
            {
                case Direction.Dir.UP:
                    retval = Vector2.up;
                    break;
                case Direction.Dir.DOWN:
                    retval = Vector2.down;
                    break;
                case Direction.Dir.RIGHT:
                    retval = Vector2.right;
                    break;
                case Direction.Dir.LEFT:
                    retval = Vector2.left;
                    break;
            }
            return retval;
        }
    }

    /**
    * SwipeData is a small object which is passed to a swipe delegate when
    * a swipe occurs. Currently it contains a Direction.Dir to indicate
    * which direction was swiped.
    * It will be extended in future to include other data.
    */
    public class SwipeData
    {
        public Direction.Dir dir;
        public Vector2 posStart; //where this swipe started
        public Vector2 posCurrent; //current position of swipe
        //could also have extra data here for 
        //- length/velocity of swipe
        //- position on screen where swipe occurred
    }
    public class MonoMobileInput : MonoBehaviour
    {

        //public delegate void OnTap(Action<Vector2> pos);
        //static public event Action<Vector2> OnTap = delegate{};
        public delegate void PosDelegate(Vector2 pos);
        public delegate void SwipeDelegate(SwipeData swipe);
        public delegate void TouchArrayDelegate(Vector3[] touches);
        public delegate void TiltDelegate(Vector3 tilt);
        public static PosDelegate OnTap;
        public static TiltDelegate OnTilt;
        public static SwipeDelegate OnSwipe;
        public static TouchArrayDelegate OnTouch;

        /**
        * Whether or not controls are enabled: each delegate should respect
        * the value of controlsEnabled and ignore input if false.
        */
        public static bool controlsEnabled = true; //disables touch controls

        /**
        * This flag indicates whether reported positions should be in
        * screen space (the default) or world space (set this to true)
        */
        [Tooltip("Should reported positions be in world space or screen space?")]
        public bool worldSpaceTouchPositions = false; //in screenspace or world space?
    }
}