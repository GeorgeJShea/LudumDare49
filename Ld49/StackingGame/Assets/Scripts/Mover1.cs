using UnityEngine;
using System.Collections;

public class Mover1 : MonoBehaviour
{
    public GameObject cube;
    private bool dirRight = true;
    public float speed = 2.0f;

    BackColor backColor;
    void Start()
    {
        backColor = GameObject.Find("ColorManager").GetComponent<BackColor>();
        StartCoroutine(DoEveryFiveSeconds());
    }

    IEnumerator DoEveryFiveSeconds()
    {

        yield return new WaitForSeconds(1);
        DoSomething();
        StartCoroutine(DoEveryFiveSeconds());
    }

    // happens every 0.5 seconds
    void DoSomething()
    {
        GameObject tempCube = Instantiate(cube, transform.position, Quaternion.identity);
        tempCube.GetComponent<SpriteRenderer>().color = backColor.color;

    }
    void Update()
    {
        if (dirRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(-Vector2.right * speed * Time.deltaTime);

        if (transform.position.x >= 4.0f)
        {
            dirRight = false;
        }

        if (transform.position.x <= -4)
        {
            dirRight = true;
        }
    }
}