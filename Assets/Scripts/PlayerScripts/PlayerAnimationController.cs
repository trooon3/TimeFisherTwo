using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;

    private int _slowRun = Animator.StringToHash("Slow Run");
    private int _jump = Animator.StringToHash("Jump");
    private int _treadingWater = Animator.StringToHash("Treading Water");
    private int _happyIdle = Animator.StringToHash("Happy Idle");
    private int _swimming = Animator.StringToHash("Swimming");


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void DoJumpAnimation(bool isOnGround, bool isInWater)
    {
        if (isOnGround || isInWater)
        {
            return;
        }
        else 
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

        //if (isOnGround)
        //{
        //    _animator.Play(_happyIdle);
        //}
        //else if (isInWater)
        //{
        //    _animator.Play(_treadingWater);
        //}

    }
     public void SetGround(bool IsOnGround, bool InWater)
     {
        _animator.SetBool("InWater", InWater);
        _animator.SetBool("OnEarth", IsOnGround);
     }
}

