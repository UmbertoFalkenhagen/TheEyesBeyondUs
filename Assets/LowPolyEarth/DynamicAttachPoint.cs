using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DynamicAttachPoint : MonoBehaviour
{
    public Transform snapbackpoint;
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(SetDynamicAttach);
    }

    private void SetDynamicAttach(SelectEnterEventArgs arg)
    {
        if (arg.interactor is XRDirectInteractor)
        {
            Vector3 attachPosition = arg.interactor.transform.position;
            // Optionally, perform a raycast or similar to get the exact hit point on the sphere

            // Create a new GameObject as a dynamic attach point
            GameObject attachPoint = new GameObject("Dynamic Attach Point");
            attachPoint.transform.SetParent(transform, false);
            attachPoint.transform.position = attachPosition;

            // Set the attach transform of the interactable
            grabInteractable.attachTransform = attachPoint.transform;
        }
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(SetDynamicAttach);
    }

    public void SnapBackToTable()
    {
        transform.position = snapbackpoint.position;
        rb.velocity = Vector3.zero;
    }
}
