using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{

	public static Vector3? LinePlaneIntersect(Vector3 planePosition, Vector3 planeDirection, Vector3 linePosition, Vector3 lineDirection)
	{
		//A plane can be defined as:
		//a point representing how far the plane is from the world origin
		Vector3 p_0 = planePosition;
		//a normal (defining the orientation of the plane), should be negative if we are firing the ray from above
		Vector3 n = -planeDirection;
		//We are intrerested in calculating a point in this plane called p
		//The vector between p and p0 and the normal is always perpendicular: (p - p_0) . n = 0

		//A ray to point p can be defined as: l_0 + l * t = p, where:
		//the origin of the ray
		Vector3 l_0 = linePosition;
		//l is the direction of the ray
		Vector3 l = lineDirection;
		//t is the length of the ray, which we can get by combining the above equations:
		//t = ((p_0 - l_0) . n) / (l . n)

		//But there's a chance that the line doesn't intersect with the plane, and we can check this by first
		//calculating the denominator and see if it's not small. 
		//We are also checking that the denominator is positive or we are looking in the opposite direction
		float denominator = Vector3.Dot(l, n);

		if (denominator > 0.00001f)
		{
			//The distance to the plane
			float t = Vector3.Dot(p_0 - l_0, n) / denominator;

			//Where the ray intersects with a plane
			Vector3 p = l_0 + l * t;

			return p;
		}
		else
		{
			return null;
		}
	}
}