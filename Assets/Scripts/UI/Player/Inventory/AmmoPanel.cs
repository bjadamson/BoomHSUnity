using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using player;

namespace ui
{
	namespace player
	{
		namespace inventory
		{
			public class AmmoPanel : MonoBehaviour
			{
				[SerializeField] private Player player;
				[SerializeField] private Text ammoRemainingText;

				void Update()
				{
					if (!player.isWeaponEquipped())
					{
						if (ammoRemainingText.gameObject.activeSelf)
						{
							ammoRemainingText.gameObject.SetActive(false);
						}
						return;
					}
					if (!ammoRemainingText.gameObject.activeSelf)
					{
						ammoRemainingText.gameObject.SetActive(true);
					}
					string value = player.equippedWeaponAmmoCount() + "/" + player.equippedWeaponMaxAmmo();
					ammoRemainingText.text = value;
				}
			}
		}
	}
}