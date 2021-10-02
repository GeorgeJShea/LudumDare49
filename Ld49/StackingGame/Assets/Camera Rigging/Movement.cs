using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speedFactor;
    [SerializeField] private BackColor backcolor;
    private void Start()
    {
        backcolor = GameObject.Find("ColorManager").GetComponent<BackColor>();
        speedFactor = backcolor.fallSpeed;
    }
    void Update()
    {
        transform.Translate(Vector3.down * speedFactor * Time.deltaTime);
    }
}
