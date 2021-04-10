using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixClone
{
    public class PlatformProperties : MonoBehaviour
    {
        public bool harmless;
        public int platformDifficulty;
        public float angleOfArc;

        [HideInInspector] public float currentRotation;
        [HideInInspector] public float freeSpaceCounter;
        [HideInInspector] public bool needToRotate = false;

        void Awake()
        {
            currentRotation = gameObject.transform.rotation.eulerAngles.y;
            freeSpaceCounter += angleOfArc;
        }
    }
}
