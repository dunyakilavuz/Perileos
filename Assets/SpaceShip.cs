using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceShip
{
	public List<ShipPart> shipParts = new List<ShipPart> ();
	public int[] partIndex;
	public List<int> myAttachPoint = new List<int> ();
	public List<int> targetAttachPoint = new List<int>();
	public string shipName;

}
