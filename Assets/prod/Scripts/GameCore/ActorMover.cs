using UnityEngine; //for vector3

namespace GameCore
{
    public class ActorMovement : MonoBehaviour
    {
        GameObject _actor;
        [SerializeField]
        protected string actorName;
        public virtual GameObject actor
        {
            get { return _actor; }
            set { _actor = value; }
        }
        public virtual Vector3 MoveToward(float speed)
        {
            return Vector3.zero;
        }
    }
}