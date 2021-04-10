using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HelixClone
{
    public class LvlCreator : MonoBehaviour
    {
        [Range(0, 1)] public float lvlDifficulty = 0; 
        [Range(0, 1)] public float difficultyThreshold = 0.5f;  // inc chance of harming platforms
        [Range(0, 1)] public float wallsThreshold = 0.1f; // inc chance of walls
        [Range(0, 1)] public float rotationTreshold = 0.15f;  // inc chance of rotating platforms
        [Range(0, 100)] public int chanceOfAdditionalObstacle = 10; // additional obstacle - walls and rotating platforms

        public int spaceBetweenFloors = 7;
        public int numberOfFloors; // make dependancy of diff
        public GameObject[] platforms;

        public static LvlCreator instance;      

        void Awake()
        {
            if(LvlCreator.instance != null)
            {
                Destroy(gameObject);
                return;
            }
            LvlCreator.instance = this;
            if(Time.timeScale != 1)
            {
                Time.timeScale = 1;
            } 
        }

        private void Start()
        {
            EventHandler.onEndingLvl += StartNewLvl;
            lvlDifficulty = PlayerPrefs.GetFloat("lvlDifficulty");
            numberOfFloors = 10 + Mathf.RoundToInt(lvlDifficulty * 100 / 2);
            FloorSpawner(numberOfFloors);
        }
        
        public void ResetThisLvl()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ResetLvlCreator()
        {
            lvlDifficulty = 0;
            StartNewLvl();
        }

        public void StartNewLvl()
        {
            lvlDifficulty += 0.01f;
            if ((lvlDifficulty % 10) % 2 == 0)
            {
                numberOfFloors++;
            }
            PlayerPrefs.SetFloat("lvlDifficulty",lvlDifficulty);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void FloorSpawner(int floors)
        {
            Instantiate(platforms[0], Vector3.zero, Quaternion.identity);

            GameObject cylinder = Instantiate(platforms[1],
                Vector3.up * ((spaceBetweenFloors * floors) / 2 + 10),
                Quaternion.Euler(new Vector3(0, 0, 0)),
                transform);
            cylinder.transform.localScale = new Vector3(4.5f, (spaceBetweenFloors * floors) / 2 + 10, 4.5f);

            for (int i = 1; i < floors; i++)
            {
                Instantiate(platforms[Random.Range(4, platforms.Length - 2)],
                    Vector3.up * i * spaceBetweenFloors,
                    Quaternion.Euler(Vector3.up * Random.Range(-180, 181)),
                    transform);
                Instantiate(platforms[3],
                    Vector3.up * i * spaceBetweenFloors,
                    Quaternion.identity,
                    transform);
            }
            Instantiate(platforms[2], Vector3.up * floors * spaceBetweenFloors, Quaternion.Euler(Vector3.up * Random.Range(-80, 120)), transform);
        } 

        private void OnDestroy()
        {
            EventHandler.onEndingLvl -= StartNewLvl;
        }
    }
}
