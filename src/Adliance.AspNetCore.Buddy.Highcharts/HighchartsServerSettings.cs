﻿namespace Adliance.AspNetCore.Buddy.Highcharts
{
    public interface IHighchartsServerSettings
    {
        string HighchartsServerUrl { get; }
    }

    public class HighchartsServerDefaultSettings : IHighchartsServerSettings
    {
        public string HighchartsServerUrl { get; set; } = "https://adliance-highcharts-server.azurewebsites.net";
    }
}
