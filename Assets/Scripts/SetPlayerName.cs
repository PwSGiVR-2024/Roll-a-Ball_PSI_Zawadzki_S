using UnityEngine;
using UnityEngine.UI;

public class SetPlayerName : MonoBehaviour
{
    public InputField nameInputField;

    public void SaveName()
    {
        if (PlayerNameController.Instance != null)
        {
            PlayerNameController.Instance.playerName = nameInputField.text;
        }
    }
}
