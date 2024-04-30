using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static PlayerInputAction;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f; //이동속도
    [SerializeField] private float jumpPower = 5f; //점프할 때 사용되는 힘
    [SerializeField] private float maxStamina = 20; // 최대 스태미나 값
    [SerializeField] private float staminaRecoveryRate = 0.75f; // 스태미나 회복 속도
    [SerializeField] private float mouseX = 10f;
    [SerializeField] private float sensitivityX = 1f;

    public float stamina = 20; //현재 스태미나 값

    private float currentSpeed;

    public bool _isJumping;
    public bool _isCrouching;
    public bool _isRunning;
    public bool _isTired;


    private Rigidbody _rigid;
    private PlayerInputAction _playerInputActions;
    private CapsuleCollider _capsuleCollider;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _rigid = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        _playerInputActions = new PlayerInputAction();

        _playerInputActions.PlayerInput.Enable();
        _playerInputActions.PlayerInput.Jump.performed += PlayerJump;
    }

    private void Start()
    {
        _isJumping = false;
        _isCrouching = false;
        _isRunning = false;
        _isTired = false;

        currentSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        RecoverStamina();
        ConsumedStamina();
        PlayerRotateX();
        PlayerRunning();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    public void PlayerMovement()
    {
        Vector2 inputVector = _playerInputActions.PlayerInput.Move.ReadValue<Vector2>();

        Vector3 movement = new Vector3(inputVector.x, 0, inputVector.y) * currentSpeed * Time.deltaTime;

        _rigid.transform.Translate(movement);
    }

    public void PlayerCrouching()
    {
        
    }

    public void PlayerRunning()
    {
        if(Input.GetKey(KeyCode.LeftShift) && !_isTired)
        {
            _isRunning = true;
            currentSpeed = moveSpeed * 1.4f;
        }

        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isRunning = false;
            currentSpeed = moveSpeed;
        }

        if(stamina <= 0)
        {
            _isRunning = false;
            currentSpeed = moveSpeed;
            _isTired = true;
        }
    }

    public void PlayerJump(InputAction.CallbackContext context)
    {
        if (context.performed && !_isJumping)
        {
            _rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            _isJumping = true;
        }
    }

    void RecoverStamina()
    {
        Vector2 inputVec = _playerInputActions.PlayerInput.Move.ReadValue<Vector2>();

        if (!_isRunning && !_isCrouching && stamina < maxStamina)
        {
            stamina += staminaRecoveryRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        }

        if(stamina > 4f)
        {
            _isTired = false;
        }
    }

    void ConsumedStamina()
    {
        Vector2 inputVec = _playerInputActions.PlayerInput.Move.ReadValue<Vector2>();

        if(_isRunning && inputVec.x != 0)
        {
            stamina -= 2.0f * Time.deltaTime;
        }

        else if(_isRunning && inputVec.y != 0)
        {
            stamina -= 2.0f * Time.deltaTime;
        }

        else if(_isCrouching)
        {
            stamina -= 0.1f * Time.deltaTime;
        }
    }

    void PlayerRotateX()
    {
        float rotationX = Input.GetAxis("Mouse X") * sensitivityX;

        mouseX = mouseX + rotationX;

        // Y축 회전값만 변경하고, X와 Z 축 회전값은 그대로 유지합니다.
        Quaternion targetRotation = Quaternion.Euler(0, mouseX ,0);

        // 회전을 적용합니다.
        transform.localRotation = targetRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            _isJumping = false;
        }

        if(collision.gameObject.CompareTag("Wall"))
        {
            _capsuleCollider.material.dynamicFriction = 0f;
            _capsuleCollider.material.staticFriction = 0f;
            _capsuleCollider.material.frictionCombine = PhysicMaterialCombine.Minimum;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _capsuleCollider.material.dynamicFriction = 0.8f;
            _capsuleCollider.material.staticFriction = 0.8f;
            _capsuleCollider.material.frictionCombine = PhysicMaterialCombine.Average;
        }
    }
}
