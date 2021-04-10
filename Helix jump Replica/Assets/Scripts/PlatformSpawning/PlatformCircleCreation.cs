using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixClone
{
    public class PlatformCircleCreation : MonoBehaviour
    {
        private PlatformProperties currentProperties;
        private GameObject[] _platforms;

        void Start()
        {
            currentProperties = gameObject.GetComponent<PlatformProperties>();
            _platforms = LvlCreator.instance.platforms;
            SpawnCircle();
        }

        private void SpawnCircle()
        {
            if (currentProperties.freeSpaceCounter < 247)
            {               
                SpawnWall();

                GameObject newPlatform = Instantiate(PlatformPicker(),
                    Vector3.up * gameObject.transform.position.y,
                    Quaternion.identity,
                    LvlCreator.instance.transform);
                PlatformProperties newPlatformProp = newPlatform.GetComponent<PlatformProperties>();
                float newPlatformRotation = currentProperties.angleOfArc / 2 + newPlatformProp.angleOfArc / 2 + currentProperties.currentRotation;
                newPlatform.transform.Rotate(Vector3.up * (newPlatformRotation));
                newPlatformProp.currentRotation = newPlatform.transform.rotation.eulerAngles.y;
                newPlatformProp.freeSpaceCounter = currentProperties.freeSpaceCounter + newPlatformProp.angleOfArc;
            }
            else
            {    
                if (LvlCreator.instance.lvlDifficulty > LvlCreator.instance.rotationTreshold &&
                    Random.Range(0, 101-LvlCreator.instance.lvlDifficulty) < LvlCreator.instance.chanceOfAdditionalObstacle)
                {
                    gameObject.GetComponent<PlatformRotator>().freeSpace = 360 - currentProperties.freeSpaceCounter;
                    currentProperties.needToRotate = true;
                }
            }
        }

        private GameObject PlatformPicker()
        {          
            float _lvlDifficulty = Random.Range(LvlCreator.instance.lvlDifficulty, 1.0f);
            int pickedIndex;
            int upperRange = 4;
            int lowerRange = 2;

            if (currentProperties.harmless && _lvlDifficulty > LvlCreator.instance.difficultyThreshold)
            {
                upperRange += 2;
                lowerRange += Random.Range(1, 3);
            }
            else
            {
                if (_lvlDifficulty > LvlCreator.instance.difficultyThreshold)
                {
                    lowerRange = 3;
                    upperRange = 4;
                }
                else
                {
                    lowerRange = Random.Range(2, 4);
                    upperRange = 4;
                }
            }

            pickedIndex = Random.Range(lowerRange, upperRange);

            for (int i = 0; i < _platforms.Length; i++)
            {
                if (_platforms[i].GetComponent<PlatformProperties>() != null)
                {
                    if ( _platforms[i].GetComponent<PlatformProperties>().platformDifficulty == pickedIndex)
                    {
                        return _platforms[i];
                    }
                }               
            }

            return _platforms[pickedIndex];
        }

        private void SpawnWall()
        {
            if (LvlCreator.instance.lvlDifficulty > LvlCreator.instance.wallsThreshold &&
                Random.Range(0, 101 - LvlCreator.instance.lvlDifficulty) < LvlCreator.instance.chanceOfAdditionalObstacle &&
                currentProperties.freeSpaceCounter < 90)
            {
                GameObject wall = Instantiate(_platforms[Random.Range(_platforms.Length - 2, _platforms.Length)],
                    Vector3.up * gameObject.transform.position.y,
                    Quaternion.Euler(Vector3.up * gameObject.transform.rotation.eulerAngles.y),
                    LvlCreator.instance.transform);
                wall.transform.position += Vector3.up * (wall.transform.localScale.y / 2 + gameObject.transform.localScale.y / 2);
            }
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }
    }
}
