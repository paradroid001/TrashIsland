using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace GAD375.Prototyper
{
    [RequireComponent(typeof(Animator))]
    public class ObjectAnimator : MonoBehaviour
    {
        [System.Serializable]
        public class AnimationInfo : NamedData<AnimationClip>{}
        
        [SerializeField]
        private Animator animatorComponent;
        public AnimationInfo[] animations;

        public void Start()
        {
            if (animatorComponent == null)
                animatorComponent = GetComponent<Animator>();
            //foreach (AnimationInfo animinfo in animations)
            //{
            //    animationComponent.AddClip(animinfo.data, animinfo.name);
            //    //animationComponent[animinfo.name] = animinfo.data;
            //}
        }

        [YarnCommand("animate")]
        public void AnimateObject(string animname)
        {
            AnimationClip clip;
            if (AnimationInfo.FindByName(animations, animname, out clip))
            {
                //we actually dont need the clip
                //we only check the name to make sure there was an
                //animation with that name.

                //All we have to do is play the animname
                animatorComponent.Play(animname);
            }
        }
        
    }
}