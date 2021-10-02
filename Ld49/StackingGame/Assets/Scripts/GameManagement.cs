using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    private UnityEngine.UI.Text scoreText;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
       scoreText = GameObject.FindWithTag("Score").GetComponent<UnityEngine.UI.Text>(); 
    }

    public void updateScore(int points) {
        score = score + points;
        scoreText.text = score.ToString();
    }

    public void resetScore() {
        score = 0;
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
