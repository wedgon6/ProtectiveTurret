using System.Collections;
using System.Collections.Generic;
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

    private void OnAddExperience()
    {
        _currentExperience++;

        if(_currentExperience == _experienceToNextLvl)
        {

        }
    }
}
