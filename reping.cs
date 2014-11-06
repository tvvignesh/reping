/*
This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
	
	Main Author: T.V.VIGNESH - tvvignesh@techahoy.in - http://www.facebook.com/tvvignesh
*/

using System;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.IO;

namespace myping
{
public static class Globals
{
    public static String myssid = "";
	public static String uid = "";
	public static String upass = "";
	public static String curFile = @"mylogindata.dat";
	public static String choice = "y";
	public static int recontime = 10000;
	
}

class reping 
{
	static void Main(string[] args)
    {
		Console.WriteLine("\r\n\r\nREPING - A mini script by T.V.VIGNESH. Any suggestions or problems, mail tvvignesh@techahoy.in\r\n\r\n");
		
		if(File.Exists(Globals.curFile))
		{
			Console.WriteLine("Do you want to use the same details (user id, password, reconnect time) as you entered in your previous session? Press y or n");
			Globals.choice = Console.ReadLine();
			if(Globals.choice=="y"||Globals.choice=="Y")
			{
				LoadUserDetails();
			}
			else
			{
				CreateNewUser();
			}
		}
		else
		{
			CreateNewUser();
		}
			
        while (true)
        {

            if (!PingHost("8.8.8.8") || !PingHost("8.8.4.4") || !PingHost("www.google.com"))
            {
                string cmd = "wlan connect name=\""+Globals.myssid+"\" ssid=\""+Globals.myssid+"\"";
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = "netsh.exe";
                proc.StartInfo.Arguments = cmd; 
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true; 
                proc.Start();

                Console.WriteLine(proc.StandardOutput.ReadToEnd());
				LaunchCommandLineApp(Globals.uid,Globals.upass);
				LaunchCommandLineApp(Globals.uid,Globals.upass);
            }
            Console.WriteLine("Connected, trying again in "+Globals.recontime+" milliseconds");
            System.Threading.Thread.Sleep(Globals.recontime);
        }

    }
	
	static void LaunchCommandLineApp(string uid,string upass)
    {

	// Use ProcessStartInfo class
	ProcessStartInfo startInfo = new ProcessStartInfo();
	startInfo.CreateNoWindow = false;
	startInfo.UseShellExecute = false;
	startInfo.FileName = "ionautologin.exe";
	startInfo.WindowStyle = ProcessWindowStyle.Hidden;
	startInfo.Arguments = uid+" "+upass;

	try
	{
	    // Start the process with the info we specified.
	    // Call WaitForExit and then the using statement will close.
	    using (Process exeProcess = Process.Start(startInfo))
	    {
		exeProcess.WaitForExit();
	    }
	}
	catch
	{
	    // Log error.
	}
    }


    public static bool PingHost(string nameOrAddress)
    {
        bool pingable = false;
        Ping pinger = new Ping();

        try
        {
            PingReply reply = pinger.Send(nameOrAddress);

            pingable = reply.Status == IPStatus.Success;
        }
        catch (PingException)
        {
            // Discard PingExceptions and return false;
        }

        return pingable;
    }
	
	
	static void CreateNewUser()
	{
		BinaryWriter bw;
		
		if(File.Exists(@"mylogindata.dat"))
		{
			File.Delete(@"mylogindata.dat");
		}
		
		//GET NEW DETAILS
		Console.WriteLine("Please specify the SSID of the Wifi connection. For eg. ION@Block-10");
		Globals.myssid = Console.ReadLine();
		Console.WriteLine("Please specify the rechecking period in milliseconds eg.10000 for 10 seconds");
		Globals.recontime = Convert.ToInt32(Console.ReadLine());
		Console.WriteLine("Please specify the user ID to login with For eg. 100906133");
		Globals.uid = Console.ReadLine();
		Console.WriteLine("Please specify the password for login. For eg. password");
		Globals.upass = Console.ReadLine();
		
		
		//create the file
            try
            {
                bw = new BinaryWriter(new FileStream("mylogindata.dat",FileMode.Create));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot create file.");
                return;
            }
		
		  //writing into the file
            try
            {
                bw.Write(Globals.myssid);
                bw.Write(Globals.recontime);
                bw.Write(Globals.uid);
                bw.Write(Globals.upass);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot write to file.");
                return;
            }

            bw.Close();
	}
	
	
	
	static void LoadUserDetails()
	{
        BinaryReader br;
		
		try
            {
                br = new BinaryReader(new FileStream("mylogindata.dat",FileMode.Open));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot open file.");
                return;
            }
            try
            {
                Globals.myssid = br.ReadString();
                Globals.recontime= br.ReadInt32();
                Globals.uid = br.ReadString();
				Globals.upass = br.ReadString();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot read from file! Try deleting the .dat file in this directory, restart the program and try again!");
                return;
            }
            br.Close();
	}
	
}
}
