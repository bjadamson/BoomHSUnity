using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
	namespace chat_window
	{
		public interface MenuItem
		{
			void hideHighlight ();

			void showHighlight ();

			void hideTarget ();

			void showTarget ();
		}
	}
}