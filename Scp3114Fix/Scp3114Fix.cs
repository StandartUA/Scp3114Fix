using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabApi.Loader.Features.Plugins;

namespace Scp3114Fix
{
	public class Scp3114Fix : Plugin
	{
		public override string Name => "Scp3114 Fix";

		public override string Description => "Fixes SCP-3114's behavior to be more consistent with SCP-3114's lore.";

		public override string Author => "TiBarification";

		public override Version Version => new Version(1, 0, 0, 0);

		public override Version RequiredApiVersion => LabApi.Features.LabApiProperties.CurrentVersion;

		private EventHandlers eventHandlers;

		public override void Disable()
		{
			LabApi.Events.CustomHandlers.CustomHandlersManager.UnregisterEventsHandler(eventHandlers);
			eventHandlers = null;
		}

		public override void Enable()
		{
			eventHandlers = new EventHandlers();
			LabApi.Events.CustomHandlers.CustomHandlersManager.RegisterEventsHandler(eventHandlers);
		}
	}
}
