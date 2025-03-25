namespace ForCSharpTesting.GCCollectionTest;

public static class GCNotiofication
{
    // Variable for continual checking in the
    // While loop in the WaitForFullGCProc method.
    static bool checkForNotify = false;

    // Variable for ending the example.
    static bool finalExit = false;

    public static void StartGCCheck()
    {
        try
        {
            // Register for a notification.
            GC.RegisterForFullGCNotification(1, 1);
            Console.WriteLine("Registered for GC notification.");

            checkForNotify = true;

            // Start a thread using WaitForFullGCProc.
            Thread thWaitForFullGC = new Thread(WaitForFullGCProc);
            thWaitForFullGC.Start();
        }
        catch (InvalidOperationException invalidOp)
        {

            Console.WriteLine("GC Notifications are not supported while concurrent GC is enabled.\n"
                              + invalidOp.Message);
        }
    }

    public static void StopGCCheck()
    {
        finalExit = true;
        checkForNotify = false;
        GC.CancelFullGCNotification();
    }

    public static void WaitForFullGCProc()
    {
        while (true)
        {
            // CheckForNotify is set to true and false in Main.
            while (checkForNotify)
            {
                Console.WriteLine("Before WaitForFullGCApproach");
                // Check for a notification of an approaching collection.
                GCNotificationStatus s = GC.WaitForFullGCApproach();
                
                Console.WriteLine("After WaitForFullGCApproach");
                if (s == GCNotificationStatus.Succeeded)
                {
                    Console.WriteLine("GC Notification raised. OnFullGCApproachNotify");
                }
                else if (s == GCNotificationStatus.Canceled)
                {
                    Console.WriteLine("GC Notification cancelled.");
                    break;
                }
                else
                {
                    // This can occur if a timeout period
                    // is specified for WaitForFullGCApproach(Timeout)
                    // or WaitForFullGCComplete(Timeout)
                    // and the time out period has elapsed.
                    Console.WriteLine("GC Notification not applicable.");
                    break;
                }

                // Check for a notification of a completed collection.
                GCNotificationStatus status = GC.WaitForFullGCComplete();
                if (status == GCNotificationStatus.Succeeded)
                {
                    Console.WriteLine($"GC Notification raised. OnFullGCCompleteEndNotify");
                }
                else if (status == GCNotificationStatus.Canceled)
                {
                    Console.WriteLine("GC Notification cancelled.");
                    break;
                }
                else
                {
                    // Could be a time out.
                    Console.WriteLine("GC Notification not applicable.");
                    break;
                }
            }

            Thread.Sleep(500);
            // FinalExit is set to true right before
            // the main thread cancelled notification.
            if (finalExit)
            {
                break;
            }
        }
    }
}