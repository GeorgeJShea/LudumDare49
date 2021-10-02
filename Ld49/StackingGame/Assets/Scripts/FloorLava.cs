using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLava : MonoBehaviour
{
    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        controller.setGameState(false);
        other.transform.GetChild(0).GetComponent<Animator>().SetTrigger("die");
    }
}
