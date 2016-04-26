using System;
using System.Collections.Generic;
using System.Text;

namespace SpiderBot
{
    public static class BotCommandsConstants
    {
	    public const string WalkY = "walkY";
	    public const string WalkX = "walkTurnZ";
	    public const string Turn = "walkTurnZ";
	    public const string HeartBeat = "heartbeat";
	    public const string ChangeColor = "changeLEDColor";
	    public const string MoveBody = "joystickTranslateBodyTo";
	    public const string RotateBody = "joystickRotateBodyTo";
		public const string MoveTail = "joystickMoveTail";
	    public const string StartSequence = "builtInSequences";
	    public const string StopSequence = "stopBuiltInSequence";
    }
}
