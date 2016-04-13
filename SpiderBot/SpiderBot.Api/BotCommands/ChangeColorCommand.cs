using System;
using System.Collections.Generic;
using System.Text;

namespace SpiderBot.BotCommands
{
    public class ChangeColorCommand : BotCommand
    {
	    public ChangeColorCommand()
	    {
		    Command = BotCommandsConstants.ChangeColor;
		    Parameters = new List<string>
		    {
			    "0",
				"0",
				"0",
				"0",
		    };
	    }

		/// <summary>
		/// 0-100 based value
		/// </summary>
	    public int Red
	    {
		    get {return FromParam(0);}
			set { Parameters[0] = value.ToString(); }
		}
		/// <summary>
		/// 0-100 based value
		/// </summary>
		public int Blue
		{
			get { return FromParam(1); }
			set { Parameters[1] = value.ToString(); }
		}
		/// <summary>
		/// 0-100 based value
		/// </summary>
		public int Green
		{
			get { return FromParam(2); }
			set { Parameters[2] = value.ToString(); }
		}

	    public int Delay
		{
			get { return FromParam(3); }
			set { Parameters[3] = value.ToString(); }
		}

    }
}
