using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTrigger : MonoBehaviour
{
    public bool activateZombies = true;
    public List<NavMeshCharacter> zombies;
    public Collider _collider;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (var zombie in zombies)
            {
                if (activateZombies)
                {
                    zombie.target = other.gameObject.transform;
                }
                else
                {
                    zombie.target = null;
                }
            }
        }
    }
}
