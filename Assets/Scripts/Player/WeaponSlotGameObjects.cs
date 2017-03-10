using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
	public class WeaponSlotGameObjects : MonoBehaviour
	{
		[SerializeField] public GameObject[] BackWeaponSlots;

		[SerializeField] public GameObject EquippedRHS;
		[SerializeField] public GameObject EquippedADS;
	}
}