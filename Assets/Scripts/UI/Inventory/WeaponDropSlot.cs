using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using player;

namespace ui.inventory
{
	public class WeaponDropSlot : MonoBehaviour
	{
		[SerializeField] private PlayerBehavior playerBehavior;

		void Start()
		{
			Debug.Assert(playerBehavior != null);
		}

		public void readParentsSiblingIndexThenEquipWeaponAtThatIndex()
		{
			int index = transform.parent.GetSiblingIndex();
			playerBehavior.ifWeaponAtPositionThenEquip(index);
		}
	}
}