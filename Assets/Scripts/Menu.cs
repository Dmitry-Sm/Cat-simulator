using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[System.Serializable]
public class Menu
{
    [SerializeField]
    GameObject button_prefab;
    [SerializeField]
    GameObject buttons_panel;
    [SerializeField]
    List<UnityEngine.UI.Button> buttons = new List<UnityEngine.UI.Button>();

    public void AddButton(string action)
    {
        GameObject go = Object.Instantiate(button_prefab);
        go.transform.SetParent(buttons_panel.transform);
        go.GetComponentInChildren<UnityEngine.UI.Text>().text = action;

        var bc = go.GetComponent<UnityEngine.UI.Button>();
        UI ui = Object.FindObjectOfType<UI>();
        UnityAction<string> actionButton = new UnityAction<string>(ui.ActionButton);
        UnityEditor.Events.UnityEventTools.AddStringPersistentListener(bc.onClick, actionButton, action);

        buttons.Add(bc);
        PlaceButtons();
    }

    void ShowReaction(int i)
    {
        Debug.Log("effect" + i);
    }

    public void PlaceButtons()
    {
        int q = 0;
        float x = -1, y = -35;
        float x_step = 110, y_step = 70;

        foreach (var btn in buttons)
        {
            if (!btn) {
                return;
            }
            btn.transform.localPosition = new Vector3(x * x_step, y, 0);
            x *= -1;
            if (q++ % 2 == 1)
            {
                y -= y_step;
            }
        }
    }
}
