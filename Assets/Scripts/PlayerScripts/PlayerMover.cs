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
        _animator.SetGround(_placeChecker.IsOnGround, _placeChecker.InWater);

        float direction = Input.GetAxis(Vertical);
        float distance = direction * _moveSpeed * Time.deltaTime;
        
        transform.Translate(distance * Vector3.forward);

        _animator.DoMove(direction);
    }

    private void Rotate()
    {
        float rotation = Input.GetAxis(Horizontal);

        transform.Rotate(rotation * _rotationSpeed * Time.deltaTime * Vector3.up);
        
    }

    private void JumpUp()
    {
        float force = Input.GetAxisRaw(Jump);

        _animator.SetGround(_placeChecker.IsOnGround, _placeChecker.InWater);

        if (_placeChecker.IsOnGround || _placeChecker.InWater)
        {
            _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        }

        _animator.DoJumpAnimation(_placeChecker.IsOnGround, _placeChecker.InWater);
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
