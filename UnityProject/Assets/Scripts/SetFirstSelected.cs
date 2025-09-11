using UnityEngine;
using UnityEngine.EventSystems;

namespace ToyGame
{
    public class SetFirstSelected : MonoBehaviour
    {
        private void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
}
