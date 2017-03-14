using UnityEngine;
using UnityEngine.UI;
using ui;

namespace ui.inventory
{
	public class ItemHighlight : MonoBehaviour
	{
		public bool isParentedBy(InventoryItem item)
		{
			Debug.Assert(item != null);
			return transform.parent == item.transform;
		}
	}
}