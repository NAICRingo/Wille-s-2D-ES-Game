using UnrealBuildTool;
using System.Collections.Generic;

public class Willes2DESGameTarget : TargetRules
{
	public Willes2DESGameTarget(TargetInfo Target) : base(Target)
	{
		Type = TargetType.Game;
		DefaultBuildSettings = BuildSettingsVersion.V5;
		IncludeOrderVersion = EngineIncludeOrderVersion.Unreal5_6;
		ExtraModuleNames.Add("Willes2DESGame");
	}
}
