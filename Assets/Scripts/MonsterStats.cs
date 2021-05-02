using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterStats : MonoBehaviour
{
    #region Monster Health Bar

    public Slider Slider;
    public Gradient Gradient;
    public Image FillImage;
    public SaveGame SaveGame;

    public void SetHealth()
    {
        Slider.maxValue = SaveGame.MaxHp;
        Slider.value = SaveGame.CurrentHp;
        FillImage.color = Gradient.Evaluate(SaveGame.CurrentHp / SaveGame.MaxHp);
    }

    #endregion


}
