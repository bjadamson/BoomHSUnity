using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
	namespace chat_window
	{
		public class ColorPickerManager : MonoBehaviour
		{
			[SerializeField] private MenuItemManager menuManager;
			[SerializeField] private ChatViewManager chatManager;
			[SerializeField] private ColorPicker colorPicker;

			public void setCurrentTabBgColorToColorPicker ()
			{
				//chatManager.setRightClickedTabBgColor (colorPicker.CurrentColor);
			}

			public void setCurrentPanelTextColor ()
			{
				chatManager.setPanelTextColor (colorPicker.CurrentColor);
				hideMenuItems ();
			}

			public void setScrollViewBgColor ()
			{
				chatManager.setScrollViewBgColor (colorPicker.CurrentColor);
				hideMenuItems ();
			}

			public void setChatOverlayTransparency ()
			{
				chatManager.setOverlayTransparency (colorPicker.CurrentColor.a);
				hideMenuItems ();
			}

			public void setChatOverlayTransparencyPreview ()
			{
				menuManager.setChatOverlayTransparencyPreview (colorPicker.CurrentColor.a);
			}

			private void hideMenuItems ()
			{
				menuManager.hideAllTargets ();
			}
		}
	}
}