using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUpdater : MonoBehaviour
{
    [SerializeField] Text _damage;
    [SerializeField] Text _armor;
    [SerializeField] Text _hp;
   // [SerializeField] Text _healthBar;
    [SerializeField] Text _skillPoints;
    Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        UpdatePlayerStatsUI();
    }

    private void UpdatePlayerStatsUI()
    {
        print(_player.Damage);
        _damage.text = _player.Damage.ToString();
        _armor.text = _player.Armor.ToString();
        _hp.text = _player.MaxHp.ToString();
        _skillPoints.text = _player.SkillPoints.ToString();
        //_healthBar.text = $"{_player.Hp}/{_player.MaxHp}";

    }

    public void AddDamage()
    {
        if (_player.SkillPoints <= 0) { return; }
        _player.Damage++;
        _player.SkillPoints--;
        UpdatePlayerStatsUI();
    }

    public void AddArmor()
    {
        if (_player.SkillPoints <= 0) { return; }
        _player.Armor++;
        _player.SkillPoints--;
        UpdatePlayerStatsUI();
    }

    public void AddMaxHp()
    {
        if (_player.SkillPoints <= 0) { return; }
        _player.MaxHp++;
        _player.SkillPoints--;
        UpdatePlayerStatsUI();
    }

    public void RemoveDamage()
    {
        if(_player.Damage <= 3) { return; }
        _player.Damage--;
        _player.SkillPoints++;
        UpdatePlayerStatsUI();
    }

    public void RemoveArmor()
    {
        if (_player.Armor <= 3) { return; }
        _player.Armor--;
        _player.SkillPoints++;
        UpdatePlayerStatsUI();
    }

    public void RemoveMaxHp()
    {
        if (_player.MaxHp <= 50) { return; }
        _player.MaxHp--;
        _player.SkillPoints++;
        UpdatePlayerStatsUI();
    }
}
