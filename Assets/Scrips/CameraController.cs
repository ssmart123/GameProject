using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform myTransform = null;
    public GameObject Target = null;

    private Transform targetTransform = null;

    public enum CameraViewPointState {FIRST, THIRD}
    public CameraViewPointState CameraState = CameraViewPointState.THIRD;

    public float Distance = 5f;
    public float Height = 2.5f;
    public float HeightDamping = 2f;
    public float RotationDamping = 3f;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        if (Target != null)
        {
            targetTransform = Target.transform;
        }
    }
    void ThirdView()
    {
        float wantedRotationAngle = targetTransform.eulerAngles.y;
        float wantedHeight = targetTransform.position.y + Height;

        float currentRotationAngle = myTransform.eulerAngles.y;  // 현재 카메라의 y축의 각도 값.
        float currentHeight = myTransform.position.y; // 현재 카메라의 높이 값.

        // 현재 각도에서 원하는 각도로 댐핑 값을 얻게 됩니다.
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, RotationDamping * Time.deltaTime);
        // 현재 높이에서 원하는 높이로 댐핑값을 얻습니다.
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, HeightDamping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0f, currentRotationAngle, 0f);

        myTransform.position = targetTransform.position;
        myTransform.position -= currentRotation * Vector3.forward * Distance;
        myTransform.position = new Vector3(myTransform.position.x, currentHeight, myTransform.position.z);

        myTransform.LookAt(targetTransform);

    }
   

   

    // Update is called once per frame
    void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }
        if (targetTransform == null)
        {
            targetTransform = Target.transform;
        }
        switch (CameraState)
        {
            case CameraViewPointState.THIRD:
                ThirdView();
                break;
            case CameraViewPointState.FIRST:
                break;
        }
    }
}
