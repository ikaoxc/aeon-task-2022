using UnityEngine;

[RequireComponent(typeof(Transform))]
public class CharacterPositionSaver : MonoBehaviour
{
    private Transform _cachedTransform;
    private Vector3 _savedPosition;

    private void Awake()
    {
        _cachedTransform = transform;
    }

    public void Save()
    {
        _savedPosition = _cachedTransform.position;
    }
    
    public void Reset()
    {
        _cachedTransform.position = _savedPosition;
    }
}
