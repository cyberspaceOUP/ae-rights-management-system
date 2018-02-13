/*
	* Title: LOGGING.
	* Description: This file encapsulates the Logger Interface.
	* Copyright: GoldwynLED
	* Company: VRVirtual.com Pvt. Ltd.
	* Dated: August-05-08
	* @filename ILogger.cs
	* @author <<a href="mailto:sanjeevk@vrvirtual.com">Sanjeev Kumar</a> > 
	* ****************************  Modification History  *********************************************
	* Sr No:		Date			Modified by	    Tracker No					Description
	* *************************************************************************************************
	***************************************************************************************************
*/

using System;

namespace Logger
{
	/// <summary>
	/// Interface for the logging utility.
	/// </summary>
	public interface ILogger
	{

		/// <summary>
		/// Logs an Exception.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <param name="ex"></param>
		/// <returns></returns>
		bool LogException		(string userID ,Severity severityID, string className, string functionName , Exception ex );

		/// <summary>
		/// Logs an Exception.
		/// </summary>
		/// <param name="ex"></param>
		/// <returns></returns>
		bool LogException(Exception ex);
		/// <summary>
		/// Writes to the configured Log.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <param name="message"></param>
		/// <param name="level"></param>
		/// <returns></returns>
		bool WriteToLog			(string userID , Severity severityID, string className, string functionName , string message, Level level); 

		/// <summary>
		/// Writes to the configured Log.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="level"></param>
		/// <returns></returns>
		bool WriteToLog			(string message, Level level); 

		/// <summary>
		/// Writes the Information to the Log.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		bool WriteToInfoLog		(string userID , Severity severityID, string className, string functionName , string message); 

		/// <summary>
		/// Writes the Information to the Log.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		bool WriteToInfoLog		(string message); 

		/// <summary>
		/// Writes the error to the Log.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		bool WriteToErrorLog	(string userID , Severity severityID, string className, string functionName , string message);

		/// <summary>
		/// Writes the error to the Log.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		bool WriteToErrorLog	(string message);

		/// <summary>
		/// Writes the Warning to the Log.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		bool WriteToWarningLog	(string userID , Severity severityID, string className, string functionName , string message);

		/// <summary>
		/// Writes the Warning to the Log.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		bool WriteToWarningLog	(string message);

	}
}
