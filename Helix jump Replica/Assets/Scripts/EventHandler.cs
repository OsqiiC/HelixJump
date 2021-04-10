using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace HelixClone
{
    public class EventHandler : MonoBehaviour
    {
        public delegate void FloorPassing();
        public static event FloorPassing onPasageOfFloor;
        public static event FloorPassing onPlayerDeath;
        public static event FloorPassing onEndingLvl;


        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlatformProperties>() != null) 
            {
                if (onPasageOfFloor != null && other.GetComponent<PlatformProperties>().angleOfArc == 0)
                {
                    onPasageOfFloor();
                    Destroy(other);
                }                        
                if (onPlayerDeath != null && !other.GetComponent<PlatformProperties>().harmless)
                {
                    onPlayerDeath();
                }
            }
            if (other.CompareTag("Finish"))
            {
                
                if (onEndingLvl != null)
                {
                    onEndingLvl();
                }
            }
        }
    }
}
