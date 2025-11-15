using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class sub_manager : MonoBehaviour
{
	public sub main_sub = null;
	private string actor = null;
	[SerializeField] private TextMeshProUGUI labil, txt_show;
	[SerializeField] private GameObject panle_show_q, btn_show_q;
	private List<dialog> temp = new List<dialog>();
	private bool find_non_player = false;
	protected void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("non-player"))
		{
			txt_show.text = "";
			var Extra_tag = other.GetComponent<extra_tag>().value;
			main_sub = subManager.load_Sub($"sub/{Extra_tag}-en");
			temp = subManager.get_dialogs(Extra_tag, ref main_sub);
			actor = Extra_tag;
			if (Application.platform == RuntimePlatform.WindowsPlayer ||
				Application.platform == RuntimePlatform.LinuxPlayer
#if UNITY_EDITOR
				|| Application.platform == RuntimePlatform.LinuxEditor ||
				Application.platform == RuntimePlatform.WindowsEditor
#endif
				)
				labil.gameObject.SetActive(true);
			else if (Application.platform == RuntimePlatform.Android)
			{
				btn_show_q.SetActive(true);
			}
			find_non_player = true;
		}
	}
	protected void OnTriggerExit(Collider other)
	{
		if (other.transform.CompareTag("non-player"))
		{
			labil.gameObject.SetActive(false);
			find_non_player = false;
			btn_show_q.SetActive(false);
			panle_show_q.gameObject.SetActive(false);
			actor = null;
			txt_show.text = "";
			if (typeEffect != null)
			{
				StopCoroutine(typeEffect);
				typeEffect = null;
			}

			remove_all_object_in_panle();
		}
	}
	private IEnumerator type_effect(string txt)
	{
		foreach (var item in txt)
		{
			txt_show.text += item;
			yield return new WaitForSeconds(.01f);
		}
	}
	protected void Update()
	{
		if ((Input.GetKeyDown(KeyCode.F) || ZInput.GetKeyDown("f")) &&
			find_non_player == true && panle_show_q.gameObject.activeSelf == false)
		{
			panle_show_q.gameObject.SetActive(true);
			btn_show_q.SetActive(false);
			labil.gameObject.SetActive(false);
			foreach (var item in main_sub.structure.dialogs)
			{
				if (item.actor == actor)
				{
					foreach (var item_actor in item.dialog)
					{
						ScrollViewContent_Additem(item_actor);
					}
					break;
				}
			}
		}
	}

	[SerializeField] private GameObject Content;
	[SerializeField] private RectTransform transform_content;
	int y_transform_content = 20;
	int pos_transform_content = 0;

	private void remove_all_object_in_panle()
	{
		for (int i = 0; i < transform_content.childCount; i++)
		{
			Destroy(transform_content.GetChild(i).gameObject);
		}
		y_transform_content = 20;
		pos_transform_content = 0;
	}
	private Coroutine typeEffect = null;
	private void ScrollViewContent_Additem(dialog d)
	{
		transform_content.sizeDelta = new Vector2(transform_content.sizeDelta.x, y_transform_content);
		GameObject v = Instantiate(Content, new Vector2(0, 0), transform_content.transform.rotation, transform_content.transform);

		v.GetComponent<RectTransform>().anchoredPosition = (new Vector2(0, -pos_transform_content));
		v.GetComponent<RectTransform>().offsetMin = new Vector2(0, v.GetComponent<RectTransform>().offsetMin.y);
		v.GetComponent<RectTransform>().offsetMax = new Vector2(0, v.GetComponent<RectTransform>().offsetMax.y);
		v.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = d.question;
		v.GetComponent<info_dialogs>().info = d;
		v.GetComponent<Button>().onClick.AddListener(() =>
		{
			var temp = v.GetComponent<info_dialogs>();
			if (typeEffect != null)
			{
				txt_show.text = "";
				StopCoroutine(typeEffect);
				typeEffect = null;
			}
			typeEffect = StartCoroutine(type_effect(temp.info.answer));
		});

		y_transform_content += 20;
		pos_transform_content += 20;
	}
}
