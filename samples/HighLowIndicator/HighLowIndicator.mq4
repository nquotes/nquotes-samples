
#property indicator_chart_window    // Indicator is drawn in the main window
#property indicator_buffers 2       // Number of buffers
#property indicator_color1 Blue     // Color of the 1st line
#property indicator_color2 Red      // Color of the 2nd line

#import "nquotes/nquoteslib.ex4"
    int nquotes_setup(string className, string assemblyName);
    int nquotes_init();
    int nquotes_start();
    int nquotes_deinit();
    int nquotes_get_property_array_size(string name);
    int nquotes_get_property_adouble(string name, double& value[]);
#import

// Declaring arrays (for indicator buffers)
double Buf0[];
double Buf1[];             

int init()                          
{
    nquotes_setup("HighLowIndicator.HighLowIndicator", "HighLowIndicator");

    // Assigning an array to a buffer
    SetIndexBuffer(0, Buf0);         
    // Assigning an array to a buffer
    SetIndexBuffer(1, Buf1);         
    
    Print("nquotes_init...");
    return (nquotes_init());
}

int start()                         
{
    // run the indicator C# code
    Print("nquotes_start...");
    int result = nquotes_start();
    if (result < 0)
    {
        Print("nquotes_start has failed");
        return (result);
    }
    
    // we know that in C# code both arrays have the same size
    int size = nquotes_get_property_array_size("Buffer0NewValues");

    // load new data for Buf0 from the property
    double buffer0NewValues[];
    ArrayResize(buffer0NewValues, size);
    nquotes_get_property_adouble("Buffer0NewValues", buffer0NewValues);

    // load new data for Buf1 from the property
    double buffer1NewValues[];
    ArrayResize(buffer1NewValues, size);
    nquotes_get_property_adouble("Buffer1NewValues", buffer1NewValues);

    // copy new data to the indicator buffers
    for (int i = 0; i < size; i++)
    {
        Buf0[i] = buffer0NewValues[i];
        Buf1[i] = buffer1NewValues[i];
    }
    
    return (result);
}

int deinit()
{
    Print("nquotes_deinit...");
    return (nquotes_deinit());
}

