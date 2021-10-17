using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    float highestVerticalPosition;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        highestVerticalPosition = Max(player.transform.position.y,transform.position.y);
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, highestVerticalPosition, transform.position.z), 20 * Time.deltaTime);
    }

    float Max(float a, float b)
    {
        return (a > b) ? a : b;
    }
}
