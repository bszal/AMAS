using System;

public static string Run(TimerInfo myTimer, ILogger log )
{
    log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
    
    DateTime timeUtc = DateTime.UtcNow;
    TimeZoneInfo CESZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
    return TimeZoneInfo.ConvertTimeFromUtc(timeUtc, CESZone).ToString();
        
}
