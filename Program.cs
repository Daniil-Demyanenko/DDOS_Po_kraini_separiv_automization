using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Sharprompt;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DosStart
{
    class Program
    {
        static readonly string PathToSettings = AppDomain.CurrentDomain.BaseDirectory + "AttackSettings.json";
        static Process AttackProcess = null;
        static async Task Main(string[] args)
        {
            var speed = Prompt.Select("Выберите скорость DOS режима", new[] { "Медленно", "Быстро" });
            var staistic = Prompt.Confirm("Отправлять статистику?", defaultValue: true);

            var AC = GetAttackCommands();
            string currentMode = "";
            if (speed == "Медленно" && staistic == true) currentMode = AC.SlowWithStatisticCommand;
            if (speed == "Медленно" && staistic == false) currentMode = AC.SlowWithoutStatisticCommand;
            if (speed == "Быстро" && staistic == true) currentMode = AC.FastWithStatisticCommand;
            if (speed == "Быстро" && staistic == false) currentMode = AC.FastWithoutStatisticCommand;

            Console.WriteLine($"Атака запущена командой: \n{currentMode}");

            StartMode(currentMode);

            await AttackProcess.WaitForExitAsync();
        }

        static CommandSettings GetAttackCommands()
        {
            CommandSettings CS = null;
            bool needCreateNew = false;

            try
            {
                CS = JsonSerializer.Deserialize<CommandSettings>(File.ReadAllText(PathToSettings));
            }
            catch
            {
                needCreateNew = true;
            }

            if (CS is null || needCreateNew) CS = CreateJsonSettings();

            return CS;
        }

        static CommandSettings CreateJsonSettings()
        {
            var Settings = new CommandSettings();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            };
            string json = JsonSerializer.Serialize(Settings, options);

            File.WriteAllText(PathToSettings, json);

            return Settings;
        }

        static void StartMode(string mode)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "/bin/bash";
            psi.Arguments = $"-c \"{mode}\"";
            psi.UseShellExecute = true;
            // psi.RedirectStandardOutput = true;
            AttackProcess = Process.Start(psi);
            //Console.WriteLine(p.StandardOutput.ReadToEnd());
        }

        ~Program()
        {
            try
            {
                AttackProcess.Kill();
            }
            catch { }
        }
    }
}
