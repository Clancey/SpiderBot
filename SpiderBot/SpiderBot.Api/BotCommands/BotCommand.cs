using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderBot
{
    public class BotCommand
    {
		public string Command { get; set; }

		public List<string> Parameters { get; set; } = new List<string>();

	    public override string ToString()
	    {
		    return Parameters.Any() ? $"{Command} {string.Join(" ", Parameters)};" : $"{Command};";
	    }

		public int FromParam(int index)
		{
			int value;
			int.TryParse(Parameters[index], out value);
			return value;
		}

		public int CommandDurration { get; set; }
	}
}
