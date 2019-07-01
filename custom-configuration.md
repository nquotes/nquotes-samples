# How to use built in NQuotes.Config file to add your own custom variables such as database connection strings or custom settings for your EA.

NQuotes has a built in feature to store and parse xml config file in file named nquotes.config that is located at "%TERMINAL_DATA_PATH%\MQL4\nquotes.config". [Read more about it](http://www.nquotes.net/docs/nquotes-config/). 

Lets say in the [Moving Average example](https://github.com/nquotes/nquotes-samples/tree/master/samples/MovingAverageCustom) you'd rather 
have all the values of the MovingAverage & MovingPeriod in a config file that can be stored in your source control.

## Steps:

1. You create the method that will load the contents of your config file
```
private static NQuotes.Config LoadConfig()
{
    string configFilePath = NQuotes.Config.FindFilePath(NQuotes.ConfigSettings.SharedSettings.LibraryDirPath);
    return new NQuotes.Config(configFilePath);
}
```
2. You parse the values from the config file
```
var config = LoadConfig();
int movingAverage = int.Parse(config.Get("MovingAverage"));
int movingShift = int.Parse(config.Get("MovingShift"));
```
You can then pass the variable to the ```iMA(..)``` method

3. And the variables in nquotes.config you add it like this:
```
    <add key="MovingAverage" value="18" />
    <add key="MovingShift" value="6" />
```
That's it!
