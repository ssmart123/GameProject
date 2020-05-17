using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Tooltip("카메라 제한")]
    public float CameraLimitMax, CameraLimitMin;
    [SerializeField]
    [Tooltip("수직감도")]
    private float horSensitivity =10f;
    [Tooltip("수평감도")]
    public float verSensitivity= 10f; 
    

    private float currentCameraRotX, currentCameraRotY;

    
    public Camera Camera;


    void Start()
    {
    }

    
    void Update()
    {

        CameraRotation();
        CharacterRotation();
        
    }

    private void CameraRotation()
    {
        float xRot = Input.GetAxisRaw("Mouse Y");
        float hor = xRot * horSensitivity;
        currentCameraRotX -= hor;
        currentCameraRotX = Mathf.Clamp(currentCameraRotX, CameraLimitMin, CameraLimitMax);

        Camera.transform.localEulerAngles = new Vector3(currentCameraRotX, 0, 0);
    }

    private void CharacterRotation()
    {
        float yRot = Input.GetAxisRaw("Mouse X");
        float ver = yRot * verSensitivity;
        currentCameraRotY += ver;

        transform.rotation = Quaternion.Euler(0, currentCameraRotY, 0);

    }

}
