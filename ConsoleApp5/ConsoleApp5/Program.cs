using ConsoleApp5;

ClientWeather client = new ClientWeather("b4af138646d645c952b8e9b795cbabe4");

WeatherModel result = await client.Get();

result.PrintResult();

WeatherModel result2 = await client.Post("Lausanne");

result2.PrintResult();
