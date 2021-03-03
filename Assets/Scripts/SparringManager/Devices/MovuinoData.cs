using System.Collections.Generic;
using SparringManager.DataManager;
using UnityEngine;

namespace SparringManager.Device
{
	/// <summary>
	/// Inherit this if you want to represent a Movuino Data Type
	/// </summary>
	public abstract class MovuinoData
	{
		public string movuinoAddress { get { return GetAddress (); } }
		public class WrongMovuinoDataFormatException : UnityException
		{

		};

		public static T CreateMovuinoData<T> () where T : MovuinoData, new()
		{
			T newMovuinoData = new T ();
			return newMovuinoData;
		}

		public abstract void ToMovuinoData (OscMessage message);
		protected abstract string GetAddress();
	}

	/// <summary>
	/// Data for the accelerometer and the gyroscope of Movuino
	/// </summary>
	public class MovuinoSensorData : MovuinoData
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

		public List<object> MovData = new List<object>();

		public static string address = "/data";

		public override void ToMovuinoData (OscMessage message)
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

			//TEEEEEEEST
			MovData.Add(accelerometer);
			MovData.Add(gyroscope);
			MovData.Add(magnetometer);
			DataController.DataSessionMovuino.StockData(MovData);
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

    public class MovuinoXMM : MovuinoData
	{

		public int gestId;
		public float gestProg;

		public static string address = "/gesture";

		public override void ToMovuinoData (OscMessage message)
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
	public class PolarBPM : MovuinoData
	{
		public float bpm;

		public static string address = "/bpm";

		public override void ToMovuinoData(OscMessage message)
		{
			bpm = message.GetFloat(0);
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

