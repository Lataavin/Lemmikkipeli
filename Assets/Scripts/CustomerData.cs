using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerData.asset", menuName = "Animal/CustomerData")]
public class CustomerData : ScriptableObject
{
    [System.Serializable]
    public class CustomerDefinition : Definition<Sprite> { }

    [SerializeField]
    private List<CustomerDefinition> _customers = new List<CustomerDefinition>();
    [SerializeField]
    private Sprite _defaultCustomer;

    public void SetCustomerVisuals(SpriteRenderer renderer)
    {
        renderer.sprite = CustomerDefinition.GetDefinition(_customers);
        if (renderer.sprite == null)
        {
            renderer.sprite = _defaultCustomer;
        }
    }
}
