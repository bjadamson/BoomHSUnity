using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatFactory : MonoBehaviour {
	[SerializeField] private GameObject tabAnchor;
	[SerializeField] private GameObject contentAnchor;

	[SerializeField] private MouseoverManager mouseOverManager;
	[SerializeField] private TabManager tabManager;
	[SerializeField] private TextPanelManager panelManager;

	private ChatChannelList channelList = new ChatChannelList();
	private int id = 0; // Skip 0, that's built in for the "general" tab.

	void Start() {
		createGeneralChatPanel ();
		createWhisperChatPanel ();
		createPartyChatPanel ();
		createGuildChatPanel ();
		createCombatLogChatPanel ();

		panelManager.showGeneral ();
		tabManager.selectGeneralTab ();
	}

	private void createGeneralChatPanel() {
		string name = "General";
		Color color = new Color (0.6f, 0.502f, 0.38f);
		GameObject tab = addNewTab (name, color);
		GameObject textPanel = addNewPanel (name, Color.white);

		tab.GetComponent<Tabs> ().initiallyTransparent = false;
		textPanel.GetComponent<TextPanel> ().initialText = "Server version 0. Welcome. Try not to die, k?";
	}

	private void createWhisperChatPanel() {
		string name = "Whisper";
		Color color = new Color (0.588f, 0.031f, 0.722f);
		GameObject tab = addNewTab (name, color);
		GameObject textPanel = addNewPanel (name, color);
	}

	private void createPartyChatPanel() {
		string name = "Group";
		Color color = new Color (0.388f, 0.796f, 1.0f);
		GameObject tab = addNewTab (name, color);
		GameObject textPanel = addNewPanel (name, color);
	}

	private void createGuildChatPanel() {
		string name = "Guild";
		Color color = new Color (0.271f, 1.0f, 0.486f);
		GameObject tab = addNewTab (name, color);
		GameObject textPanel = addNewPanel (name, color);
	}

	private void createCombatLogChatPanel() {
		string name = "Combat Log";
		Color color = new Color (1.0f, 0.337f, 0.337f);
		GameObject tab = addNewTab (name, color);
		GameObject textPanel = addNewPanel (name, color);
	}

	private GameObject addNewTab(string windowName, Color bgColor) {
		GameObject newTab = new GameObject (windowName);
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
		image.color = bgColor;
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
		text.text = windowName;
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
		tab.manager = mouseOverManager;
		tab.panelId = id++;
		tabManager.addTab (tab);
		return newTab;
	}

	private GameObject addNewPanel(string windowName, Color fontColor) {
		GameObject newPanel = new GameObject (windowName + "Content");
		newPanel.transform.SetParent (contentAnchor.transform);
		RectTransform rectTransform = newPanel.AddComponent<RectTransform> ();
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
		textPanel.textColor = fontColor;
		panelManager.addPane (textPanel);
		return newPanel;
	}

	public void addNewChatPanel(string windowName) {
		// TODO: make sure there are no spaces in the the window name, make sure it's <= 16 chars or something.

		//, string channelName) {//, Color bgColor) {
		//if (channelList.containsWindow (windowName)) {
			// TODO: Somehow tell the user that window already exists, and do not allow the user to create the new window.
		//}
		//channelList.addIfNotAlreadyPresent (channelName);
	}

	public void removeChatChannel(string windowName) {
	}

	public void movePosition(string windowName, int position) {
	}
}