using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    [SerializeField] private GameObject[] _characterPrefabs;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private TMP_Text _label;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        GameObject prefab = _characterPrefabs[selectedCharacter];
        GameObject clone = Instantiate(prefab, _spawnPoint.position, Quaternion.identity);
        clone.SetActive(true);
        _label.text = prefab.name;
    }
}
