# Willes2DESGame

Willes2DESGame 是一个面向 Windows PC 的 Unreal Engine C++ 项目。M0“仓库与工程基线”已在本机完成验证，项目可以生成工程文件、编译 Editor Target、启动 Unreal Editor 并正常退出；尚无正式玩法或美术内容，也尚未开始 M1。

## 环境

- Unreal Engine 5.6.1，默认安装位置为 `C:\Program Files\Epic Games\UE_5.6`
- Windows 目标平台
- Visual Studio 2022 C++ Build Tools 17.14.44
- .NET Framework 4.8 SDK（`Microsoft.Net.Component.4.8.SDK`）
- .NET Framework 4.8 Targeting Pack（`Microsoft.Net.Component.4.8.TargetingPack`）
- Git LFS

## 本地打开

从 Epic Games Launcher 启动 Unreal Engine 5.6.1 后，在 Unreal Project Browser 的最近项目中选择 `Willes2DESGame`。如果项目尚未显示，使用 **Browse/浏览** 选择现有文件：

```text
D:\github\Willes2DESGame\Willes2DESGame.uproject
```

不要使用“新建项目”。首次成功打开后，UE 5.6 会将该文件写入最近项目记录，项目仍保留在 `D:\github\Willes2DESGame`。

也可以在 PowerShell 中从仓库根目录直接打开同一个现有项目：

```powershell
$Project = (Resolve-Path .\Willes2DESGame.uproject).Path
& 'C:\Program Files\Epic Games\UE_5.6\Engine\Binaries\Win64\UnrealEditor.exe' $Project
```

在已正确关联 Unreal Engine 5.6 的 Windows 环境中，也可以双击 `Willes2DESGame.uproject`。

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

- UBT 工程文件生成成功，退出码为 `0`；
- `Willes2DESGameEditor Win64 Development` 构建成功，退出码为 `0`；
- 已生成 `Binaries\Win64\UnrealEditor-Willes2DESGame.dll`；
- `UnrealEditor-Cmd` 无人值守冒烟成功，退出码为 `0`，没有项目模块缺失、Fatal、断言或崩溃；
- GUI 启动时窗口标题为 `Willes2DESGame - 虚幻编辑器`，随后正常关闭且退出码为 `0`；
- UE 5.6 的最近项目记录和 `ProjectEditorRecords.json` 均包含现有 `.uproject` 路径。

## 无人值守启动冒烟

```powershell
$Project = (Resolve-Path .\Willes2DESGame.uproject).Path
& 'C:\Program Files\Epic Games\UE_5.6\Engine\Binaries\Win64\UnrealEditor-Cmd.exe' `
    $Project -Unattended -NullRHI -NoSound -NoSplash '-ExecCmds=Quit_Editor' -stdout -FullStdOutLogOutput
```

`Quit_Editor` 是 UE 5.6 Editor 的正常退出命令；普通 `Quit` 仅适用于 Game Engine，不会关闭 Editor 进程。

## 仓库卫生

`Binaries/`、`DerivedDataCache/`、`Intermediate/`、`Saved/` 和本机 IDE 状态均为可再生成的派生内容，不应提交。Unreal 的 `*.uasset` 与 `*.umap` 二进制资产由 Git LFS 管理。

仓库不跟踪 UBT 自动生成的 `.vsconfig`；该文件包含宽泛的可选工作负载，不能替代上面明确列出的 .NET Framework 4.8 SDK 要求。
