using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private readonly int _slowRun = Animator.StringToHash("Slow Run");
        private readonly int _jump = Animator.StringToHash("Jump");
        private readonly int _swimming = Animator.StringToHash("Swimming");

        public void SetAnimator(Animator animator)
        {
            _animator = animator;
            _animator.Play(_jump);
        }

        public void DoJumpAnimation(bool isOnGround, bool isInWater)
        {
            if (!isOnGround && !isInWater)
            {
                _animator.Play(_jump);
            }
        }

        public void DoSwimAnimation()
        {
            _animator.Play(_swimming);
        }

        public void DoRunAnimation()
        {
            _animator.Play(_slowRun);
        }

        public void DoMove(float speed)
        {
            _animator.SetFloat("Speed", speed);
        }

        public void SetGround(bool IsOnGround, bool InWater)
        {
            _animator.SetBool("InWater", InWater);
            _animator.SetBool("OnEarth", IsOnGround);
        }
    }
}