using UnityEngine;
using MoreMountains.Tools;

namespace MRF.Containers
{
    public class BaseContainerController : MonoBehaviour
    {
        // Editor
        [SerializeField]
        protected ContainerValues m_containerData;
        [SerializeField]
        protected MMHealthBar m_healthBar;
        // Debug
        [SerializeField]
        protected float m_currentValue;

        // Data
        protected Container m_container;
        public Container Container { get { return m_container; } }
        
        protected virtual void Awake()
        {
            m_container = new Container(m_containerData);
            m_container.OnValueChangedCallback += OnValueChanged;
            m_container.OnValueChangedInUpdateCallback += OnValueChangedInUpdate;
            m_container.OnEmptyCallback += OnContainerEmpty;
            m_container.OnFullCallback += OnContainerFull;
        }

        protected virtual void OnDestroy()
        {
            m_container.OnValueChangedCallback -= OnValueChanged;
            m_container.OnValueChangedInUpdateCallback -= OnValueChangedInUpdate;
            m_container.OnEmptyCallback -= OnContainerEmpty;
            m_container.OnFullCallback -= OnContainerFull;
        }

        protected void Start()
        {
            OnValueChanged();
        }

        protected virtual void Update()
        {
            m_container.OnUpdate();
        }

        protected virtual void OnValueChanged()
        {
            // to show current value in inspector
            m_currentValue = GetCurrentValue();
            m_healthBar?.UpdateBar(m_container.GetNormalizedPercentage(), 0, 1, true);
        }

        protected virtual void OnValueChangedInUpdate()
        {
            // to show current value in inspector
            m_currentValue = GetCurrentValue();
            m_healthBar?.UpdateBar(m_container.GetNormalizedPercentage(), 0, 1, true);
        }

        public virtual void ForceUpdate()
        {
            OnValueChanged();
        }

        public virtual void Reset()
        {
            m_container.Reset();
        }

        protected virtual void OnContainerEmpty()
        { 
        
        }

        protected virtual void OnContainerFull()
        { 
        
        }

        public void InitializeContainerValues(float min, float max, float current)
        {
            m_container.SetMinValue(min);
            m_container.SetValue(current);
            m_container.SetMaxValue(max);
        }

        public float GetMaxValue()
        {
            return m_container.GetMaxValue();
        }

        public void SetMaxValue(float value)
        {
            m_container.SetMaxValue(value);
        }

        public void SetMinValue(float value)
        {
            m_container.SetMinValue(value);
        }

        public void AddValue(float value)
        {
            m_container.AddValue(value);
        }

        public void AddValueToMax()
        {
            m_container.SetValue(m_containerData.BaseMaxValue);
        }

        public float GetCurrentValue()
        {
            return m_container.CurrentValue;
        }

        public void SetRegenerationValue(float value)
        {
            m_container.SetRegenerationValue(value);
        }

        public void SetCanRegenerate(bool canRegenerate)
        {
            m_container.SetCanRegenerate(canRegenerate);
        }

        public bool GetCanRegenerate()
        {
            return m_container.GetCanRegenerate();
        }

        public bool IsEmpty()
        {
            return m_container.IsEmpty();
        }

        public bool IsFull()
        {
            return m_container.IsFull();
        }

        public float GetPercentage()
        {
            return m_container.GetPercentage();
        }
    }
}