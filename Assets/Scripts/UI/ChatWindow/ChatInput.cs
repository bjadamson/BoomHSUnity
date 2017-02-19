using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
	namespace chat_window
	{
		public class ChatInput : MonoBehaviour
		{
			[SerializeField] private GameObject emotePanel;

			private InputField field;

			void Start ()
			{
				field = GetComponent<InputField> ();
			}

			void Update ()
			{
				if (Input.GetKeyDown (KeyCode.Tab)) {
					emotePanel.SetActive (!emotePanel.activeSelf);
				}
			}

			public void test ()
			{
				emotePanel.SetActive (false);
			}
		}


	}
}