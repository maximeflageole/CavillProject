using System;
using UnityEngine;

namespace MRF.Containers
{
    [Serializable]
    public struct ContainerValues
    {
        public float BaseMinValue;
        public float BaseMaxValue;
        public float DefaultValue;
        public float RegenerationValue;
        public bool CanRegenerate;
        public float RegenerationCooldownAfterDamage;
    };

    [Serializable]
    public class Container
    {
        private ContainerValues m_containerValues;
        [field:SerializeField]
        public float CurrentValue { get; private set; }
        public float CurrentCooldown { get; private set; } = 0;

        public event Action OnValueChangedCallback;
        public event Action OnValueChangedInUpdateCallback;
        public event Action OnEmptyCallback;
        public event Action OnFullCallback;

        private bool m_isFirstEmpty;
        private bool m_isFirstFull;

        public Container(float maxHealth, bool regen = false, float regenValue = 0, float regenCooldown = 0)
        {
            m_containerValues = new ContainerValues();
            m_containerValues.BaseMaxValue = maxHealth;
            m_containerValues.DefaultValue = maxHealth;
            m_containerValues.BaseMinValue = 0;
            m_containerValues.CanRegenerate = regen;
            m_containerValues.RegenerationValue = regenValue;
            m_containerValues.RegenerationCooldownAfterDamage = regenCooldown;

            Init();
        }

        public Container(ContainerValues data)
        {
            m_containerValues = data;
            Init();
        }

        protected void Init()
        {
            CurrentValue = Mathf.Clamp(m_containerValues.DefaultValue, m_containerValues.BaseMinValue, m_containerValues.BaseMaxValue);

            m_isFirstEmpty = true;
            m_isFirstFull = true;
            CurrentCooldown = 0;

            OnValueChangedCallback?.Invoke();
        }

        public void Reset()
        {
            CurrentValue = Mathf.Clamp(m_containerValues.DefaultValue, m_containerValues.BaseMinValue, m_containerValues.BaseMaxValue);
            m_isFirstEmpty = true;
            m_isFirstFull = true;
            CurrentCooldown = 0;

            OnValueChangedCallback?.Invoke();
        }

        public void OnUpdate()
        {
            TryUpdateRegeneration();
            CurrentCooldown -= Time.deltaTime;
        }

        private void TryUpdateRegeneration()
        {
            if (CanRegenerate())
            {
                UpdateRegeneration();
            }
        }

        private bool CanRegenerate()
        {
            return m_containerValues.CanRegenerate && IsInContainer() && CurrentCooldown <= 0;
        }

        private bool IsInContainer()
        {
            return CurrentValue > m_containerValues.BaseMinValue  && CurrentValue < m_containerValues.BaseMaxValue;
        }

        private void UpdateRegeneration()
        {
            CurrentValue = Mathf.Clamp(CurrentValue + (m_containerValues.RegenerationValue * Time.deltaTime), m_containerValues.BaseMinValue, m_containerValues.BaseMaxValue);

            OnValueChangedInUpdateCallback?.Invoke();
        }

        public void AddValue(float delta)
        {
            CurrentValue = Mathf.Clamp(CurrentValue + delta, m_containerValues.BaseMinValue, m_containerValues.BaseMaxValue);
            OnValueChangedCallback?.Invoke();

            if (IsFull() && m_isFirstFull)
            {
                OnFullCallback?.Invoke();
                m_isFirstFull = false;
            }
        }

        public void RemoveValue(float delta, bool affectsCooldown = true)
        {
            CurrentValue = Mathf.Clamp(CurrentValue - delta, m_containerValues.BaseMinValue, m_containerValues.BaseMaxValue);

            if (affectsCooldown)
            {
                CurrentCooldown = m_containerValues.RegenerationCooldownAfterDamage;
            }

            OnValueChangedCallback?.Invoke();

            if (IsEmpty() && m_isFirstEmpty)
            {
                OnEmptyCallback?.Invoke();
                m_isFirstEmpty = false;
            }
        }

        public void SetValue(float value)
        {
            if (m_containerValues.BaseMinValue > value  || value > m_containerValues.BaseMaxValue )
                return;

            CurrentValue = Mathf.Clamp(value, m_containerValues.BaseMinValue, m_containerValues.BaseMaxValue);
            OnValueChangedCallback?.Invoke();
        }

        public float GetMaxValue()
        {
            return m_containerValues.BaseMaxValue;
        }

        public void SetMaxValue(float newMaxValue)
        {
            m_containerValues.BaseMaxValue = newMaxValue;
        }

        public void SetMinValue(float newMinValue = 0f)
        {
            m_containerValues.BaseMinValue = newMinValue;
        }

        public ContainerValues GetContainerData()
        {
            return m_containerValues;
        }

        public bool IsEmpty()
        {
            return CurrentValue <= m_containerValues.BaseMinValue;
        }

        public bool IsFull()
        {
            return CurrentValue >= m_containerValues.BaseMaxValue;
        }

        // Percentage clamped between 0 and 1
        public float GetNormalizedPercentage()
        {
            return (CurrentValue - m_containerValues.BaseMinValue) / (m_containerValues.BaseMaxValue - m_containerValues.BaseMinValue);
        }

        // Percentage between 0 and 100
        public float GetPercentage()
        {
            return GetNormalizedPercentage() * 100f;
        }

        public void SetRegenerationValue(float value)
        {
            m_containerValues.RegenerationValue = value;
        }

        public void SetCanRegenerate(bool canRegenerate)
        {
            m_containerValues.CanRegenerate = canRegenerate;
        }

        public bool GetCanRegenerate()
        {
            return m_containerValues.CanRegenerate;
        }
    }
}