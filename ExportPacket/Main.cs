using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.IO.Compression;
namespace ExportPacket
{
	class MainClass
	{
		/*
		 * 
		ushort MachoNetVersion = 320;
		double EVEVersionNumber = 7.31;
		int EVEBuildVersion = 360229;
		string EVEProjectCodename = "EVE-EVE-TRANQUILITY";
		string EVEProjectRegion = "ccp";
		string EVEProjectVersion = "EVE-EVE-TRANQUILITY@ccp";
		 */
		static void Main()
		{
			byte GzipMarker = 126; // 0x7E
			// Has 60 bytes worth of raw packet data
			FileStream stream = new FileStream("ExportedStamp", FileMode.Open);
			int PacketLengthMarker = 0;
			ushort Machonet = 0;

			// Resize for Int32 Buffer
			byte[] buffer = new byte[4];
			stream.Read(buffer, 0, 4);
			// It's a packet length in worth of data (Purpose of GZip, 
			// otherwise it'd be retarded since TCP protocol already covered that.)
			PacketLengthMarker = BitConverter.ToInt32(buffer, 0);

			// Gzip Stream isn't necessary, because it already extracted via 7E marker.
			//var fixStream = new DeflateStream(stream, CompressionMode.Decompress); 
			stream.Read(buffer, 0, 1); // This is 7E marker

			stream.Read(buffer, 0, 4); // This is the unknown marker that occupy 4 bytes of the marshal stream

			buffer = new byte[2];
			stream.Read(buffer, 0, 2);
			Machonet = BitConverter.ToUInt16(buffer, 0);
			//Machonet = BitConverter.ToUInt16(buffer, 0);
			Console.WriteLine("Packet Size: " + PacketLengthMarker);
			Console.WriteLine("Machonet: " + Machonet);
			Console.ReadLine ();
		}
	}
}
