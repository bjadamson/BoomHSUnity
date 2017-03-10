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

			[SerializeField] private GameObject tabAnchor;
			[SerializeField] private GameObject tabAnchorParent;

			[SerializeField] private float scrollBarTransparentAlpha = 0.55f;
			[SerializeField] private float scrollBarOpaqueAlpha = 1.0f;
			[SerializeField] private float scrollViewOpaqueAlpha = 0.80f;

			public void makeOpaque ()
			{
				chatCanvasGroup.alpha = chatWindowCanvasOpaqueAlpha;
				scrollBackground.setAlpha (scrollBarOpaqueAlpha, scrollViewOpaqueAlpha);

				scrollBackground.showEverything();

				// Here we insert the tabAnchor back into the hierarchy, but ensure that it is the "first" child so it
				// renders ontop of the scroll view.
				tabAnchor.transform.SetParent(tabAnchorParent.transform);
				tabAnchor.transform.SetSiblingIndex(0);
				tabAnchor.transform.localPosition = Vector3.zero;
				tabAnchor.transform.localRotation = Quaternion.identity;
				tabAnchor.transform.localScale = Vector3.one;
			}

			public void makeTransparent ()
			{
				chatCanvasGroup.alpha = chatWindowCanvasTransparentAlpha;
				scrollBackground.setAlpha (scrollBarTransparentAlpha, scrollBarTransparentAlpha);

				scrollBackground.hideEverything();
				tabAnchor.transform.SetParent(null);
			}
		}
	}
}