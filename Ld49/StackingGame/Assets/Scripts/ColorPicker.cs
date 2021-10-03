using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    public Gradient gradient;

    // Start is called before the first frame update
    void Start()
    {
        Color selectedColor = gradient.Evaluate(Random.Range(0f,1f));
        Debug.Log(selectedColor);
        GetComponent<SpriteRenderer>().color = selectedColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
