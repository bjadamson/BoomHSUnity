using UnityEngine;

public class HitIndicator : MonoBehaviour
{
	private readonly float VISIBLE_TIME = 0.25f;

	void Start()
	{
		hideHitIndicator();
	}

	public void showThenHideHitIndicator()
	{
		gameObject.SetActive(true);
		Invoke("hideHitIndicator", VISIBLE_TIME);
	}

	private void hideHitIndicator()
	{
		gameObject.SetActive(false);
	}
}