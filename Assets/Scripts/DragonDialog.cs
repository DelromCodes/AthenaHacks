using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonDialog : MonoBehaviour {

	public Text TitleLabel;
	public Text ContentLabel;
	public Button ConfirmButton;
	public Button CancelButton;

	public void OnConfirmButtonPressed() {
		HideButtons();
		TitleLabel.text = "Thank you!";
		ContentLabel.text = "I can't wait to begin training!";
	}

	public void OnCancelButtonPressed() {
		HideButtons();
		TitleLabel.text = "Aw...";
		ContentLabel.text = "I guess I'll always be a baby...";
	}

	public void OnUnderlayButtonPressed() {
		Close();
	}

	protected void HideButtons() {
		ConfirmButton.gameObject.SetActive(false);
		CancelButton.gameObject.SetActive(false);
	}
		
	protected void Close() {
		gameObject.SetActive(false);
	}
}
