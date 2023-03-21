using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConfirmation
{
	string Message { get; set; }
	string[] ButtonTexts { get; set; }
	Action[] ButtonActions { get; set; }

	void OnConfirmationEnd();
}

