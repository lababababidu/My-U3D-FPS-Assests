using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerCharacterController : MonoBehaviour
{
    public static PlayerCharacterController Instance;

    public Camera playerCamera;
    public float gravityForce = 20f;
    public float maxSpeedOnGround = 8f;
    public float moveSharpness = 15f;
    public float cameraRotationSpeed = 200f;
    public float cameraHeight = 0.9f;
    public Vector3 CharacterVelocity {set;get;}
    public bool FireAbility =false;

    // public bool onGround = true;

    private CharacterController _characterController;
    private PlayerInputHandler _playerInputHandler;
    private float _targetCharacterHeight = 1.8f;
    private float _cameraVerticalAngel = 0f;

    private void Awake(){
        Instance = this;
    }

    private void Start(){
        _characterController = GetComponent<CharacterController>();
        _playerInputHandler = GetComponent<PlayerInputHandler>();

        _characterController.enableOverlapRecovery = true;
        UpdateCharacterHeight();
    }

    private void Update(){
        HandleCharactorView();
        HandleCharactorMovement();
        // HandleCharactorJump();
    }

    private void UpdateCharacterHeight(){
        _characterController.height = _targetCharacterHeight;
        _characterController.center = Vector3.up * _characterController.height * 0.5f;

        playerCamera.transform.localPosition = Vector3.up * _characterController.height * cameraHeight;
    }

    private void HandleCharactorMovement(){
        Vector3 worldSpaceMove = transform.TransformVector(_playerInputHandler.GetMoveInput());

        if(_characterController.isGrounded){
            Vector3 targetVelocity = worldSpaceMove * maxSpeedOnGround;
            this.CharacterVelocity = Vector3.Lerp(CharacterVelocity,targetVelocity,moveSharpness * Time.deltaTime);
            if(_playerInputHandler.GetJumpInput()){
                this.CharacterVelocity += Vector3.up * 7.5f;
            }
        }else{
            this.CharacterVelocity += Vector3.down * gravityForce * Time.deltaTime;
        }

        _characterController.Move(this.CharacterVelocity * Time.deltaTime); 
    }

    private void HandleCharactorView(){
        float horizontal,vertical;
        (horizontal,vertical) = _playerInputHandler.GetMouseLook();
        
        transform.Rotate(new Vector3(0, horizontal * cameraRotationSpeed,0),Space.Self); 

        _cameraVerticalAngel += vertical * cameraRotationSpeed;
        _cameraVerticalAngel = Mathf.Clamp(_cameraVerticalAngel,-89f,89f);

        playerCamera.transform.localEulerAngles = new Vector3(-_cameraVerticalAngel,0,0);

    }

    
}
