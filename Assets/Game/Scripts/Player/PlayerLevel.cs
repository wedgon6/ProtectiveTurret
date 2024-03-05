using System;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{ 
    private const int StartExperienceToNextLvl = 3;
    private const int FactorExperienceToNextLvl = 2;
    private const int StartPalerLvl = 1;

    private int _currentPlayerLvl = StartPalerLvl;
    private int _experienceToNextLvl = StartExperienceToNextLvl;
    private int _currentExperience = 0;

    public int ExperienceToNextLvl => _experienceToNextLvl;
    public int CurrentExperience => _currentExperience;
    public int CurrentPlayerLvl => _currentPlayerLvl;

    public Action<int, int> OnPlayerExpirianceChanget;
    public Action OnPlayerLvlChenget;
    public Action OnDataChenged;
    public Action SetLvLPlayer;

    public void SetData(int currentLvl, int currentExperiance)
    {
        _currentPlayerLvl = currentLvl;
        _currentExperience = currentExperiance;
        OnPlayerExpirianceChanget?.Invoke(_currentExperience, _experienceToNextLvl);
        SetLvLPlayer?.Invoke();
    }

    public void AddExperience()
    {
        _currentExperience++;
        OnPlayerExpirianceChanget?.Invoke(_currentExperience,_experienceToNextLvl);

        if(_currentExperience == _experienceToNextLvl)
        {
            _currentExperience = 0;
            _currentPlayerLvl++;
            OnPlayerExpirianceChanget?.Invoke(_currentExperience, _experienceToNextLvl);
            OnPlayerLvlChenget?.Invoke();
        }

        OnDataChenged?.Invoke();
    }
}
