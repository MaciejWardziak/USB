using System.Management;

class Program
{
    static void Main()
    {
        var insertWatcher = new ManagementEventWatcher();
        insertWatcher.Query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2"); 
        insertWatcher.EventArrived += new EventArrivedEventHandler(DeviceInsertedEvent);
        insertWatcher.Start();

        var removeWatcher = new ManagementEventWatcher();
        removeWatcher.Query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 3");
        removeWatcher.EventArrived += new EventArrivedEventHandler(DeviceRemovedEvent);
        removeWatcher.Start();

        Console.WriteLine("Monitoring USB connections. Press Enter to exit.");
        Console.ReadLine();

        insertWatcher.Stop();
        removeWatcher.Stop();
    }

    private static void DeviceInsertedEvent(object sender, EventArrivedEventArgs e)
    {
        Console.WriteLine("USB device inserted.");
    }

    private static void DeviceRemovedEvent(object sender, EventArrivedEventArgs e)
    {
        Console.WriteLine("USB device removed.");
    }
}