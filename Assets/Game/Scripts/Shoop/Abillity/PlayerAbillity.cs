using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbillity : MonoBehaviour
{
    [SerializeField] private string _lable;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _price;
    [SerializeField] private float _multiplier;

    private int _currentLvl = 1;

    public string Lable => _lable;
    public Sprite Icon => _icon;
    public int Price => _price;
    private int CurrentLvl => _currentLvl;

}
