using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    [SerializeField] private MonsterStats _monsterHealthBar;
    [SerializeField] private Text _healthText;
    [SerializeField] private int _monsterMaxHealth;
    [SerializeField] private int _monsterCurrentHealth;
    [SerializeField] private Slider _slider;

    void Start()
    {
        SaveGame.MaxHp = _monsterMaxHealth = 200;
        SaveGame.CurrentHp = _monsterCurrentHealth = _monsterMaxHealth;
        SetSliderValues(SaveGame.MaxHp, SaveGame.CurrentHp);
        _healthText.text = $"{_monsterCurrentHealth}/{_monsterMaxHealth}";
    }

    public void SetSliderValues(int max, int value)
    {
        _slider.maxValue = max;
        _slider.value = value;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(50);
            if (SaveGame.CurrentHp <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }

    }

    void TakeDamage(int damage)
    {
        SaveGame.CurrentHp -= damage;
        _monsterHealthBar.SetHealth();
        _healthText.text = $"{SaveGame.CurrentHp}/{SaveGame.MaxHp}";
    }

}
