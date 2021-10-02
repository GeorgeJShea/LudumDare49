using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
}

public class BackColor : MonoBehaviour
{
    [Header("Brick Settings")]
    [SerializeField] private GameObject colorBrick;
    [Tooltip("Will loop through this be sure to have the same start and end color")]
    public List<Gradient> gradientList;
    private Gradient gradientSet;

    [Tooltip("Larger the number the slower the fade")]
    [Header("Gradiant Speed")]
    [SerializeField] float duration;
    [Tooltip("Changes how fast the brick will fall")]
    [SerializeField] public float fallSpeed;
    float gradiantValue = 0f;


    [Header("Backdrop")]
    [SerializeField]private GameObject backdrop;
    Color color;

    public Dictionary<string, Queue<GameObject>> poolDic;
    public List<Pool> pools;

    private void Start()
    {
        poolDic = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for( int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDic.Add(pool.tag, objectPool);
        }


        //Debug.Log("STart");
        GameObject brick = SpawnFromPool("brick", gameObject.transform.position, Quaternion.identity);
        brick.GetComponent<SpriteRenderer>().color = color;

        gradientSet = gradientList[Random.Range(0, gradientList.Count)];
        backdrop.GetComponent<SpriteRenderer>().color = gradientSet.Evaluate(0);
    }
    void Update()
    {
        ColorRadiance();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject brick = SpawnFromPool("brick", gameObject.transform.position, Quaternion.identity);
        brick.GetComponent<SpriteRenderer>().color = color;
    }

    private GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        GameObject objectSpawn = poolDic[tag].Dequeue();
        objectSpawn.SetActive(true);
        objectSpawn.transform.position = position;
        objectSpawn.transform.rotation = rotation;

        poolDic[tag].Enqueue(objectSpawn);

        return objectSpawn;
    }
    private void ColorRadiance()
    {
        float value = Mathf.Lerp(0f, 1f, gradiantValue);
        gradiantValue += Time.deltaTime / duration;
        color = gradientSet.Evaluate(value);
        // Resets Gradiant      gradiantValue is on a 0 to 1 scale
        if (gradiantValue > 1)
        {
            gradiantValue = 0f;
            value = Mathf.Lerp(0f, 1f, gradiantValue);
        }
    }
}