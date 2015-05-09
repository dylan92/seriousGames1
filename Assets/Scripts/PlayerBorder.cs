using UnityEngine;
using System.Collections;

public class PlayerBorder : Singleton<PlayerBorder> {

	public Bounds playerBorder;
	
	
	public void OnDrawGizmosSelected() {
		Gizmos.DrawWireCube(playerBorder.center, playerBorder.extents);
	}
}
