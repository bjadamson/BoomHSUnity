using UnityEngine;
using UnityEngine.UI;

namespace ui.inventory
{
	public class InventoryItem : MonoBehaviour
	{
		public Text Text;
		public Button ImageButton;
		public int InventoryId = -1;

		// drag state
		private Transform draggedItem;

		void Start()
		{
			Text = GetComponentInChildren<Text>();
			Debug.Assert(Text != null);

			ImageButton = GetComponentInChildren<Button>();
			Debug.Assert(ImageButton != null);
		}
	}
}