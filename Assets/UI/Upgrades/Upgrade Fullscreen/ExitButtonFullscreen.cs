using UnityEngine;

public class ExitButtonFullscreen : MonoBehaviour
{
    [SerializeField] private GameObject UiFullscreen;

    public void ExitButton()
    {
        print("Exit Button");
        UiFullscreen.SetActive(false);
    }
}
