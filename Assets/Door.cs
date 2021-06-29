using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform closePosition;
    public Transform openPosition;
    public float openSpeed = 5.0f;
    public float closeSpeed = 5.0f;
    public bool open = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, openPosition.position, Time.deltaTime * openSpeed);
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, closePosition.position, Time.deltaTime * closeSpeed);
        }
    }

    public void SetOpen(bool open)
    {
        this.open = open;
    }
}
