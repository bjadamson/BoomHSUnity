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
			
		public void setIcon(Sprite icon)
		{
			setIconAndRefresh(icon, 1.0f);
		}

		private void refreshChildren()
		{
			Text = transform.parent.GetComponentInChildren<Text>();
			Debug.Assert(Text != null);

			ImageButton = GetComponent<Button>();
			Debug.Assert(ImageButton != null);
		}

		private void setIconAndRefresh(Sprite icon, float alpha)
		{
			ImageButton.image.sprite = icon;

			var color = ImageButton.colors.normalColor;
			color.a = alpha;
			ImageButton.image.color = color;

			refreshChildren();
		}
	}
}