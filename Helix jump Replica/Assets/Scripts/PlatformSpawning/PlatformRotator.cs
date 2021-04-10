using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixClone 
{
    public class PlatformRotator : MonoBehaviour
    {
        [Range (0,2)] public float speed;

        public float freeSpace = 0; 
        public float rotationOnStart;

        private float inc = 0;

        private void Start()
        {
            rotationOnStart = transform.rotation.eulerAngles.y;
            speed = speed * 2;
        }

        private void Update()
        {
            RotatePlatform();
        }

        private void RotatePlatform()
        {
            if (gameObject.GetComponent<PlatformProperties>().needToRotate)
            {
                inc += Time.deltaTime * speed;
                float sinedNum = Mathf.Sin(inc) * freeSpace / 2 + freeSpace / 2 + rotationOnStart;
                transform.rotation = Quaternion.Euler(Vector3.up * sinedNum);
            }
        }
    }
}
