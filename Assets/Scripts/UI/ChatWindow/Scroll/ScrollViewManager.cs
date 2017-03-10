using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ui
{
	namespace chat_window
	{
		namespace scroll
		{
			public class ScrollViewManager : MonoBehaviour
			{
				[SerializeField] private Image scrollHandleBackground;
				[SerializeField] private Image scrollHandle;
				[SerializeField] private Image scrollViewBackground;
				[SerializeField] private ScrollRect scrollRect;
				[SerializeField] private Scrollbar scrollbar;

				void Start ()
				{
					scrollbar.value = 0.0f;
				}

				public void resetPosition ()
				{
					scrollbar.value = 0.0f;
				}

				public void setAlpha (float scrollBarAlpha, float scrollViewAlpha)
				{
					Color color = scrollHandleBackground.color;
					color.a = scrollBarAlpha;

					scrollHandleBackground.color = color;
					scrollHandle.color = color;

					color = scrollViewBackground.color;
					color.a = scrollViewAlpha;
					scrollViewBackground.color = color;
				}

				public void setContentRect(RectTransform rect) {
					scrollRect.content = rect;
				}

				public void hideEverything() {
					hideImage(scrollHandleBackground);
					hideImage(scrollHandle);
					hideImage(scrollViewBackground);
				}

				public void showEverything()
				{
					showImage(scrollHandleBackground);
					showImage(scrollHandle);
					showImage(scrollViewBackground);
				}

				private static void hideImage(Image img) {
					var color = img.color;
					color.a = 0.0f;
					img.color = color;
				}

				private static void showImage(Image img) {
					var color = img.color;
					color.a = 1.0f;
					img.color = color;
				}
			}
		}
	}
}