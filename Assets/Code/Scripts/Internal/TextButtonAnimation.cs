using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextButtonAnimation : MonoBehaviour
{
	public Image outline;
	public Color hoverColor;

	private Color defaultColor;

	void Start()
	{
		defaultColor = outline.color;
	}

	public void OnPointerEnter()
	{
		outline.color = hoverColor;
	}

	public void OnPointerExit()
	{
		outline.color = defaultColor;
	}
}
