using UnityEngine;
using UnityEngine.UI;

public class PlayerJoinUI : MonoBehaviour
{
    public GameObject P1Join;
    public GameObject P1Back;

    public GameObject P2Join;
    public GameObject P2Back;

    public GameObject P3Join;
    public GameObject P3Back;

    public GameObject P4Join;
    public GameObject P4Back;

    public Image[] ColorBackgrounds;

    private bool p1ready;
    private bool p2ready;
    private bool p3ready;
    private bool p4ready;

    private void Update()
    {
        if (PlayerInput.Instance.GetButtonUp(EController.Controller1, !p1ready ? EControllerButton.Button_A : EControllerButton.Button_B))
        {
            p1ready = !p1ready;
            P1Join.gameObject.SetActive(!p1ready);
            P1Back.gameObject.SetActive(p1ready);

            SetColor(EController.Controller1, p1ready);
        }
        else if (PlayerInput.Instance.GetButtonUp(EController.Controller2, !p2ready ? EControllerButton.Button_A : EControllerButton.Button_B))
        {
            p2ready = !p2ready;
            P2Join.gameObject.SetActive(!p2ready);
            P2Back.gameObject.SetActive(p2ready);

            SetColor(EController.Controller2, p2ready);
        }
        else if (PlayerInput.Instance.GetButtonUp(EController.Controller3, !p3ready ? EControllerButton.Button_A : EControllerButton.Button_B))
        {
            p3ready = !p3ready;
            P3Join.gameObject.SetActive(!p3ready);
            P3Back.gameObject.SetActive(p3ready);

            SetColor(EController.Controller3, p3ready);
        }
        else if (PlayerInput.Instance.GetButtonUp(EController.Controller4, !p4ready ? EControllerButton.Button_A : EControllerButton.Button_B))
        {
            p4ready = !p4ready;
            P4Join.gameObject.SetActive(!p4ready);
            P4Back.gameObject.SetActive(p4ready);

            SetColor(EController.Controller4, p4ready);
        }
    }

    private void SetColor(EController controller, bool player)
    {
        int index = (int)controller - 1;
        index = Mathf.Clamp(index, 0, 4);

        ColorBackgrounds[index].color = player ? PlayerManager.GetPlayerColor(controller) : Color.white;
    }
}

