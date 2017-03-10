using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using player;

namespace ui
{
	namespace inventory
	{
		public class AmmoPanel : MonoBehaviour
		{
			[SerializeField] private PlayerBehavior PlayerBehavior;
			private Text AmmoRemainingText;
			private Image AmmoImage;

			void Start()
			{
				AmmoRemainingText = GetComponentInChildren<Text>();
				Debug.Assert(AmmoRemainingText != null);

				AmmoImage = GetComponentInChildren<Image>();
				Debug.Assert(AmmoImage != null);
			}

			public void setText(string value)
			{
				AmmoRemainingText.text = value;
			}

			public void setImage(Image value)
			{
				AmmoImage = value;
			}
		}
	}
}