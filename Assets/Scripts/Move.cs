using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // 움직임 속도
    public float MoveSpeed = 10;
    // 카메라 민감도
    public float LookSensitivity = 10;
    // X축 카메라 제한
    public float CameraRotationLimitX ;
    // 카메라 기본값
    public float currentCameraRotationX, currentCameraRotationY;
    // 마우스 회전 변수
    public float _xRotation, _yRotation;
    private Rigidbody rigidbody;


    public Camera theCamera;

    private void Start()
    { 
        
       rigidbody = GetComponent<Rigidbody>();

        
    
    }


    private void Update()
    {
        Moves();
        CameraRotation();
        CharacterRotation();


    }
   void Moves()
   {
        // wasd로 캐릭터 움직임
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
        }
   }
    public void CameraRotation()
    {
        _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * LookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -CameraRotationLimitX, CameraRotationLimitX);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0, 0);
    }
   public void CharacterRotation()
    {
        _yRotation = Input.GetAxisRaw("Mouse X");
        float _characterRotationY = _yRotation*LookSensitivity;
        currentCameraRotationY += _characterRotationY;

        transform.rotation = Quaternion.Euler(0, currentCameraRotationY, 0);
    }
   
}
