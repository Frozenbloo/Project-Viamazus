using System;

public interface IConfirmation
{
	string Message { get; set; }
	string[] ButtonTexts { get; set; }
	Action[] ButtonActions { get; set; }

	void OnConfirmationEnd();

	void ShowConfirmation();
}

