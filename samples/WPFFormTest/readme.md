# WPFForm expert advisor sample

The WPFFormTest sample demonstrates how to spawn a non-blocking UI from an NQuotes-based expert advisor.

You can find the sample source code in the "%TERMINAL_DATA_PATH%\MQL4\Projects\nquotes\WPFFormTest" folder.

1. Build the sample Visual Studio project (WPFFormTest.sln).
2. Run the project in debugger. It will start DebugHost and wait for the MT4 terminal.
3. Start the terminal and attach the "nquotes" EA to a chart.

The expert advisor works in a dedicated thread (EA thread) which is controlled by the MetaTrader terminal and NQuotes. 
This thread must be available for calls from MetaTrader to .NET (for example to call start() function), and it is blocked while the .NET MQL API calls are running.
Thus the EA thread can't be used to run non-blocking UI.

A special UI thread is created to display the UI and handle UI updates. The WPFForm API is single-threaded meaning that all UI API calls must be done from the same thread where a form was created and displayed. Not only this includes the Form class methods and UI event handlers, but also methods and properties of form controls such as buttons, labels etc. Even an operation as simple as a label text update must be done from the UI thread:

```
Dispatcher.InvokeAsync(new Action(() =>
	{
		symbolLabel.Content = symbol;
		brokerCompanyLabel.Content = terminalCompany;
	}));
```

A more detailed discussion of this can be found here:
[WPF Threading Model](https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/threading-model) and 
[Dispatcher.Invoke Method](https://docs.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatcher.invoke?view=netframework-4.8)

While the UI is shown, the start() method is running on the EA thread in a "while" loop. This loop is a basis for an event loop that can handle UI commands by running .NET MQL API calls and passing results back to the UI thread to update the UI controls. The MqlApi object must only be accessed from the EA thread inside the start() method. A way to do this is that when UI button is clicked it should post a command or a Task to a queue, then the EA thread "while" loop finds and executes the Task, and then notifies the UI thread that it's done.

### Limitations

- Make sure that MqlApi calls happen only in the EA thread, and UI update calls happen only in the UI thread.
- The window is not attached to the terminal window. You should handle the window arrangement and keep it in the foreground if needed.
