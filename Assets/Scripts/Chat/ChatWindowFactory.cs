﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatWindowFactory : MonoBehaviour {
	[SerializeField] private GameObject tabAnchor;
	[SerializeField] private GameObject panelAnchor;

	[SerializeField] private TabManager tabManager;
	[SerializeField] private TextPanelManager panelManager;
	private int id = 0;

	public void createDefaultWindow(ChatChannelList channelList) {
		createGeneralChatPanel (channelList);
		channelList.sendMessage("general", "Server version 0.");
		channelList.sendMessage("general", "Press 'enter' and then 'tab' to view chat commands.");
		channelList.sendMessage("general", "Welcome. Try not to die, k?");

		createWhisperChatPanel (channelList);
		createPartyChatPanel (channelList);
		createGuildChatPanel (channelList);
		createCombatLogChatPanel (channelList);

		panelManager.showGeneral ();
		tabManager.selectGeneralTab ();
	}

	private void createGeneralChatPanel(ChatChannelList channelList) {
		string name = "General";
		Color tabBgColor = new Color (0.6f, 0.502f, 0.38f);
		Color panelTextColor = Color.white;

		bool transparent = false;
		createNewChatPanel (name, transparent, tabBgColor, panelTextColor, channelList);
	}

	private void createWhisperChatPanel(ChatChannelList channelList) {
		string name = "Whisper";
		Color tabBgColor = new Color (0.588f, 0.031f, 0.722f);
		bool transparent = true;
		createNewChatPanel (name, transparent, tabBgColor, tabBgColor, channelList);
	}

	private void createPartyChatPanel(ChatChannelList channelList) {
		string name = "Group";
		Color tabBgColor = new Color (0.388f, 0.796f, 1.0f);
		bool transparent = true;
		createNewChatPanel (name, transparent, tabBgColor, tabBgColor, channelList);
	}

	private void createGuildChatPanel(ChatChannelList channelList) {
		string name = "Guild";
		Color tabBgColor = new Color (0.271f, 1.0f, 0.486f);
		bool transparent = true;
		createNewChatPanel (name, transparent, tabBgColor, tabBgColor, channelList);
	}

	private void createCombatLogChatPanel(ChatChannelList channelList) {
		string name = "Combat Log";
		Color tabBgColor = new Color (1.0f, 0.337f, 0.337f);
		bool transparent = true;
		createNewChatPanel (name, transparent, tabBgColor, tabBgColor, channelList);
	}

	private void createNewChatPanel(string channelName, bool transparent, Color tabBgColor, Color panelTextColor, ChatChannelList channelList) {
		GameObject newTab = new GameObject (channelName);
		newTab.transform.SetParent (tabAnchor.transform);
		RectTransform rectTransform = newTab.AddComponent<RectTransform> ();
		rectTransform.pivot = Vector2.zero;
		rectTransform.anchorMin = Vector2.zero;
		rectTransform.anchorMax = Vector2.zero;
		rectTransform.transform.localScale = Vector3.one;
		rectTransform.transform.localPosition = Vector3.zero;

		newTab.AddComponent<CanvasRenderer> ();
		Image image = newTab.AddComponent<Image> ();
		image.sprite = Resources.Load<Sprite> ("UIButton");
		Debug.Assert (image.sprite != null);
		image.color = tabBgColor;
		image.type = Image.Type.Sliced;
		image.fillCenter = true;

		Button button = newTab.AddComponent<Button> ();
		button.interactable = true;
		button.transition = Selectable.Transition.ColorTint;

		GameObject buttonText = new GameObject ("Text");
		buttonText.transform.SetParent (newTab.transform);

		rectTransform = buttonText.AddComponent<RectTransform> ();
		rectTransform.pivot = new Vector2 (0.5f, 0.5f);
		rectTransform.transform.position = Vector3.zero;
		rectTransform.transform.localScale = Vector3.one;
		rectTransform.transform.localPosition = Vector3.zero;
		rectTransform.anchorMin = Vector2.zero;
		rectTransform.anchorMax = Vector2.one;
		rectTransform.sizeDelta = Vector2.zero;
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;

		Text text = buttonText.AddComponent<Text>();
		text.text = channelName;
		text.alignment = TextAnchor.MiddleCenter;
		text.alignByGeometry = false;
		text.font = Resources.GetBuiltinResource<Font> ("Arial.ttf");
		text.fontSize = 13;
		text.fontStyle = FontStyle.Normal;
		text.supportRichText = true;
		text.horizontalOverflow = HorizontalWrapMode.Wrap;
		text.verticalOverflow = VerticalWrapMode.Truncate;
		text.resizeTextForBestFit = false;
		text.color = Color.white;

		Tabs tab = newTab.AddComponent<Tabs> ();
		tab.button = button;
		tab.manager = tabManager;
		tab.panelId = id++;
		tabManager.addTab (tab);

		GameObject newPanel = new GameObject (channelName + "Content");
		newPanel.transform.SetParent (panelAnchor.transform);
		rectTransform = newPanel.AddComponent<RectTransform> ();
		rectTransform.transform.position = Vector3.zero;
		rectTransform.transform.localScale = Vector3.one;
		rectTransform.transform.localPosition = Vector3.zero;
		rectTransform.anchorMin = Vector2.zero;
		rectTransform.anchorMax = Vector2.one;
		rectTransform.pivot = Vector2.zero;
		rectTransform.sizeDelta = Vector2.zero;
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;

		GridLayoutGroup gridLayout = newPanel.AddComponent<GridLayoutGroup> ();
		gridLayout.padding.left = 3;
		gridLayout.cellSize = new Vector2 (485.0f, 26.4f);
		gridLayout.startCorner = GridLayoutGroup.Corner.UpperLeft;
		gridLayout.startAxis = GridLayoutGroup.Axis.Vertical;
		gridLayout.childAlignment = TextAnchor.LowerLeft;
		gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
		gridLayout.constraintCount = 1;

		ContentSizeFitter fitter = newPanel.AddComponent<ContentSizeFitter> ();
		fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
		fitter.verticalFit = ContentSizeFitter.FitMode.MinSize;

		TextPanel textPanel = newPanel.AddComponent<TextPanel> ();
		textPanel.textColor = panelTextColor;
		panelManager.addPane (textPanel);

		channelList.addNewChannel (channelName, textPanel.GetComponent<TextPanel>());
		tab.GetComponent<Tabs> ().initiallyTransparent = transparent;
	}
}