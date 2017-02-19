using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ui
{
	namespace chat_window
	{
		public class ScrollBar : MonoBehaviour
		{
			private Scrollbar scrollbar;

			void Start ()
			{
				scrollbar = GetComponent<Scrollbar> ();
				scrollbar.value = 0.0f;
			}

			public void resetPosition ()
			{
				scrollbar.value = 0.0f;
			}
		}

	}
}