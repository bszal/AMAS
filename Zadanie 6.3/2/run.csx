using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

public static async Task Run(string myQueueItem, ILogger log)
{
    log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

    
    var connectionString =  Environment.GetEnvironmentVariable("SqlCon");
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        TimeZoneInfo westZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        log.LogInformation($"Time {westZone}");

        var text = $"INSERT INTO [dbo].[Logs] ([LogMessage], [CreateDate]) VALUES ('{myQueueItem}', '{TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, westZone)}') ";
        log.LogInformation($"Sql Value: {text} ");

        using (SqlCommand cmd = new SqlCommand(text, connection))
        {
            var rows = await cmd.ExecuteNonQueryAsync();
        }
    
    }
}
