using UnityEngine;
using UnityEngine.UI;

public class TextButtonAnimation : MonoBehaviour
{
	[SerializeField] Image outline;
	[SerializeField] Color hoverColor;

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
