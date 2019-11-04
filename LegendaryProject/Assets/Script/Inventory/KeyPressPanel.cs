using UnityEngine;

public class KeyPressPanel : MonoBehaviour
{
    [SerializeField] GameObject inventoryGameObject;
    [SerializeField] KeyCode[] toggleInventoryKeys;
    private bool IsOpened=false;
	void Update ()
    {

        for (int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]) && (GameObject.FindGameObjectWithTag("Player").GetComponent<movementplayer>().isGrounded || IsOpened))
            {
                inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);
                if (inventoryGameObject.activeSelf)
                {
                    ShowMouseCursor();
                    IsOpened = true;
                }
                else
                {
                    HideMouseCusor();
                    IsOpened = false;
                }
                break;
            }
        }
    }

    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void HideMouseCusor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public bool GetInventoryIsOpen()
    {
        return IsOpened;
    }
}
