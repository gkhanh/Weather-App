﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace Weather_App.WeatherServiceClient
{
    public class AccessWeatherAPI<T>
    {
        private const string OpenWeatherAPI = "http://api.openweathermap.org/data/2.5/weather?q=";
        private const string API_Key = "653b1f0bf8a08686ac505ef6f05b94c2";
        HttpClient _httpClient = new HttpClient();

        //waiting to receive data from API link
        public async Task<T> GetAllWeathers(string city)
        {
            var json = await _httpClient.GetStringAsync(OpenWeatherAPI + city + "&APPID=" + API_Key);
            var getWeatherModels = JsonConvert.DeserializeObject<T>(json);
            return getWeatherModels;
        }
    }
}
