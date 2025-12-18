using UnityEngine;

public class imageScrolling : MonoBehaviour
{   
    public float speed = 0.1f;
    private Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y <= -21.75f)
        {
            transform.position = startPosition;
        }
    }
}