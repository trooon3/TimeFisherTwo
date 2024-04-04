using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerMover : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);
    private const string Vertical = nameof(Vertical);

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private PlaceChecker _placeChecker;
    private Rigidbody _rigidbody;
    private PlayerAnimationController _animator;

    [SerializeField] public bool IsRun { get; private set; }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<PlayerAnimationController>();
    }

    private void Move()
    {
        float direction = Input.GetAxisRaw(Vertical);
        float distance = direction * _moveSpeed * Time.deltaTime;

        transform.Translate(distance * Vector3.forward);

        if (direction > 0.1f)
        {
            if (_placeChecker.IsOnGround)
            {
                _animator.DoRunAnimation();
            }

            if (_placeChecker.InWater)
            {
                _animator.DoSwimAnimation();
            }
        }
        else
        {
            if (_placeChecker.IsOnGround)
            {
                _animator.DoIdleAnimation();
            }
            if (_placeChecker.InWater)
            {
                _animator.DoTreadingAnimation();
            }
        }
    }

    private void Rotate()
    {
        float rotation = Input.GetAxisRaw(Horizontal);

        transform.Rotate(rotation * _rotationSpeed * Time.deltaTime * Vector3.up);
        
    }

    private void JumpUp()
    {
        float force = Input.GetAxisRaw(Jump);

        if (_placeChecker.IsOnGround || _placeChecker.InWater)
        {
            _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        }

        if (_placeChecker.IsOnGround == false && _placeChecker.InWater == false)
        {
            _animator.DoJumpAnimation();
        }
    }

    //private void Interact()
    //{
    //    if (_groundChecker.IsInteractbleNearby )
    //    {

    //    }
    //}

    void FixedUpdate()
    {
        Move();
        Rotate();
        JumpUp();
    }
}
