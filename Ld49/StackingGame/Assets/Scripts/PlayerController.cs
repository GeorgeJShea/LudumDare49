using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool hasBlock = false;
    private float direction = .01f;
    private bool isResetting = true;
    private int resetCounter = 0;
    private PolygonCollider2D colliderRef;
    private bool gamePlaying = true;
    public GameObject block;
    public float speed = 1f;
    public float leftBound = -9f;
    public float rightBound = 9f;

    // Start is called before the first frame update
    void Start()
    {
        colliderRef = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePlaying) {
            if (isResetting) {
                resetCounter++;
                if (resetCounter == 30) {
                    colliderRef.enabled = true;
                }
            }

            // check if the player is trying to drop the block

            if (Input.GetKeyDown(KeyCode.Space) && hasBlock) {
                // releaseBlock
                colliderRef.enabled = false;
                hasBlock = false;
                isResetting = true;
            }

            // if there is a block in the carrier, move the carrier back and forth
            if (hasBlock || isResetting) {
                // move left and right
                movePlatform();
            }
        } else if (Input.GetKeyDown(KeyCode.Return)) {
            GameObject.FindWithTag("GameOver").GetComponent<UnityEngine.UI.Text>().enabled = false;
            GameObject.FindWithTag("GameController").GetComponent<GameManagement>().resetScore();
            GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Block");
            for (int i = 0; i < allBlocks.Length; i++) {
                if (allBlocks[i].name != "LandingPlatform") {
                    GameObject.Destroy(allBlocks[i]);
                }
            }
            colliderRef.enabled = true;
            hasBlock = false;
            gamePlaying = true;
            spawnNewBlock();
        }
    }

    void movePlatform() {
        float xVal = transform.position.x;
            // If it hits the bounds, change direction
        if (xVal > rightBound || isResetting) {
            direction = -.01f;
        } else if (xVal < leftBound) {
            direction = .01f;
        }
        transform.position = new Vector3(xVal+(direction*speed), 3.4f, 0); 
    }

    public void setGameState(bool gState) {
        gamePlaying = gState;
        if (!gState) {
            GameObject.FindWithTag("GameOver").GetComponent<UnityEngine.UI.Text>().enabled = true;
        }
    }

    private void spawnNewBlock() {
        isResetting = false;
        resetCounter = 0;
        direction = .01f;
        // spawn block once
        transform.position = new Vector3(-8f,3.4f,0);
        Vector3 spawnLocation = new Vector3(-8.45f, 6.19f, 0);
        Instantiate(block, spawnLocation, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Block")) {
            hasBlock = true;
        } else if (other.gameObject.CompareTag("Respawn")) {
            if (isResetting) {
                spawnNewBlock();
            }
        }
    }
}
