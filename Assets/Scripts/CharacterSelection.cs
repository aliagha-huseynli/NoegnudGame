﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] _characters;
    [SerializeField] private int _selectedCharacter = 0;


    public void NextCharacter()
    {
        _characters[_selectedCharacter].SetActive(false);
        _selectedCharacter = (_selectedCharacter + 1) % _characters.Length;
        _characters[_selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        _characters[_selectedCharacter].SetActive(false);
        _selectedCharacter--;
        if (_selectedCharacter < 0)
        {
            _selectedCharacter += _characters.Length;
        }
        _characters[_selectedCharacter].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharacter", _selectedCharacter);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
