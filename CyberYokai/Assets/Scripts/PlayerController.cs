using UnityEngine;

public class PlayerController : MonoBehaviour
{
	RaycastHit hitInfo;

	PlayableEntity selectedCharacter;
	PlayableEntity SelectedCharacter
	{
		/// Just a classic Property.
		/// Instead of just assigning the value to the variable, we're gonna do some
		/// more stuff before or after it's assigned (check down)
		get
		{
			return selectedCharacter;
		}
		set
		{
			/// After selecting the character, we setup a hud for him
			selectedCharacter = value;

			/// Check if character is selected or deselected
			if (value != null)
			{
				SetupCombatHud();
			}
			else
			{
				CloseCombatHud();
			}
		}
	}

	public void Update()
	{
		/// Create a ray and if it hits something, put that value in hitInfo var.
		/// We're not checking here by layer, rather by component that the object has.
		/// i.e - if you want enemy, check if the object has EnemyBehaviour script rather than 'Enemy' layer.
		/// We still have to have a 'Entity' layer on anything we want to be interactable, we
		/// just differentiate them by checking on components
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hitInfo, 1000f, LayerMask.GetMask("Entity")))
		{
			/// Check if the object is behaviour entity (character, enemy, minion, etc.)
			if (hitInfo.collider.GetComponent<EntityBehaviour>() != null)
			{
				/// Check if it's playable entity in order to set it up for ui and other stuff
				if (hitInfo.collider.GetComponent<PlayableEntity>() != null)
				{
					if (Input.GetMouseButtonDown(0))
					{
						SelectedCharacter = hitInfo.collider.GetComponent<PlayableEntity>();
					}
				}
			}
		}

		if (selectedCharacter != null)
		{
			if (Input.GetMouseButtonDown(1))
			{
				selectedCharacter = null;
			}
		}
	}

	public void SetupCombatHud()
	{
		Debug.Log("open ui");
	}
	public void CloseCombatHud()
	{
		Debug.Log("Hud Closed");
	}
}
