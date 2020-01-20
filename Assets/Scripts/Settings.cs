using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Sprite vibroOn;
    [SerializeField] Sprite vibroOff;
    [SerializeField] Image vibroImg;
    [Space]
    [SerializeField] GameObject bckrBtn;
    [SerializeField] GameObject btnsPanel;

    public void SwitchVisibility()
    {
        vibroImg.sprite = SaveLoad.savedGame.vibroEnabled? vibroOn : vibroOff;
        bckrBtn.SetActive(!bckrBtn.activeSelf);
        btnsPanel.SetActive(!btnsPanel.activeSelf);
    }
    
    public void SwitchVibro()
    {
        SaveLoad.savedGame.vibroEnabled = !SaveLoad.savedGame.vibroEnabled;
        vibroImg.sprite = SaveLoad.savedGame.vibroEnabled? vibroOn : vibroOff;
        SaveLoad.Save();
    }

    public void ToPrivacy()
    {
        Application.OpenURL("https://text-host.ru/raw/L7NNoHjvRl");
    }
}
