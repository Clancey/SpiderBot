using System;
using System.Collections.Generic;
using System.Text;

namespace SpiderBot.BotCommands
{
	public enum Sequences
	{
		Wave = 0,
		Wiggle = 1,
		Worm = 2,
		Dougie = 3,
		Salsa = 4
	}
    public class SequenceCommand : BotCommand
    {
	    public SequenceCommand()
	    {
		    Command = BotCommandsConstants.StartSequence;
			Parameters = new List<string> {"0"};
		    CommandDurration = 3000;
	    }

	    public Sequences Sequence
	    {
		    get { return (Sequences)FromParam(0); }
		    set
		    {
			    Parameters[0] = ((int) value).ToString();
				SetDurration(value);
		    }
	    }

	    void SetDurration(Sequences sequence)
	    {
		    CommandDurration = 4000;
		    switch (sequence)
		    {
				case Sequences.Wave:
				    CommandDurration = 5000;
				    return;
				case Sequences.Wiggle:
				    CommandDurration = 3500;
				    return;
				case Sequences.Worm:
				case Sequences.Dougie:
				case Sequences.Salsa:
					CommandDurration = 10000;
					return;
		    }
	    }
    }
}
