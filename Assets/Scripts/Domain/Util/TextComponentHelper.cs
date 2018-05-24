using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 文字のアニメーション
/// </summary>
public class TextComponentHelper
{
	Text uiText;

    string currentText = string.Empty;
	float timeUntilDisplay = 0;
	float timeElapsed = 0;
	int lastUpdateCharacter = -1;
    const float defaultIntervalForCharacterDisplay = 0.05f;



	public TextComponentHelper (Text _uiText)
	{
		uiText = _uiText;
	}

	public bool IsCompleteDisplayText {
		get { return  Time.time > timeElapsed + timeUntilDisplay; }
	}

	public void CompleteDisplayText ()
	{
		timeUntilDisplay = 0;
	}

	public void Update ()
	{
		int displayCharacterCount = (int)(Mathf.Clamp01 ((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
        if (displayCharacterCount != lastUpdateCharacter && displayCharacterCount > 0) {
			uiText.text = currentText.Substring (0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;
		}
	}

    public void SetNextLine (string nextLineText, float intervalTime = defaultIntervalForCharacterDisplay)
	{
		currentText = nextLineText;
		timeUntilDisplay = currentText.Length * intervalTime;
		timeElapsed = Time.time;
		lastUpdateCharacter = -1;
	}
}