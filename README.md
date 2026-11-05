# Willes2DESGame

Willes2DESGame 是一个面向 Windows PC 的 Unreal Engine C++ 项目。仓库当前处于 M0“仓库与工程基线”阶段，目标是建立可生成、可编译和可启动的最小工程；尚无正式玩法或美术内容。当前验证状态见下文。

## 环境

- Unreal Engine 5.6.1，默认安装位置为 `C:\Program Files\Epic Games\UE_5.6`
- Windows 目标平台
- Visual Studio 2022 C++ Build Tools
- .NET Framework SDK 4.6 或更高版本（UE 5.6.1 的 Editor Target 通过 `SwarmInterface` 需要）
- Git LFS

## 本地打开

完成 Editor Target 构建后，在 PowerShell 中从仓库根目录执行：

```powershell
$Project = (Resolve-Path .\Willes2DESGame.uproject).Path
& 'C:\Program Files\Epic Games\UE_5.6\Engine\Binaries\Win64\UnrealEditor.exe' $Project
```

也可以在已正确关联 Unreal Engine 5.6 的 Windows 环境中打开 `Willes2DESGame.uproject`。

## 生成工程文件

本机安装版不包含源码构建使用的 `GenerateProjectFiles.bat`，因此直接调用 Unreal Build Tool：

```powershell
$Project = (Resolve-Path .\Willes2DESGame.uproject).Path
$UBT = 'C:\Program Files\Epic Games\UE_5.6\Engine\Binaries\DotNET\UnrealBuildTool\UnrealBuildTool.exe'
& $UBT -projectfiles "-project=$Project" -game -engine -progress
```

## 编译 Development Editor

```powershell
$Project = (Resolve-Path .\Willes2DESGame.uproject).Path
& 'C:\Program Files\Epic Games\UE_5.6\Engine\Build\BatchFiles\Build.bat' `
    Willes2DESGameEditor Win64 Development "-Project=$Project" -WaitMutex -NoHotReloadFromIDE
```

## 当前验证状态

截至 2026-11-06，本机已验证：

- UBT 工程文件生成成功；
- `Willes2DESGame Win64 Development` 构建成功；
- `Willes2DESGameEditor Win64 Development` 已执行但被本机缺失 .NET Framework SDK 阻塞。UBT 在创建 `SwarmInterface` 模块规则时退出，尚未形成可供 Editor 加载的项目模块。
- `UnrealEditor-Cmd` 已确认运行 UE 5.6.1 并进入项目加载流程，但因上述 Editor 模块尚未生成而以退出码 1 结束；这不计为通过的启动冒烟。

因此下面的 Editor 冒烟命令尚未在当前基线上通过；应先补齐上述 SDK，再重新执行 Editor 构建与冒烟验证。

## 无人值守启动冒烟

```powershell
$Project = (Resolve-Path .\Willes2DESGame.uproject).Path
& 'C:\Program Files\Epic Games\UE_5.6\Engine\Binaries\Win64\UnrealEditor-Cmd.exe' `
    $Project -Unattended -NullRHI -NoSound -NoSplash '-ExecCmds=Quit' -stdout -FullStdOutLogOutput
```

## 仓库卫生

`Binaries/`、`DerivedDataCache/`、`Intermediate/`、`Saved/` 和本机 IDE 状态均为可再生成的派生内容，不应提交。Unreal 的 `*.uasset` 与 `*.umap` 二进制资产由 Git LFS 管理。
