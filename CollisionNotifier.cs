using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionNotifier : MonoBehaviour
{
    public delegate void collision();
    static public event collision onEggCollision;
    // Start is called before the first frame update

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Egg")
        {
            Debug.Log("Collision detected with egg");
            onEggCollision();
        }
    }
}
