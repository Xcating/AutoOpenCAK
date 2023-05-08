<!-- TOC -->
* [English README](#english-readme)
* [自动启动应用程序](#自动启动应用程序)
* [运行效果](#运行效果)
* [编写初衷](#编写初衷)
* [用途](#用途)
  * [如何使用](#如何使用)
  * [注意事项](#注意事项)
<!-- TOC -->
# English README

[English version](https://github.com/Micah123321/AutoOpenCAK/blob/main/README-EN.md)

# 自动启动应用程序

本项目是一个使用C#编写的自动启动应用程序,可定制化的绕过原神hoyoprot 并且同时启动任意dll注入器 并且可同时启动3dm原神模型修改工具以及ce修改

# 运行效果
![image](https://user-images.githubusercontent.com/76832465/236788391-98559835-a629-48e7-923b-ccdccf39f685.png)

# 编写初衷

- 由 **Strigger** 编写的 **Cotton Buds** 因为是ct表构成的结构 无法完成如:地图传送 内置米哈游小地图等功能
- 而 **korepi** 项目因为部分代码缺失导致无法使用剧情加速,杀戮光环等功能
- 使用本程序能在开启 **korepi** 项目的同时过掉验证同时开启 **Cotton Buds**
- 做到不输于 **AKEBI** 的游戏体验

# 用途
- 执行配置好,就可以做到一键运行账号切换器 任意注入方式启动器 过检测的方式启动原神


## 如何使用

1. 打开release下载最新版本[Releases](https://github.com/Micah123321/AutoOpenCAK/releases)
2. 想要愉悦的使用本工具,最理想的情况是同拥有GenshinAccount.exe(账号切换工具) 3dm(模型修改工具)  **korepi** 以及**Cotton Buds**
3. 请根据自己的情况配置config.ini![](https://www.ake1.com/mkoss/2023-05-08/13583df7.png)
4. 对应的工具请自行寻找
## 注意事项

- 如果启动3dm执行程序导致闪退 请开启独显直连 可能是驱动导致无法兼容启动

- 本项目中的代码仅适用于Windows系统。
- 如果您在使用应用程序时遇到任何问题，请查看应用程序的日志文件以获取更多信息。
- 请确保您具有足够的权限来添加和删除Windows系统的启动项。
- 如果您想要更改应用程序的启动项设置，请查看“OpenCorepiAndBypass\OpenCorepiAndBypass\src\Program.cs”文件中的代码并进行必要的更改。

祝您好运！
