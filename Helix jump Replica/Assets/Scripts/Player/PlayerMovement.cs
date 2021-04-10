using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixClone {
    public class PlayerMovement : MonoBehaviour
    {
        public float startPosOffsetY;
        public float startPosOffsetZ;
        public float cameraOffsetY;
        public float cameraOffsetZ;

        public float speed;
        public float jumpForce;
        public float gravity = -9.81f;
        public LayerMask groundMask;

        private float velocity = 0;
        private Camera cam;
        private float hor;

        void Start()
        {
            EventHandler.onPlayerDeath += ResetPlayerPosition;
            cam = Camera.main;
            DelayedExecute.instance.ExecuteMethod(ResetPlayerPosition,1);
        }

        // Update is called once per frame
        void Update()
        {
            MovePlayerAndCamera();
        }

        private void MovePlayerAndCamera()
        {
            hor = Input.GetAxis("Mouse X") * speed  * Time.deltaTime;
            if (Input.GetMouseButton(0))
            {
                cam.transform.RotateAround(Vector3.zero, Vector3.up, hor);
                transform.RotateAround(Vector3.zero, Vector3.up, hor);
            }
            //if (Physics.CheckBox(transform.position - Vector3.up * 0.6f, transform.localScale / 2 + (Vector3.up * -0.5f), Quaternion.identity, groundMask) && velocity < 0)
            if (Physics.CheckSphere(transform.position, transform.localScale.x / 2, groundMask) && velocity < 0)
            {
                velocity = jumpForce;
            }
            
            velocity += gravity * Time.deltaTime;
            Vector3 yPostion = Vector3.up * velocity * Time.deltaTime;
            cam.transform.position += yPostion;
            transform.position += yPostion;
        }

        private void ResetPlayerPosition()
        {
            if (LvlCreator.instance != null)
            {
                velocity = 0;
                transform.rotation = Quaternion.identity;
                cam.transform.rotation = Quaternion.Euler(Vector3.right * 25);
                transform.position = new Vector3(0, LvlCreator.instance.numberOfFloors * LvlCreator.instance.spaceBetweenFloors + startPosOffsetY, startPosOffsetZ);
                cam.transform.position = new Vector3(0, LvlCreator.instance.numberOfFloors * LvlCreator.instance.spaceBetweenFloors + cameraOffsetY, cameraOffsetZ);
            }
        }

        private void OnDestroy()
        {
            EventHandler.onPlayerDeath -= ResetPlayerPosition;
        }
    }
}
