using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CamereMoving
{
    public class CamMoveing : MonoBehaviour
    {
        public float LookSensitivityX = 10f;
        public float LookSensitivityY = 10f;

        private float degree1;
        private float degree2 = 50;

        public float MinVelue = 15;
        public float MaxVelue = 170;

        private float Angle = -90;

        public float radius1 = 3f;
        public float radius2 = 3f;

        public float hight = 1f;

        public Transform Player;
        public Transform CrossHead;
        public Transform Cam;



        void LateUpdate()
        {

            CrossHeadMove();
        }

        void CrossHeadMove()
        {
            float X = Input.GetAxisRaw("Mouse X");
           
            float xRot = X * LookSensitivityX;
            degree1 -= xRot;
            float radian1 = degree1 * Mathf.PI / 180;

            
            Vector3 CrossHeadPos = new Vector3(radius1 * Mathf.Cos(radian1) + Player.position.x, Player.position.y + hight, radius1 * Mathf.Sin(radian1) + Player.position.z);
            CrossHead.position = CrossHeadPos;




            CamMove();
        }

        void CamMove()
        {
            float Y = Input.GetAxisRaw("Mouse Y");

            float yRot = Y * LookSensitivityY;
            degree2 += yRot;


            if (degree2 <= MinVelue)
                degree2 = MinVelue;
            
            if (degree2 >= MaxVelue)
                degree2 = MaxVelue;

            float radian2 = Mathf.Clamp(degree2, MinVelue, MaxVelue) * Mathf.PI / 180;

            float radian3 = (degree1 + Angle) * Mathf.PI / 180;

           
            Vector3 CamPos = new Vector3((radius2 * Mathf.Sin(radian2) * Mathf.Cos(radian3)) + CrossHead.position.x, (radius2 * Mathf.Cos(radian2) + CrossHead.position.y), (radius2 * Mathf.Sin(radian2) * Mathf.Sin(radian3)) + CrossHead.position.z);

            Cam.position = CamPos;


            Cam.LookAt(CrossHead);
        }
    }
}
