using UnityEngine;
using UnityEngine.PlayerLoop; //Allows us to connect 

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        [Header("This is a title")]
        [Space(10)]
        [Tooltip("Hover Description")]
        //move direction that the player will be heading
        Vector3 _moveDirection = Vector3.zero;
        //the movement speeds such as walk run jump
        [SerializeField] float _moveSpeed, _walk = 5, _sprint = 10, _crouch = 2.5f, _jump = 8;
        //how the player is going to not float away
        [SerializeField] float _gravity = 20;
        //a way to calculate physics
        [SerializeField] CharacterController _characterController;
        [SerializeField] private Camera mainCamera;
        [SerializeField] float _maxStamina = 75, _currentStamina = 50, staminaConsume = 12.5f, staminaRegen = 10;
        #endregion
        #region Function
        private void Awake()
        {
            //Assign a value to our reference by getting
            //This object that the script is attached to
            //Get a component on this object called Character Controller
            _characterController = this.GetComponent<CharacterController>();
            _moveSpeed = _walk;
            this.enabled = true;
            if (!mainCamera)
            {
                mainCamera = Camera.main;
            }
        }
        private void Update()
        {
            if (_characterController != null)
            {
                if (_characterController.isGrounded)
                {
                    //move along the axes
                    _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    //move in the direction faced
                    _moveDirection = transform.TransformDirection(_moveDirection);
                    //movement speed
                    _moveDirection *= _moveSpeed;
                    //if the input button "Jump" is pressed, move upwards the y axis
                    if (Input.GetButton("Jump"))
                    {
                        _moveDirection.y = _jump;
                    }
                    //if LeftShift is pressed, moveSpeed will be equal to sprint value
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        if (_currentStamina != 0)
                        {
                            _moveSpeed = _sprint;
                            _currentStamina -= staminaConsume * Time.deltaTime;
                        }
                    }
                    //if LeftControl is pressed, moveSpeed will be equal to crouch value
                    else if (Input.GetKey(KeyCode.LeftControl))
                    {
                        _moveSpeed = _crouch;
                    }
                    //else move at normal walk speed value
                    else
                    {
                        _moveSpeed = _walk;
                    }
                     
                }       
                        
            }
            _moveDirection.y -= _gravity * Time.deltaTime;
            _characterController.Move(_moveDirection * Time.deltaTime);
        }
    }
    #endregion
    /*/void Example()
    {
        /* if(Input.GetKeyDown(KeyCode.LeftShift))
                {
                    _moveSpeed = _sprint;
                }
                if(Input.GetKeyUp(KeyCode.LeftShift))
                {
                    _moveSpeed = _walk;
                }
                if(Input.GetKeyDown(KeyCode.LeftControl))
                {
                    _moveSpeed = _crouch;*/

}



