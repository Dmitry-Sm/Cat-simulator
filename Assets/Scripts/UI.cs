using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField]
    GameObject catPanel;
    [SerializeField]
    GameObject reactionPanel;
    [SerializeField]
    GameObject buttonPanel;
    [SerializeField]
    GameObject header;
    [SerializeField]
    UnityEngine.UI.Image catImage;

    Actions a;
    string current_mood;
    UnityEngine.UI.Text moodLabel;
    UnityEngine.UI.Text reactionLabel;
    

    private void Start() {
        a = Object.FindObjectOfType<Actions>();
        current_mood = a.moods[0];
        moodLabel = catPanel.GetComponentInChildren<UnityEngine.UI.Text>();
        moodLabel.text = "Настроение: " + current_mood;
        reactionLabel = reactionPanel.GetComponentInChildren<UnityEngine.UI.Text>();
        catImage.sprite = a.moodIcons[current_mood];
    }

    public void ActionButton(string s)
    {
        Effect e = a.effect_by_action_and_mood[a.GetKey(s, current_mood)];
        current_mood = a.moods[e.next_mood];
        moodLabel.text = "Настроение: " + current_mood;
        catImage.sprite = a.moodIcons[current_mood];
        
        reactionPanel.SetActive(true);
        header.SetActive(false);
        catPanel.SetActive(false);
        buttonPanel.SetActive(false);

        reactionLabel.text = e.reaction;
    }

    public void ContinueButton()
    {
        reactionPanel.SetActive(false);
        header.SetActive(true);
        catPanel.SetActive(true);
        buttonPanel.SetActive(true);
    }

}
