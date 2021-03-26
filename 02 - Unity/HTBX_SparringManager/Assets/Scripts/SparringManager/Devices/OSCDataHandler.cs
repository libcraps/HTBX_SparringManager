/*
 * http://thomasfredericks.github.io/UnityOSC/
 * 
 * https://github.com/thomasfredericks/UnityOSC
 */

using System.Collections.Generic;
using SparringManager.DataManager;
using UnityEngine;

namespace SparringManager.Device
{
	/// <summary>
	/// Inherit this if you want to represent a Movuino Data Type, it allows you to connect on an OSC server and get data of the correspondant address:
	/// </summary>
	public abstract class OSCDataHandler
	{
		/*
		 * Mother class that represent and connect device on OSC server
		 * Type of data handled : movuino 9axes, bpm, and xmm (gesture)
		 * OSCAddress :
		 *		/data : movuino 9axes
		 *		/gesture : xmm
		 *		/bpm : polar
		 */
		public string OSCAddress { get { return GetAddress (); } }
		public class WrongOSCDataHandlerFormatException : UnityException
		{

		};

		// <=> constructor
		public static T CreateOSCDataHandler<T> () where T : OSCDataHandler, new()
		{
			T newMovuinoData = new T ();
			return newMovuinoData;
		}

		//Method that read data of an OSCmessage
		public abstract void ToOSCDataHandler (OscMessage message);
		protected abstract string GetAddress();
	}

	/// <summary>
	/// Data of the accelerometer, the gyroscope and the magnetometer of Movuino
	/// </summary>
	public class OSCMovuinoSensorData : OSCDataHandler
	{
		/// <summary>
		/// Accelerometer data.
		/// </summary>
		public Vector3 accelerometer;
		/// <summary>
		/// Gysocope data.
		/// </summary>
		public Vector3 gyroscope;
		/// <summary>
		/// Magnetometer data.
		/// </summary>
		public Vector3 magnetometer;

		public static string address = "/data";

		public override void ToOSCDataHandler (OscMessage message)
		{
			float ax = message.GetFloat(0);
			float ay = message.GetFloat(1);
			float az = message.GetFloat(2);
			float gx = message.GetFloat(3);
			float gy = message.GetFloat(4);
			float gz = message.GetFloat(5);
			float mx = message.GetFloat(6);
			float my = message.GetFloat(7);
			float mz = message.GetFloat(8);
			accelerometer = new Vector3(ax, ay, az);
			gyroscope = new Vector3(gx, gy, gz);
			magnetometer = new Vector3(mx, my, mz);

		}

		protected override string GetAddress ()
		{
			return address;
		}

		public override string ToString ()
		{
			return string.Format ("[MovuinoSensorData] = "
			+ "Accelerometer = "
			+ accelerometer.ToString ()
			+ " Gyroscope = "
			+ gyroscope.ToString ()
			+ " Magnetometer = "
			+ magnetometer.ToString ());
		}
	}

	/// <summary>
	/// Data of the XMM from the pathMax (normally)
	/// </summary>
	/// <remarks>It gets the gesture ID and his progression</remarks>
	public class OSCMovuinoXMM : OSCDataHandler
	{
		public int gestId;
		public float gestProg;

		public static string address = "/gesture";

		public override void ToOSCDataHandler (OscMessage message)
		{
			gestId = message.GetInt(0);
			gestProg = message.GetFloat(1);
		}
			
		protected override string GetAddress ()
		{
			return address;
		}

		public override string ToString ()
		{
			return string.Format ("[MovuinoXMM] = "
			+ "xmmGestId = "
			+ gestId.ToString ()
			+ "xmmGestProg = "
			+ gestProg.ToString ());
		}
	}
	public class OSCPolarBPM : OSCDataHandler
	{
		public float bpm;

		public static string address = "/bpm";

		public override void ToOSCDataHandler(OscMessage message)
		{
			bpm = message.GetFloat(0);
			Debug.Log("BPM recu via OSC : " + bpm);

		}

		protected override string GetAddress()
		{
			return address;
		}

		public override string ToString()
		{
			return string.Format("[PolarBPM] = "
			+ "bpm = " + bpm);
		}
	}
}

