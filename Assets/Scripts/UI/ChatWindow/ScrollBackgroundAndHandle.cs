using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ui
{
	namespace chat_window
	{
		public class ScrollBackgroundAndHandle : MonoBehaviour
		{
			[SerializeField] private Image scrollHandleBackground;
			[SerializeField] private Image scrollHandle;
			[SerializeField] private Image scrollViewBackground;

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
		}

	}
}