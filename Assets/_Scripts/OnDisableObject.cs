// Implement OnDisable and OnEnable script functions.
// These functions will be called when the attached GameObject
// is toggled.
// This example also supports the Editor.  The Update function
// will be called, for example, when the position of the
// GameObject is changed.

using FMODUnity;
using UnityEngine;

[ExecuteInEditMode]
public class OnDisableObject : MonoBehaviour
    {
    [SerializeField]
    private StudioEventEmitter m_StudioEventEmitter;
    void OnDisable()
        {
        //FMOD Studio event Play
        m_StudioEventEmitter.Play();
        Debug.Log("PrintOnDisable: script was disabled");
        }

    void OnEnable()
        {
        Debug.Log("PrintOnEnable: script was enabled");
        }

    void Update()
        {
#if UNITY_EDITOR
        Debug.Log("Editor causes this Update");
#endif
        }
    }
