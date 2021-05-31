using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUIController : MonoBehaviour
{
    [SerializeField] Text PlayerName = null;
    [SerializeField] GameObject _popup = null;
    [SerializeField] PlayerUIDisplayer playerUI;
    [SerializeField] Player player = null;
    [SerializeField] private GameObject _fade = null;
    [SerializeField] private GameObject cubes = null;
    public Text Damage = null;
    public Text Armor = null;
    public Text SkillPoint = null;

    private void Start()
    {
        //ClearGender();
        LoadGenderAndName();
        SetPlayerInfoUI();
    }


    private void SetPlayerInfoUI()
    {
        player = FindObjectOfType<Player>();
        Damage.text = $"{player.Damage}";
        Armor.text = $"{player.Armor}";
        SkillPoint.text = $"{player.SkillPoints}";
    }

    public static void ClearGender()
    {
        PlayerPrefs.DeleteKey((PlayerUIDisplayer.IS_IMAGE_ASSIGNED_STRING));
        PlayerPrefs.DeleteKey((PlayerUIDisplayer.GENDER));
        PlayerPrefs.DeleteKey(PlayerUIDisplayer.NAME);
    }

    public void SetPlayerName(TMP_InputField input)
    {
        if (string.IsNullOrEmpty(input.text)) { return; }
        string playerName = input.text;
        PlayerName.text = playerName;
        PlayerPrefs.SetString((PlayerUIDisplayer.NAME), input.text);
        _popup.SetActive((false));
        cubes.SetActive(true);
    }

    public void SetGender(int gender) // 0 for male, 1 for female
    {
        if (playerUI._spriteRenderer == null) { return; }
        switch (gender)
        {
            case (0):
                playerUI._spriteRenderer.sprite = playerUI.MaleSprite;
                break;
            case (1):
                playerUI._spriteRenderer.sprite = playerUI.FemaleSprite;
                break;
        }
        PlayerPrefs.SetString(PlayerUIDisplayer.IS_IMAGE_ASSIGNED_STRING, true.ToString());
        PlayerPrefs.SetInt(PlayerUIDisplayer.GENDER, gender);

        _fade.SetActive((false));

        OpenNameInputPopup();
    }

    private void OpenNameInputPopup()
    {
        _popup.SetActive((true));
    }

    private void LoadGenderAndName()
    {
        string genderAssigned = PlayerPrefs.GetString(PlayerUIDisplayer.IS_IMAGE_ASSIGNED_STRING);
        if (string.IsNullOrEmpty(genderAssigned)) { return; }
        if (!bool.Parse((genderAssigned))) { return; }
        _fade.SetActive((false));
        playerUI._spriteRenderer.sprite = PlayerPrefs.GetInt(PlayerUIDisplayer.GENDER) == 0 ? playerUI.MaleSprite : playerUI.FemaleSprite;
        string previousName = PlayerPrefs.GetString(PlayerUIDisplayer.NAME);
        if (!string.IsNullOrEmpty(previousName))
        {
            PlayerName.text = previousName;
        }
        else
        {
            _popup.SetActive((true));
        }
    }

    public void Update()
    {
        SetPlayerInfoUI();
    }
}
