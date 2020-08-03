using System;
using System.Collections.Generic;
using UnityEngine;


public class Actions : MonoBehaviour
{
    [HideInInspector]
    public List<string> actions = new List<string> {"Поиграть", "Покормить", "Погладить", "Дать пинка"};
    [HideInInspector]
    public List<string> moods = new List<string> {"Плохое", "Хорошее", "Отличное"};
    

    public List<ActionMood> keys = new List<ActionMood>();
    public ActionMood GetKey(string action, string mood) {
        foreach (var key in keys)
        {
            if (key.action == action && key.mood == mood) 
            {
                return key;
            }
        }
        var new_key = new ActionMood(action, mood);
        keys.Add(new_key);
        return new_key;
    }

    [Serializable] 
    public class DictionaryOfActionMood : SerializableDictionary<ActionMood, Effect> {}
    public DictionaryOfActionMood effect_by_action_and_mood = new DictionaryOfActionMood();

    [Serializable] 
    public class DictionaryOfIcons : SerializableDictionary<string, Sprite> {}
    public DictionaryOfIcons moodIcons = new DictionaryOfIcons();

    public Menu menu = new Menu();

    public void Init() {
        foreach (var action in actions)
        {
            foreach (var mood in moods)
            {
                moodIcons.Add(mood, null);
                effect_by_action_and_mood.Add(GetKey(action, mood), new Effect());
            }
            menu.AddButton(action);
        }
    }

    public void AddAction(string action)
    {
        actions.Add(action);
        foreach (var mood in moods)
        {
            effect_by_action_and_mood.Add(GetKey(action, mood), new Effect());
        }
        menu.AddButton(action);
    }

    public void RemoveAction(string action)
    {
        actions.Remove(action);
        foreach (var mood in moods)
        {
            effect_by_action_and_mood.Remove(GetKey(action, mood));
        }
    }

    public void AddMood(string mood)
    {
        moods.Add(mood);
        moodIcons.Add(mood, null);
        foreach (var action in actions)
        {
            effect_by_action_and_mood.Add(GetKey(action, mood), new Effect());
        }
    }

    public void RemoveMood(string mood)
    {
        moods.Remove(mood);
        moodIcons.Remove(mood);
        foreach (var action in actions)
        {
            effect_by_action_and_mood.Remove(GetKey(action, mood));
        }
    }

    public void Test()
    {
        foreach (var mood in moods)
        {
            moodIcons.Add(mood, null);
        }
    }
}


[System.Serializable]
public struct ActionMood
{
    public string action;
    public string mood;
    public ActionMood(string _action, string _mood)
    {
        action = _action;
        mood = _mood;
    }
}


[System.Serializable]
public class Effect 
{
    public string reaction;
    public int next_mood;
}
