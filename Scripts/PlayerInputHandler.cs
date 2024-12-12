using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler Instance;

    public float MouseSensitivity = 1f;

    private void Awake(){
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public Vector3 GetMoveInput(){
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        move = Vector3.ClampMagnitude(move,1);
        return move;
    }

    public bool GetFireInput(){
        if (Input.GetMouseButton(0)){
            return true;
        }
        return false;
    }
    public bool GetReloadInput(){
        if (Input.GetKeyDown(KeyCode.R)){
            Debug.Log("R");
            return true;
        }
        return false;
    }
    public bool GetJumpInput(){
        if (Input.GetKey(KeyCode.Space)){
            Debug.Log("Space");
            return true;
        }
        return false;
    }

    public (float,float) GetMouseLook(){
        float horizontal,vertical;
        horizontal = Input.GetAxisRaw("Mouse X") * MouseSensitivity * 0.1f;
        vertical = Input.GetAxisRaw("Mouse Y")* MouseSensitivity * 0.1f;

        return (horizontal,vertical);
    }
}
