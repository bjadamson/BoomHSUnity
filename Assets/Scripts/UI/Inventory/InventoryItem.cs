using UnityEngine;
using UnityEngine.UI;
using weapon;

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
			refreshChildren();
		}

		public void refreshChildren()
		{
			Text = transform.parent.GetComponentInChildren<Text>();
			Debug.Assert(Text != null);

			ImageButton = GetComponent<Button>();
			Debug.Assert(ImageButton != null);
		}
	}
}