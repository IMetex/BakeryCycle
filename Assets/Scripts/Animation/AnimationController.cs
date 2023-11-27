using Singleton;
using UnityEngine;

namespace Animation
{
   public class AnimationController : Singleton<AnimationController>
   {
      [SerializeField] private Animator _animator;

      public void SetBoolean(string animationType, bool value)
      {
         _animator.SetBool(animationType,value);
      }
   }
}
