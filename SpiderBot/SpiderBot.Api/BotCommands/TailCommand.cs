using System.Collections.Generic;

namespace SpiderBot.BotCommands
{
	public class TailCommand : BotCommand
	{
		readonly Axis axis;
		float amount;

		public TailCommand(Axis axis)
		{
			this.axis = axis;

			Command = BotCommandsConstants.MoveTail;
			
			Parameters = new List<string>() { (axis == Axis.X ? "0" : "1"), "0" };
		}

		const int zero = 100;
		const int range = 70;

		/// <summary>
		///     Valid ranges -1 : 1
		/// </summary>
		public float Amount
		{
			get { return amount; }
			set
			{
				amount = value;
				Parameters[1] = ((Amount * range * (axis == Axis.X ? -1 : 1)) + zero).ToString();
			}
		}
	}
}