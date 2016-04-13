using System.Collections.Generic;

namespace SpiderBot.BotCommands
{
	public enum Axis
	{
		X,
		Y
	}

	public class WalkCommand : BotCommand
	{
		readonly Axis axis;
		float amount;

		public WalkCommand(Axis axis)
		{
			this.axis = axis;
			Command = axis == Axis.X
				? BotCommandsConstants.WalkX
				: BotCommandsConstants.WalkY;
			Parameters = new List<string>(){ "zero" };
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
				Parameters[0] = ((Amount*range * (axis == Axis.X ? -1 : 1)) + zero).ToString();
			}
		}
	}
}