using UnityEngine;

public class SelectTurret : MonoBehaviour
{
    [SerializeField] private GameObject UpgradeUi; 

    [SerializeField] private OnClickEffect onClickEffect;

    public bool AllowSelectingTurret = true;

    void Update()
    {
        if (AllowSelectingTurret)
        {
            if (Input.GetMouseButtonDown(0))
            {
                onClickEffect.TurretSelected();
            }
        }
    }
}