using Dapper;
using Newtonsoft.Json;
using Npgsql;

namespace BoxFactoryTest;

public static class Helper
{
    public static readonly Uri Uri;
    public static readonly string ProperlyFormattedConnectionString;
    public static readonly NpgsqlDataSource DataSource;

    static Helper()
    {
        string rawConnectionString;
        string envVarKeyName = "pgconn";

        rawConnectionString = Environment.GetEnvironmentVariable(envVarKeyName)!;
        if (rawConnectionString == null)
        {
            throw new Exception("Connection string environment variable is empty or null.");
        }

        try
        {
            Uri = new Uri(rawConnectionString);
            ProperlyFormattedConnectionString = string.Format(
                "Server={0};Database={1};User Id={2};Password={3};Port={4};Pooling=true;MaxPoolSize=3",
                Uri.Host,
                Uri.AbsolutePath.Trim('/'),
                Uri.UserInfo.Split(':')[0],
                Uri.UserInfo.Split(':')[1],
                Uri.Port > 0 ? Uri.Port : 5432);
            DataSource =
                new NpgsqlDataSourceBuilder(ProperlyFormattedConnectionString).Build();
            DataSource.OpenConnection().Close();
        }
        catch (Exception e)
        {
            throw new Exception("The provided connection string is invalid.", e);
        }
    }

    public static async Task<bool> IsCorsFullyEnabledAsync(string path)
    {
        using var client = new HttpClient();
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Options, new Uri(path));
            // Add Origin header to simulate CORS request
            request.Headers.Add("Origin", "https://google.com"); //Remember to change to our frontend
            request.Headers.Add("Access-Control-Request-Method", "GET");
            request.Headers.Add("Access-Control-Request-Headers", "X-Requested-With");

            var response = await client.SendAsync(request);

            bool corsEnabled = false;

            if (response.Headers.Contains("Access-Control-Allow-Origin"))
            {
                var accessControlAllowOrigin =
                    response.Headers.GetValues("Access-Control-Allow-Origin").FirstOrDefault();
                corsEnabled = accessControlAllowOrigin == "*" ||
                              accessControlAllowOrigin == "https://google.com";
            }

            var accessControlAllowMethods = response.Headers.GetValues("Access-Control-Allow-Methods").FirstOrDefault();
            var accessControlAllowHeaders = response.Headers.GetValues("Access-Control-Allow-Headers").FirstOrDefault();

            if (corsEnabled && (accessControlAllowMethods != null && accessControlAllowMethods.Contains("GET")) &&
                (accessControlAllowHeaders != null && accessControlAllowHeaders.Contains("X-Requested-With")))
            {
                return true;
            }
        }
        catch (Exception)
        {
            throw new Exception("CORS is not fully enabled for the given path. Please review the CORS settings.");
        }


        return false;
    }

    public static string BadResponseBody(string content)
    {
        return $@"
Tried converting the response body from the API into a class object, but something went wrong. Below are the details:

RESPONSE BODY: {{content}}

EXCEPTION:
";
    }

    public static void TriggerRebuild()
    {
        using (var conn = DataSource.OpenConnection())
        {
            try
            {
                conn.Execute(RebuildScript);
            }
            catch (Exception e)
            {
                throw new Exception("THERE WAS AN ERROR REBUILDING THE DATABASE.", e);
            }
        }
    }

    public static string MyBecause(object actual, object expected)
    {
        string expectedJson = JsonConvert.SerializeObject(expected, Formatting.Indented);
        string actualJson = JsonConvert.SerializeObject(actual, Formatting.Indented);

        return $"because we want these objects to be equivalent:\nExpected:\n{expectedJson}\nActual:\n{actualJson}";
    }

    public static string RebuildScript = @"
DROP SCHEMA IF EXISTS box_factory CASCADE;
CREATE SCHEMA box_factory;
CREATE TABLE IF NOT EXISTS box_factory.boxes
(
    id             INTEGER GENERATED BY DEFAULT AS IDENTITY,
    BoxName        TEXT,
    Price          DOUBLE PRECISION,
    BoxWidth       DOUBLE PRECISION,
    BoxLength      DOUBLE PRECISION,
    BoxHeight      DOUBLE PRECISION,
    BoxThickness   DOUBLE PRECISION,
    BoxColor       TEXT,
    BoxImgUrl       TEXT,
    PRIMARY KEY (id)
);
 ";

    public static string NoResponseMessage = "Failed to get a response from the API. Please check your request and try again.";
}