using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{

	public MenuPanel currentMenu;

	public void Start() {
		ShowMenu (currentMenu);
	}

	public void ShowMenu(MenuPanel toShowMenu) {
		if (currentMenu != null) {
			currentMenu.IsOpen = false;
		}

		currentMenu = toShowMenu;
		currentMenu.IsOpen = true;
	}
}

