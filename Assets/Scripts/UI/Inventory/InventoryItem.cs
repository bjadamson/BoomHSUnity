using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using player;

namespace ui
{
	namespace inventory
	{
		public class InventoryItem : MonoBehaviour
		{
			public Text Text;
			public Button ImageButton;
			public int InventoryId = -1;

			void Start() {
				Text = GetComponentInChildren<Text>();
				Debug.Assert(Text != null);

				ImageButton = GetComponent<Button>();
				Debug.Assert(ImageButton != null);
			}
		}
	}
}