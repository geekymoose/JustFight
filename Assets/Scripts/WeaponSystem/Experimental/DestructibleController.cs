using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace WeaponSystem
{
    public class DestructibleController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Type of destructible object")]
        private DestructibleData destructibleData;

        [SerializeField]
        [Tooltip("Event raised whenever the destructible object takes damage")]
        private UnityEvent<float> takeDamageEvent;

        private void Awake()
        {
            Assert.IsNotNull(this.destructibleData, "Missing asset");
        }

        public void TakeDamage(float amount)
        {
            Debug.Log(amount);
            this.takeDamageEvent.Invoke(amount);
        }

        public DestructibleData GetDestructibleData()
        {
            return this.destructibleData;
        }
    }
}
