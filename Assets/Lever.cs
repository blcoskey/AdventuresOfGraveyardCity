using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    public bool flipped = false;
    public bool playerInRange = false;
    public Transform lever;
    public Text promptText;
    public Door door;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!flipped && playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Flip();
        }
    }

    public void Flip()
    {
        lever.Rotate(new Vector3(60, 0, 0));
        promptText.text = "";
        door.SetOpen(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
            if (!flipped)
                promptText.text = "F to use";
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
            promptText.text = "";
        }
    }
}
