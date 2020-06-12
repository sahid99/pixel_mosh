using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TDMManager : MonoBehaviour
{
    public static int bScore;
    public static int rScore;
    public static int sceneIndex;
    public TMP_Text RedScore;
    public TMP_Text BlueScore;
    public TMP_Text TimeLeft;
    // Start is called before the first frame update
    void Start()
    {
        RedScore = RedScore.GetComponent<TMP_Text>();
        BlueScore = BlueScore.GetComponent<TMP_Text>();
        TimeLeft = TimeLeft.GetComponent<TMP_Text>();
        bScore = 0;
        rScore = 0;
        RedScore.text = rScore.ToString();
        BlueScore.text = bScore.ToString();
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        RedScore.text = rScore.ToString();
        BlueScore.text = bScore.ToString();
    }
    void GameOver(){

    }
}
