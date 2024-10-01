using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);
    private const string Vertical = nameof(Vertical);

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private PlaceChecker _placeChecker;
    [SerializeField] private PlayerAnimationController _animator;
    [SerializeField] private ButtonChangerController _buttonChangerController;

    private float  _maxPlaceToSwim = 210;
    private float  _minPlaceToSwim = 80;
    private float _increaseTimeSec = 60f;
    private Rigidbody _rigidbody;
    private Coroutine _coroutine;
    private WaitForSeconds _increaseTime;
    private bool _isActiveIncreaseAd;

    public bool IsActiveIncreaseAd => _isActiveIncreaseAd;
    public float IncreaseTimeSec => _increaseTimeSec;

    private void Start()
    {
        _increaseTime = new WaitForSeconds(_increaseTimeSec);
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        JumpUp();
    }

    public void SetActiveIncrease()
    {
        _moveSpeed = _moveSpeed * 2;
        _isActiveIncreaseAd = true;
        _buttonChangerController.SetButtonChangerOff();
        StartIncreaseTimer();
    }

    private void StartIncreaseTimer()
    {
        if (_coroutine != null)
        {
            StopCoroutine(IncreaseTimer());
        }

        _coroutine = StartCoroutine(IncreaseTimer());
    }

    private IEnumerator IncreaseTimer()
    {
        yield return _increaseTime;
        _isActiveIncreaseAd = false;
        _buttonChangerController.SetButtonChangerOn();
        _moveSpeed = _moveSpeed/2;
    }

    public void Move()
    {
        _animator.SetGround(_placeChecker.IsOnGround, _placeChecker.InWater);

        Vector3 direction = new Vector3(Input.GetAxisRaw(Horizontal), 0, Input.GetAxisRaw(Vertical)).normalized;

        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
        {
            JoyStickMove(direction);
        }
    }

    public void JoyStickMove(Vector3 direction)
    {
        Vector3 distance = direction * _moveSpeed * Time.deltaTime;
        Vector3 nextPosition = transform.position + distance;

        if (nextPosition.x <= _maxPlaceToSwim && nextPosition.x >= _minPlaceToSwim && nextPosition.z <= _maxPlaceToSwim && nextPosition.z >= _minPlaceToSwim)
        {
            if (direction != Vector3.zero)
            {
                Rotate(direction);
            }

            transform.position = transform.position + distance;

            _animator.DoMove(direction.magnitude);
        }
    }

    private void Rotate(Vector3 forward)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(forward), _rotationSpeed);
    }

    public void JumpUp()
    {
        float force = Input.GetAxisRaw(Jump);

        _animator.SetGround(_placeChecker.IsOnGround, _placeChecker.InWater);

        if (_placeChecker.IsOnGround || _placeChecker.InWater)
        {
            _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        }

        _animator.DoJumpAnimation(_placeChecker.IsOnGround, _placeChecker.InWater);
    }
}
