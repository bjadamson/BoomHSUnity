using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ui.chat_window.scroll;

namespace ui
{
	namespace chat_window
	{
		public class TransparencyManager : MonoBehaviour
		{
			[SerializeField] private CanvasGroup chatCanvasGroup;
			[SerializeField] private ScrollViewManager scrollBackground;
			[SerializeField] private float chatWindowCanvasTransparentAlpha = 0.55f;
			[SerializeField] private float chatWindowCanvasOpaqueAlpha = 1.0f;

			[SerializeField] private float scrollBarTransparentAlpha = 0.55f;
			[SerializeField] private float scrollBarOpaqueAlpha = 1.0f;
			[SerializeField] private float scrollViewOpaqueAlpha = 0.80f;

			public void makeOpaque ()
			{
				chatCanvasGroup.alpha = chatWindowCanvasOpaqueAlpha;
				scrollBackground.setAlpha (scrollBarOpaqueAlpha, scrollViewOpaqueAlpha);
			}

			public void makeTransparent ()
			{
				chatCanvasGroup.alpha = chatWindowCanvasTransparentAlpha;
				scrollBackground.setAlpha (scrollBarTransparentAlpha, scrollBarTransparentAlpha);
			}
		}
	}
}