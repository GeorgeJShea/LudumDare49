using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScorer : MonoBehaviour
{
    private GameManagement gameManagement;
    private Animator animator;
    public GameObject smokePrefab;
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.FindWithTag("GameController");
        gameManagement = manager.GetComponent<GameManagement>();
        if (name!="LandingPlatform") {
            animator = transform.GetChild(0).GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (name!="LandingPlatform") {
            if (transform.rotation.z > 0.1) {
                animator.SetTrigger("concerned");
            }
            //animator.SetFloat("Rotation", transform.rotation.z);
        }
    }

    private void checkCameraPosition(float blockPosition) {
        GameObject camera = GameObject.FindWithTag("MainCamera");
        Vector3 cameraPosition = camera.transform.position;
        if (blockPosition > cameraPosition.y) {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().incrementYValue(blockPosition-cameraPosition.y);
            camera.transform.position = new Vector3(cameraPosition.x, blockPosition, cameraPosition.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Block")) {
            gameManagement.updateScore(10);
            Vector3 smokePosition = new Vector3(transform.position.x, transform.position.y-0.35f, 0f);
            Instantiate(smokePrefab, smokePosition, Quaternion.identity);
            checkCameraPosition(other.transform.position.y);
            if (name != "LandingPlatform") {
                animator.SetTrigger("landed");
            }
        }
    }
}
