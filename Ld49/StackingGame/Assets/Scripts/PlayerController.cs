using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float direction = 1f;
    private bool gamePlaying = true;
    public GameObject block;
    public float speed = 1f;
    public float leftBound = -9f;
    public float rightBound = 9f;

    private float y_value = 4f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gamePlaying) {

            // check if the player is trying to drop the block
            if (Input.GetKeyDown(KeyCode.Space)) {
                //instantiate block
                spawnNewBlock();
            }
            movePlatform();
        } else if (Input.GetKeyDown(KeyCode.Return)) {
            // Reset the game
            //Application.LoadLevel(Application.loadedLevel);
            UnityEngine.SceneManagement.SceneManager.LoadScene("WaylundScene");
             /*
            // turn off the game over screen
            GameObject.FindWithTag("GameOver").GetComponent<UnityEngine.UI.Text>().enabled = false;
            // reset score
            GameObject.FindWithTag("GameController").GetComponent<GameManagement>().resetScore();
            // clear out the blocks
            GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
            for (int i = 0; i < allBlocks.Length; i++) {
                // don't delete the landing pad
                if (allBlocks[i].name != "LandingPlatform") {
                    GameObject.Destroy(allBlocks[i]);
                }
            }
            y_value=4f;
            Transform cameraTransform = GameObject.FindWithTag("MainCamera").transform;
            cameraTransform.position = new Vector3(cameraTransform.position.x,0f,cameraTransform.position.z);
            // turn the game back in
            gamePlaying = true;
            */
        }
    }

    void movePlatform() {
        float xVal = transform.position.x;
        // If it hits the bounds, change direction
        if (xVal > rightBound) {
            direction = -1f;
        } else if (xVal < leftBound) {
            direction = 1f;
        }
        transform.position = new Vector3(xVal+(direction*speed)*Time.deltaTime, y_value, 0); 
    }

    public void setGameState(bool gState) {
        // this lets other classes stop or start the game - this should maybe more to GameManager
        gamePlaying = gState;
        if (!gState) {
            GameObject.FindWithTag("GameOver").GetComponent<UnityEngine.UI.Text>().enabled = true;
        }
    }

    public void incrementYValue(float addOn) {
        y_value += addOn;
    }
    private void spawnNewBlock() {
        // spawn block once
        //transform.position = new Vector3(-8f,3.4f,0);
        Vector3 spawnLocation = new Vector3(transform.position.x, 3.4f, 0);
        Instantiate(block, transform.position, Quaternion.identity);
    }

}
