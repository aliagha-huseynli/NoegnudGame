using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<Item> Items = new List<Item>();
    [SerializeField] public List<Item> Potions = new List<Item>();
}
