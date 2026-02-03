using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public AchievementManager instance;
    public List<Achievement> achievements;

    private void Awake()
    {
        instance = this;
        CreateAchievements();
    }

    public void CreateAchievements()
    {
        achievements = new List<Achievement>();

        achievements.Add(new Achievement("The Beginning", "Earn your first dollar", (object o) => GameLogic.instance.moneyCount >= 1));
    }

    
    private void Update()
    {
        CheckAchievementCompletion();
    }

    private void CheckAchievementCompletion()
    {
        if (achievements == null)
            return;

        foreach (var achievement in achievements)
        {
            achievement.UpdateState();
        }
    }


}

[System.Serializable]
public class Achievement
{
    public string name;
    public string description;
    public bool unlocked;
    public Predicate<object> requirement;

    public Achievement(string name, string description, Predicate<object> requirement)
    {
        this.name = name;
        this.description = description;
        this.requirement = requirement;
    }

    public void UpdateState()
    {
        if (unlocked)
        {
            return;
        }

        if (ConditionMet())
        {
            Debug.Log($"{name}: {description}");
            this.unlocked = true;
        }
    }


    public bool ConditionMet()
    {
        return requirement.Invoke(null);
    }

}
