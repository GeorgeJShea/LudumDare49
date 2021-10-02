using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScorer : MonoBehaviour
{
    private GameManagement gameManagement;
    public GameObject smokePrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.FindWithTag("GameController");
        gameManagement = manager.GetComponent<GameManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void checkCameraPosition(float blockPosition) {
        GameObject camera = GameObject.FindWithTag("MainCamera");
        Vector3 cameraPosition = camera.transform.position;
        if (blockPosition > cameraPosition.y) {
            camera.transform.position = new Vector3(cameraPosition.x, blockPosition, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Block")) {
            gameManagement.updateScore(10);
            Vector3 smokePosition = new Vector3(transform.position.x, transform.position.y-0.35f, 0f);
            Instantiate(smokePrefab, smokePosition, Quaternion.identity);
            checkCameraPosition(other.transform.position.y);
        }
    }
}
