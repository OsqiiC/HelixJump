using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace HelixClone
{
    public class ScoreCounter : MonoBehaviour
    {
        public TextMeshProUGUI scoreUI;

        public int score;
        public int scoreIncrement;

        private void Start()
        {
            IncreaseIncrement();
            score = 0;
            EventHandler.onPasageOfFloor += IncreaseScore;    
        }


        private void IncreaseScore()
        {
            score += scoreIncrement;
            scoreUI.text  = $"{score}";
        }
        private void IncreaseIncrement()
        {
            scoreIncrement = Mathf.RoundToInt(LvlCreator.instance.lvlDifficulty * 100);
        }

        private void ReserScoreCounter()
        {
            score = 0;
            scoreUI.text = $"{score}";
        }

        private void OnDestroy()
        {
            EventHandler.onPasageOfFloor -= IncreaseScore;
        }
    }
}
