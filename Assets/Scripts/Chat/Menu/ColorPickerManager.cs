using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerManager : MonoBehaviour {
	[SerializeField] private ChatManager chatManager;
	[SerializeField] private ColorPicker colorPicker;

	public void setCurrentTabBgColorToColorPicker() {
		//chatManager.setRightClickedTabBgColor (colorPicker.CurrentColor);
	}

	public void setCurrentPanelTextColor() {
		chatManager.setPanelTextColor (colorPicker.CurrentColor);
	}

	public void setScrollViewBgColor() {
		chatManager.setScrollViewBgColor (colorPicker.CurrentColor);
	}

	public void setChatOverlayTransparency() {
		chatManager.setOverlayTransparency (colorPicker.CurrentColor.a);
	}
}