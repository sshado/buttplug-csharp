using System;
using System.Collections.Generic;
using System.Text;

using Serilog.Sinks.SystemConsole.Themes ;

namespace Buttplug.Client.Platforms.Bluetooth
{
    public static class ConsoleExtensions
    {
        public static SystemConsoleTheme BluetoothConsole { get; } = new SystemConsoleTheme(
            new Dictionary<ConsoleThemeStyle, SystemConsoleThemeStyle>
            {
                [ConsoleThemeStyle.Text] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Blue },
                [ConsoleThemeStyle.SecondaryText] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
                [ConsoleThemeStyle.TertiaryText] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
                [ConsoleThemeStyle.Invalid] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkYellow },
                [ConsoleThemeStyle.Null] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.White },
                [ConsoleThemeStyle.Name] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkBlue },
                [ConsoleThemeStyle.String] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkBlue },
                [ConsoleThemeStyle.Number] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkBlue },
                [ConsoleThemeStyle.Boolean] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkBlue },
                [ConsoleThemeStyle.Scalar] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkBlue },
                [ConsoleThemeStyle.LevelVerbose] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkGray, Background = ConsoleColor.White },
                [ConsoleThemeStyle.LevelDebug] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkCyan, Background = ConsoleColor.White },
                [ConsoleThemeStyle.LevelInformation] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkGreen, Background = ConsoleColor.White },
                [ConsoleThemeStyle.LevelWarning] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Cyan, Background = ConsoleColor.Yellow },
                [ConsoleThemeStyle.LevelError] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkMagenta, Background = ConsoleColor.White },
                [ConsoleThemeStyle.LevelFatal] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkRed, Background = ConsoleColor.Gray }
            });
    }
}