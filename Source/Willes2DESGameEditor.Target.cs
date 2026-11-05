using UnrealBuildTool;
using System.Collections.Generic;

public class Willes2DESGameEditorTarget : TargetRules
{
	public Willes2DESGameEditorTarget(TargetInfo Target) : base(Target)
	{
		Type = TargetType.Editor;
		DefaultBuildSettings = BuildSettingsVersion.V5;
		IncludeOrderVersion = EngineIncludeOrderVersion.Unreal5_6;
		ExtraModuleNames.Add("Willes2DESGame");
	}
}
