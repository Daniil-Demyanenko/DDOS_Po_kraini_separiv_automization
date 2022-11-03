namespace DosStart
{
    public class CommandSettings
    {
        public string FastWithoutStatisticCommand { get; set; }
        public string SlowWithoutStatisticCommand { get; set; }
        public string FastWithStatisticCommand { get; set; }
        public string SlowWithStatisticCommand { get; set; }

        public CommandSettings()
        {
            FastWithoutStatisticCommand = "echo Настройте комманду для быстрой атаки без статистики";
            SlowWithoutStatisticCommand = "echo Настройте комманду для медленной атаки без статистики";
            FastWithStatisticCommand = "echo Настройте комманду для быстрой атаки со статистикой";
            SlowWithStatisticCommand = "echo Настройте комманду для медленной атаки со статистикой";
        }
    }
}