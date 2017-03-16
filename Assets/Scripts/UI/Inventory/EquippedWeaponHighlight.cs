using UnityEngine;
using UnityEngine.UI;
using ui;

namespace ui.inventory
{
	public class EquippedWeaponHighlight : MonoBehaviour
	{
		private float width;

		// state
		public int Index = 0;

		public void setPosition(int position)
		{
			setWidthToParentsFirstChildsWidth();

			Vector3 pos = Vector3.zero;
			pos.x = width * position;
			transform.localPosition = pos;
		}

		private void setWidthToParentsFirstChildsWidth()
		{
			var rect = GetComponent<RectTransform>().rect;
			Debug.Assert(rect != null);

			var parent = transform.parent;
			Debug.Assert(parent != null);

			var firstSibling = parent.GetChild(0);
			Debug.Assert(firstSibling != null);

			var firstSiblingRectTransform = firstSibling.GetComponent<RectTransform>();
			Debug.Assert(firstSiblingRectTransform != null);
			width = firstSiblingRectTransform.rect.width;
		}
	}
}