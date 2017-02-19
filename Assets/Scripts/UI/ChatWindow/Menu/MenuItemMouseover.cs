using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ui
{
	namespace chat_window
	{
		public class MenuItemMouseover : MonoBehaviour, MenuItem, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
		{
			[SerializeField] private MenuItemManager menuManager;
			[SerializeField] private GameObject targetPanel;
			private GameObject mouseoverPanel;

			void Start ()
			{
				mouseoverPanel = transform.Find ("MouseoverPanel").gameObject;
				menuManager.addMenuItem (this);

				hideHighlight ();
				hideTarget ();
			}

			public void OnPointerEnter (PointerEventData eventData)
			{
				menuManager.onPointerEnterMenuItem (this);
			}

			public void OnPointerExit (PointerEventData eventData)
			{
				menuManager.onPointerExitMenuItem (this);
			}

			public void OnPointerClick (PointerEventData eventData)
			{
				menuManager.onPointerClickMenuItem (this);
			}

			public void showHighlight ()
			{
				mouseoverPanel.SetActive (true);
			}

			public void hideHighlight ()
			{
				mouseoverPanel.SetActive (false);
			}

			public void hideTarget ()
			{
				if (targetPanel != null) {
					targetPanel.SetActive (false);
				}
			}

			public void showTarget ()
			{
				if (targetPanel != null) {
					targetPanel.SetActive (true);
				}
			}
		}

	}
}