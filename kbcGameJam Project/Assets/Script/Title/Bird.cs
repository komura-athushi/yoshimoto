using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{
    public float startXpos;
    public float x_pos;
    public float y_pos;
    public float z_pos;
    public float time;
    void Start()
    {
        startXpos = gameObject.transform.position.x;
        x_pos = gameObject.transform.position.x;
        y_pos = gameObject.transform.position.y;
        z_pos = gameObject.transform.position.z;
    }

    void Update()
    {
        time += Time.deltaTime;
        x_pos -= 50 * Time.deltaTime;
        if (time >= 16)
        {
            x_pos = startXpos;
            time = 0f;
        }
        gameObject.transform.position = new Vector3(x_pos, y_pos, z_pos);
    }
}