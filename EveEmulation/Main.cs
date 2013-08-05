using System;
using System.IO;
using System.Net;
using System.Collections.Generic;

namespace EveEmulation
{
	class MainClass
	{
		public const ushort MachoNetVersion = 320;
		public const double EVEVersionNumber = 7.31;
		public const int EVEBuildVersion = 360229;
		public const string EVEProjectCodename = "EVE-EVE-TRANQUILITY";
		public const string EVEProjectRegion = "ccp";
		public const string EVEProjectVersion = "EVE-EVE-TRANQUILITY@ccp";
		public static void Main (string[] args)
		{
			Console.WriteLine ("Eve Emulation Server Initialized.");
			System.Net.Sockets.TcpListener listen = new System.Net.Sockets.TcpListener (IPAddress.Loopback, 26000);
			listen.Start ();
			Console.Write ("Now listening on ");
			Console.Write (IPAddress.Loopback.ToString ());
			Console.WriteLine (":26000");
			while (!listen.Pending())
				continue;
			var eveClient = listen.AcceptTcpClient ();
			Console.WriteLine ("Connected.");
			Console.WriteLine("Handshaking...");
			var SendBuffer = System.Text.UTF8Encoding.UTF8.GetBytes("Hello!");
			eveClient.GetStream().Write(SendBuffer, 0, SendBuffer.Length);
			while (eveClient.Client.Available <= 0)
				continue;
			Console.WriteLine ("Got Stream");
			byte[] buffer = new byte[1024];
			int lengthFix = 0;
			var stream = eveClient.GetStream ();
			List<byte> RawListOfBytes = new List<byte>();
			do
			{
				lengthFix = stream.Read(buffer, 0, buffer.Length);
				Array.Resize(ref buffer, lengthFix);
				RawListOfBytes.AddRange(buffer);
				buffer = new byte[1024];

			} while (lengthFix > 0);
			Console.WriteLine("Output:");
			Console.WriteLine("");
			Console.Write (BitConverter.ToString(RawListOfBytes.ToArray()));
			Console.ReadLine();
		}
	}
}
