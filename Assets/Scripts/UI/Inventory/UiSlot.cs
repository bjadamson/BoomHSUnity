using UnityEngine;
using UnityEngine.UI;
using weapon;

namespace ui.inventory
{
	public class UiSlot : MonoBehaviour
	{
		public Text Text;
		public Button ImageButton;
		public int InventoryId = -1;

		void Start()
		{
			refreshChildren();
		}
			
		public void setUiIndexAndIcon(int index, Sprite icon)
		{
			setIndexAndIcon(index, icon, 1.0f);
		}

		private void refreshChildren()
		{
			Text = transform.parent.GetComponentInChildren<Text>();
			Debug.Assert(Text != null);

			ImageButton = GetComponent<Button>();
			Debug.Assert(ImageButton != null);
		}

		private void setIndexAndIcon(int index, Sprite icon, float alpha)
		{
			ImageButton.image.sprite = icon;
			InventoryId = index;

			var color = ImageButton.colors.normalColor;
			color.a = alpha;
			ImageButton.image.color = color;

			refreshChildren();
		}
	}
}