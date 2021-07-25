using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionSetter : MonoBehaviour
{
    void Start()
    {
        Player player = FindObjectOfType<Player>();
        player.transform.position = player.defaultPosition;
    }
}
