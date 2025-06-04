using UnityEngine;

public class SelectTurret : MonoBehaviour
{
    [SerializeField] private GameObject UpgradeUi; 

    [SerializeField] private OnClickEffect onClickEffect;

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            onClickEffect.TurretSelected();
        }

        
        if (Input.GetMouseButtonDown(0) && (Input.GetKey(KeyCode.LeftControl)))
        {
            onClickEffect.TurretSelected();
        }
    }
}