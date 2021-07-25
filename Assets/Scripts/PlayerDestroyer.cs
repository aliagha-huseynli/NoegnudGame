using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroyer : MonoBehaviour
{
    void Start()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            Destroy(player.gameObject);
        }
    }
}