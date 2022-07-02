using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BulletItemGUI : MonoBehaviour
{
    public void SetData(Sprite bulletSprite)
    {
        GetComponent<Image>().sprite = bulletSprite;
    }
}
