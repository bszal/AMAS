
using System;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


public static HttpResponseMessage Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string date = req.Query["date"];

    if (String.IsNullOrEmpty(date)) 
    {
        return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
    }

    var json = new StringBuilder();
    var cs = Environment.GetEnvironmentVariable("SqlCon");
    var sql = $"SELECT CreateDate FROM dbo.Logs WHERE CAST(CreateDate AS DATE) ='{date}' for json path;";

     using (SqlConnection connection = new SqlConnection(cs))
    {
        connection.Open();

        log.LogInformation("Connected SQL");
        log.LogInformation("SQL:  " + sql);

        using (SqlCommand cmd = new SqlCommand(sql, connection))
        {
            SqlDataReader r = cmd.ExecuteReader();

            if (!r.HasRows)
            {
                json.Append("No LogMessage");
            }
            else
            {
                while (r.Read())
                {
                    json.Append(r.GetString(0));
                }
            }
        }
    }

    var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
    response.Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
    
    return response;

}
