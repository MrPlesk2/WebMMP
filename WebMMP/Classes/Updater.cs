using WebMMP.Scripts;

public class Updater
{
    private DateTime currentProgramTime = DateTime.UtcNow.AddHours(5);

    private static DateTime[] updateUtcTime = new[]
    {
        new DateTime(1,1,1, 7,0,0).AddHours(-5),
        new DateTime(1,1,1, 10,30,0).AddHours(-5),
        new DateTime(1,1,1, 12,10,0).AddHours(-5),
        new DateTime(1,1,1, 14,20,0).AddHours(-5),
        new DateTime(1,1,1, 16,0,0).AddHours(-5),
        new DateTime(1,1,1, 17,40,0).AddHours(-5),
        new DateTime(1,1,1, 19,20,0).AddHours(-5),
    };

    private Schedule schedule;

    public Updater(Schedule schedule)
    { 
        this.schedule = schedule;
    }

    public void Update()
    {
        while (true)
        {
            if (DateTime.UtcNow.AddHours(5).Day != currentProgramTime.Day)
            {
                lock (schedule)
                {
                    schedule.UpdateGroups();
                }
                currentProgramTime = DateTime.UtcNow.AddHours(5);
            }
            foreach (var updateTime in updateUtcTime)
                if (updateTime.TimeOfDay - DateTime.UtcNow.TimeOfDay <= new TimeSpan(0,0,0,30) && 
                    updateTime.TimeOfDay - DateTime.UtcNow.TimeOfDay >= new TimeSpan(0, 0, 0, -30))
                {
                    schedule.UpdateBook();
                    break;
                }

            Thread.Sleep(60000);
        }
    }
}