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
    private float  _maxPlaceToSwim = 210;
    private float  _minPlaceToSwim = 80;
    private Rigidbody _rigidbody;
    private PlayerAnimationController _animator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<PlayerAnimationController>();
    }

    private void Move()
    {
        _animator.SetGround(_placeChecker.IsOnGround, _placeChecker.InWater);

        Vector3 direction = new Vector3(Input.GetAxisRaw(Horizontal),0, Input.GetAxisRaw(Vertical)).normalized;

        Vector3 distance = direction * _moveSpeed * Time.deltaTime;
        Vector3 nextPosition = transform.position + distance;

        if (nextPosition.x <= _maxPlaceToSwim && nextPosition.x >= _minPlaceToSwim && nextPosition.z <= _maxPlaceToSwim && nextPosition.z >= _minPlaceToSwim)
        {
            if (direction != Vector3.zero)
            {
                transform.forward = direction;
            }

            transform.position = transform.position + distance;

            _animator.DoMove(direction.magnitude);
        }
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

    void FixedUpdate()
    {
        Move();
        JumpUp();
    }
}
