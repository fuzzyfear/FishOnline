using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemies;
    public int numberOfEnemies;
    //public Vector2 enemySpawn;
    [HideInInspector] public List<GameObject> enemyList = new List<GameObject>();
    [HideInInspector] public int currentNumberOfEnemies;
    public Text victoryText;
    public Text currentTime;
    public Text bestTime;
    private float tempTime;
    private float highScore;
    private string highScoreString = "highScore";
    private PassingCheckpoints pc;

    void Awake()
    {
        pc = GameObject.Find("PassingCheckpoints").GetComponent<PassingCheckpoints>();
    }

    void Start()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject temp = Instantiate(enemy, pc.currentCheckpoint.position, Quaternion.identity);
            temp.transform.SetParent(enemies.transform);
            enemyList.Add(temp);
        }
        highScore = PlayerPrefs.GetFloat(highScoreString, 0);
        bestTime.text = "Best Time: " + highScore;
        currentNumberOfEnemies = enemyList.Count;
    }

    private void Update()
    {
        if(currentNumberOfEnemies != enemyList.Count)
        {
            currentNumberOfEnemies = enemyList.Count;
            Debug.Log(currentNumberOfEnemies + " left");
        }

        if(currentNumberOfEnemies > 0)
        {
            tempTime = (float)System.Math.Round(Time.timeSinceLevelLoad, 2);
            currentTime.text = "Current Time: " + tempTime + "s";
        }
        else
        {
            victoryText.enabled = true;
            if (Input.GetKey(KeyCode.R)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if(tempTime < highScore || highScore == 0)
            {
                bestTime.text = "Best Time: " + tempTime + "s";
                PlayerPrefs.SetFloat(highScoreString, tempTime);
                PlayerPrefs.Save();
            }
        }

        if (Input.GetKey(KeyCode.T))
        {
            bestTime.text = "Best Time: " + 0 + "s";
            PlayerPrefs.SetFloat(highScoreString, 0);
            PlayerPrefs.Save();
        }
    }
}
