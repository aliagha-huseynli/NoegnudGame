using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIDisplayer : MonoBehaviour
{
    public static string IS_IMAGE_ASSIGNED_STRING = "IsImageAssigned";
    public static string GENDER = "Gender";
    public static string NAME = "Name";

    [SerializeField] Player player = null;
    public SpriteRenderer _spriteRenderer;
    public Sprite MaleSprite, FemaleSprite;
    [SerializeField] private Slider _slider = null;

    public Text HealthText = null;
    
    public Text PlayerName = null;

    public Slider Slider;
    public Gradient Gradient;
    public Image FillImage;

    private void Start()
    {
        SetSliderValues(SaveGame.MaxHp, SaveGame.CurrentHp);
        UpdateHealth();
    }

    public void SetSliderValues(int max, int value)
    {
        _slider.maxValue = max;
        _slider.value = value;
    }

    public void UpdateHealth()
    {
        if (player.MaxHp == 0) { return; }
        Slider.maxValue = player.MaxHp;
        Slider.value = player.Hp;
        FillImage.color = Gradient.Evaluate(player.Hp / player.MaxHp);
        HealthText.text = $"{player.Hp}/{player.MaxHp}";
    }


}