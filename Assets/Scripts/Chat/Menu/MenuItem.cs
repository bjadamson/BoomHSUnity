using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface MenuItem {
	void hideHighlight ();
	void showHighlight ();

	void hideTarget();
	void showTarget();
}