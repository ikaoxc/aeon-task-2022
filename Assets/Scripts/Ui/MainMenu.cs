using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;

    public void Show()
    {
        _mainMenu.SetActive(true);
    }

    public void Hide()
    {
        _mainMenu.SetActive(false);
    }
}
