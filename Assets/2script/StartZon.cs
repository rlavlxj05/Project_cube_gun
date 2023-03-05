using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartZon : MonoBehaviour
{
    public Gamemanager manager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        manager.StageStart();
    }
}
