using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameView : UIView<ResultsState>
{
    private static List<string> s_Blacklist = new List<string>(new string[] {
        "ASS", "FUK", "COK", "DIK", "SHT", "CNT", "TIT"
    });

    [SerializeField]
    private TMP_InputField m_NameText = null;

    [SerializeField]
    private Button m_SubmitButton = null;

    private string m_Name = "AAA";

    public void OnNameChanged()
    {
        m_Name = CorrectName(m_NameText.text);
        m_NameText.SetTextWithoutNotify(m_Name);

        if (m_Name.Length < m_NameText.characterLimit)
        {
            m_SubmitButton.interactable = false;
        }
        else
        {
            m_SubmitButton.interactable = true;
        }
    }

    public void OnSubmitClicked()
    {
        GetController().SubmitScoreWithName(m_Name);
    }

    public void OnCancelClicked()
    {
        GameController.UIManager.NavigateBack();
    }

    protected override void OnWillShow()
    {
        m_NameText.SetTextWithoutNotify(m_Name);

        if (m_Name.Length < m_NameText.characterLimit)
        {
            m_SubmitButton.interactable = false;
        }
        else
        {
            m_SubmitButton.interactable = true;
        }
    }

    protected override void OnShown()
    {
        m_NameText.Select();
    }

    private string CorrectName(string name)
    {
        string corrected = name.ToUpper();
        foreach (char c in corrected)
        {
            if ((c < 'a' && c > 'z') || (c < 'A' && c > 'Z'))
            {
                // Invalid character
                return m_Name;
            }
        }

        if (s_Blacklist.Contains(corrected))
        {
            // Blacklisted word
            return m_Name;
        }

        return corrected;
    }
}
