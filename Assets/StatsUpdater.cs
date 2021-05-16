using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUpdater : MonoBehaviour
{
    [SerializeField] Text damage;
    [SerializeField] Text armor;
    [SerializeField] Text hp;
    [SerializeField] Text skillPoints;
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        UpdatePlayerStatsUI();
    }

    private void UpdatePlayerStatsUI()
    {
        damage.text = player.Damage.ToString();
        armor.text = player.Armor.ToString();
        hp.text = player.MaxHp.ToString();
        skillPoints.text = player.SkillPoints.ToString();
    }

    public void AddDamage()
    {
        if (player.SkillPoints <= 0) { return; }
        player.Damage++;
        player.SkillPoints--;
        UpdatePlayerStatsUI();
    }

    public void RemoveDamage()
    {
        if(player.Damage <= 3) { return; }
        player.Damage--;
        player.SkillPoints++;
        UpdatePlayerStatsUI();
    }
}
