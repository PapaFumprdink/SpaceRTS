using UnityEngine;

[SelectionBase]
[DisallowMultipleComponent]
public sealed class PlayerControlledEntity : MonoBehaviour
{
    [SerializeField] private PCEAction[] m_PerformableActions;

    private void OnValidate()
    {
        for (int i = 0; i < m_PerformableActions.Length; i++)
        {
            PCEAction action = m_PerformableActions[i];

            if (!action.IsEntityValid(this, out string errorMessage))
            {
                Debug.LogWarning($"{action.GetType()} is not valid on {name} because {errorMessage}", gameObject);
                m_PerformableActions[i] = null;
            }
        }
    }
}
