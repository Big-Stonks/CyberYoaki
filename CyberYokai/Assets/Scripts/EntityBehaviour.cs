using UnityEngine;

/// <summary>
/// This represents any entity usable by a player.
/// Heroes, minions, and anything that player can 'use' and control.
/// </summary>
public interface PlayableEntity
{

}

/// <summary>
///  This is a basic class to handle any sort of entity that's gonna have any sort of behaviour
///  It could be a hero, enemy, minion, even random dude on the side that yells every 5 turns
/// </summary>
public class EntityBehaviour : MonoBehaviour
{
	public EntitySpot spot;
}
