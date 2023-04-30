using UnityEngine;
using UnityEngine.UI;

public class TextButtonAnimation : MonoBehaviour
{
	[SerializeField] private Image outline;
	[SerializeField] private Color hoverColor;

	private Color defaultColor;

	private void Start()
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
