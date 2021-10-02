using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool hasBlock = false;
    private Collider2D currentBlockCollider;
    private float direction = 1f;
    private bool isResetting = true;
    private PolygonCollider2D colliderRef;
    private bool gamePlaying = true;
    public GameObject block;
    public float speed = 1f;
    public float leftBound = -9f;
    public float rightBound = 9f;

    // Start is called before the first frame update
    void Start()
    {
        // this is used to manage interactions between the platform and the block
        colliderRef = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePlaying) {

            // check if the player is trying to drop the block
            if (Input.GetKeyDown(KeyCode.Space) && hasBlock) {
                // releaseBlock
                Physics2D.IgnoreCollision(colliderRef, currentBlockCollider);
                hasBlock = false;
                // This tells it to move back to the start position
                isResetting = true;
            }

            // if there is a block in the carrier, move the carrier back and forth
            if (hasBlock || isResetting) {
                // move left and right
                movePlatform();
            }
        } else if (Input.GetKeyDown(KeyCode.Return)) {
            // Reset the game
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
            // Tell it we don't have a block, in case we did when the game ended
            hasBlock = false;
            // turn the game back in
            gamePlaying = true;
            spawnNewBlock();
        }
    }

    void movePlatform() {
        float xVal = transform.position.x;
        // If it hits the bounds, change direction
        if (xVal > rightBound || isResetting) {
            direction = -1f;
        } else if (xVal < leftBound) {
            direction = 1f;
        }
        transform.position = new Vector3(xVal+(direction*speed)*Time.deltaTime, 3.4f, 0); 
    }

    public void setGameState(bool gState) {
        // this lets other classes stop or start the game - this should maybe more to GameManager
        gamePlaying = gState;
        if (!gState) {
            GameObject.FindWithTag("GameOver").GetComponent<UnityEngine.UI.Text>().enabled = true;
        }
    }

    private void spawnNewBlock() {
        isResetting = false;
        direction = 1f;
        // spawn block once
        transform.position = new Vector3(-8f,3.4f,0);
        Vector3 spawnLocation = new Vector3(-8.45f, 6.19f, 0);
        Instantiate(block, spawnLocation, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // if the platform is colliding with a block
        if (other.gameObject.CompareTag("Block")) {
            // Say it has a block
            hasBlock = true;
            // reference the collider of the block - we'll use this to release the block later
            currentBlockCollider = other.collider;
        } else if (other.gameObject.CompareTag("Respawn")) {
            // If we bump into the respawn object while resetting, spawn a block
            if (isResetting) {
                spawnNewBlock();
            }
        }
    }
}
