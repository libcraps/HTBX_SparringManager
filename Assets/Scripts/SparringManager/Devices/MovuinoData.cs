using System.Collections.Generic;
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

		public static T CreateMovuinoData<T> (List<object> list) where T : MovuinoData, new()
		{
			T newMovuinoData = new T ();
			newMovuinoData.ToMovuinoData (list);
			return newMovuinoData;
		}

		protected abstract void ToMovuinoData (List<object> list);
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

		public static string address = "/movuino";

		protected override void ToMovuinoData (List<object> list)
		{
			try {
				if (list.Count >= 10) {
                    string id = (string)list[0];
					float ax = (float)list [1];
					float ay = (float)list [2];
					float az = (float)list [3];
					float gx = (float)list [4];
					float gy = (float)list [5];
					float gz = (float)list [6];
					float mx = (float)list [7];
					float my = (float)list [8];
					float mz = (float)list [9];
					this.accelerometer = new Vector3 (ax, ay, az);
					this.gyroscope = new Vector3 (gx, gy, gz);
					this.magnetometer = new Vector3 (mx, my, mz);
				} else {
					throw new WrongMovuinoDataFormatException ();
				}
			} catch (UnityException) {
				throw new WrongMovuinoDataFormatException ();
			}
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

		protected override void ToMovuinoData (List<object> list)
		{
			try {
				if (list.Count >= 2) {
					this.gestId = (int)list [0];
					this.gestProg = Mathf.Clamp ((float)list [1], 0.0f, 1.0f);
				} else {
					throw new WrongMovuinoDataFormatException ();
				}
			} catch (UnityException) {
				throw new WrongMovuinoDataFormatException ();
			}
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
}

