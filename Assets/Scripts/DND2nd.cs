using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DND2nd : MonoBehaviour
{
    public enum DragMode
    {
        TransformPosition,
        MovePosition,
        Velocity,
        BoxCast
    }

    [Tooltip("Choose how to move the object to the cursor")]
    public DragMode mode;
    [Tooltip("Optional limit on how fast the object can follow")]
    public float maxSpeed = float.PositiveInfinity;
    [Tooltip("Select which layers should block the boxcast drag mode")]
    public LayerMask obstacleLayers;

    Rigidbody2D _body;
    BoxCollider2D _collider;

    delegate YieldInstruction dragMethod(Vector2 destination);

    // Start a drag using the selected method when clicked.
    void OnMouseDown()
    {
        dragMethod method = null;
        switch (mode)
        {
            case DragMode.TransformPosition:
                method = TransformPosition;
                break;
            case DragMode.MovePosition:
                method = MovePosition;
                break;
            case DragMode.Velocity:
                method = Velocity;
                break;
            case DragMode.BoxCast:
                method = BoxCast;
                break;
        }
        // Start a function that will run each frame/physics step
        // to update our dragged position until the button is released.
        StartCoroutine(Drag(method));
    }

    // Update the dragged position as long as the mouse button is held.
    IEnumerator Drag(dragMethod dragTo)
    {
        // Turn off our gravity while we're being dragged.
        float cachedGravityScale = _body.gravityScale;
        _body.gravityScale = 0f;

        // Stash our current offset from the cursor, 
        // so we can preserve it through the move.
        var offset = transform.InverseTransformPoint(ComputeCursorPosition());

        while (Input.GetMouseButton(0))
        {
            // Keep the object from accumulating velocity while dragging.
            _body.velocity = Vector2.zero;
            _body.angularVelocity = 0f;

            // Calculate desired drag position.
            var cursor = ComputeCursorPosition();
            var destination = cursor - transform.TransformVector(offset);

            var travel = Vector2.ClampMagnitude(
                destination - transform.position,
                maxSpeed * Time.deltaTime);

            // Let our chosen drag method choose how to get us there.
            yield return dragTo(_body.position + travel);
        }

        // Re-enable gravity as before.
        _body.gravityScale = cachedGravityScale;
    }

    // Using this method, the object will teleport through obstacles.
    YieldInstruction TransformPosition(Vector2 destination)
    {
        transform.position = destination;
        return null;
    }

    // Using this method, the object will stop at obstacles,
    // though it may penetrate for a frame before rebounding.
    YieldInstruction MovePosition(Vector2 destination)
    {
        _body.MovePosition(destination);
        return null;
    }

    // Effectively the same results as MovePosition.
    YieldInstruction Velocity(Vector2 destination)
    {
        var velocity = (destination - _body.position) / Time.deltaTime;
        _body.velocity = velocity;
        return new WaitForFixedUpdate();
    }

    // Using this method, the object will stop at the border of the obstacle.
    // It can "stick" to surfaces when dragged into them, because it keeps colliding.
    // Pull the cursor parallel or away from the surface to unstick it.
    YieldInstruction BoxCast(Vector2 destination)
    {
        // Compute the direction & distance to scan ahead.
        var travel = destination - _body.position;
        var distance = travel.magnitude;

        // Skip the query if we're not going anywhere.
        if (Mathf.Approximately(distance, 0f))
            return null;


        // Find the center of our box.
        Vector2 origin = transform.TransformPoint(_collider.offset);

        // Check for any obstacles that our collider would clip on the way.
        var hit = Physics2D.BoxCast(
            origin,
            _collider.size,
            _body.rotation,
            travel,
            distance,
            obstacleLayers);

        // If we hit something, stop just a hair before the collision.
        if (hit.collider)
        {
            var direction = travel / distance;
            distance = hit.distance - Physics2D.defaultContactOffset * 2f;
            destination = _body.position + direction * distance;
        }

        // Now it's safe to use any of our other methods without penetrating/tunneling,
        // since we took responsibility for avoiding collisions ourselves.
        transform.position = destination;
        return null;
    }

    // Initialize component dependencies.
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    // Utility functions to compute dragged position.
    float GetDepthOffset(Transform relativeTo)
    {
        Vector3 offset = transform.position - relativeTo.position;
        return Vector3.Dot(offset, relativeTo.forward);
    }

    Vector3 ComputeCursorPosition()
    {
        var camera = Camera.main;
        var screenPosition = Input.mousePosition;
        screenPosition.z = GetDepthOffset(camera.transform);
        var worldPosition = camera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
