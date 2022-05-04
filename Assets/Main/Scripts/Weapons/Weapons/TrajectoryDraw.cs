using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryDraw : MonoBehaviour
{
    public int linePoints;
    public float distanceBetweenPoints = 0.1f;
    public float animationSpeed = 0.0f;
    
    [SerializeField] private Weapon weapon;
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform hitPosition;
    [SerializeField] private LayerMask physicsMask;

	private void Update()
	{
        line.positionCount = linePoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = weapon.bulletSpawnPoints[0].position;
        Vector3 startingVelocity = weapon.bulletSpawnPoints[0].forward * weapon.bulletVelocity;

        for(float i = 0.0f; i < linePoints; i += distanceBetweenPoints)
		{
            Vector3 currentPoint = startingPosition + i * startingVelocity;
            currentPoint.y = startingPosition.y + startingVelocity.y * i + Physics.gravity.y / 2.0f * i * i;
            points.Add(currentPoint);

            Vector3 nextPoint = startingPosition + (i + distanceBetweenPoints) * startingVelocity;
            nextPoint.y = startingPosition.y + startingVelocity.y * (i + distanceBetweenPoints) + Physics.gravity.y / 2.0f * (i + distanceBetweenPoints) * (i + distanceBetweenPoints);
            
            RaycastHit hit;
            if (Physics.Raycast(currentPoint, nextPoint - currentPoint, out hit, 0.8f, physicsMask))
			{
                Vector3 hitPoint = hit.point;
                line.positionCount = points.Count;
                points[points.Count - 1] = hitPoint;
                hitPosition.position = hitPoint + (hitPosition.forward * 0.01f);
                hitPosition.rotation = Quaternion.LookRotation(hit.normal);
                break;
			}
		}

        line.SetPositions(points.ToArray());
        line.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0.0f);
	}
}
