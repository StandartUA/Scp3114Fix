using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items.Keycards;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;

namespace Scp3114Fix
{
	public class EventHandlers: CustomEventsHandler
	{
		public override void OnPlayerInteractingDoor(PlayerInteractingDoorEventArgs ev)
		{
			if (ev.Player.Role == PlayerRoles.RoleTypeId.Scp3114)
			{
				// If user has bypass we not gonna check any permission
				if (ev.Player.IsBypassEnabled)
					return;
				if (ev.Player.CurrentItem?.Category == ItemCategory.Keycard)
				{
					DoorPermissionFlags flags = DoorPermissionFlags.None;
					// get SCP related permissions.
					if (ev.Player.RoleBase is IDoorPermissionProvider doorPermissionProvider)
						flags |= doorPermissionProvider.GetPermissions(ev.Door.Base);

					// getting all keycard permission. (If user has 05 and scientitst for example we use both)
					if (ev.Player.CurrentItem?.Base is InventorySystem.Items.Keycards.KeycardItem keycardItem)
						flags |= keycardItem.GetPermissions(ev.Door.Base);

					// checks the permission for the combined one
					ev.CanOpen = ev.Door.Base.PermissionsPolicy.CheckPermissions(flags);
					//Logger.Debug($"Can access door: CanOpen={ev.CanOpen} IsAllowed={ev.IsAllowed}");
				}
			}
		}
	}
}
