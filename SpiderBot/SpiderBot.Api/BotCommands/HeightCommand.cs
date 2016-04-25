using System.Collections.Generic;

namespace SpiderBot.BotCommands
{
	public class HeightCommand : BotCommand
	{
		int amount;

		public HeightCommand()
		{
			Command = BotCommandsConstants.MoveBody;
			Parameters = new List<string> { "2", "0" };
		}

		public int Amount
		{
			get { return amount; }
			set
			{
				amount = value;
				Parameters[1] = Amount.ToString();
			}
		}
	}
}