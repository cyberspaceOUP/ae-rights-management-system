/*
	*  Title:LOGGING.
	* Description: This file encapsulates the Trace logger class, used for logging the event in the Event Trace.
	* Copyright: GoldwynLED
	* Company: VRVirtual.com Pvt. Ltd.
	* Dated: August-05-08
	* @filename TraceLogger.cs
	* @author <<a href="mailto:sanjeevk@vrvirtual.com">Sanjeev Kumar</a> > 
	* ****************************  Modification History  *********************************************
	* Sr No:		Date			Modified by	    Tracker No					Description
	* *************************************************************************************************
	***************************************************************************************************
*/

using System;
using System.Text; 
using System.IO;
using System.Configuration; 
using System.Diagnostics; 
using System.Web; 
using System.Reflection;

namespace Logger
{
	/// <summary>
	/// Logs events to the Application Trace
	/// </summary>
	public class TraceLogger : AbstractLogger
	{
		private const  string LOGGER_FILE	=   "Logger_FileName";

		/// <summary>
		/// Loads the Class into the App Domain.
		/// </summary>
		public TraceLogger()
		{
			//AppSettingsReader reader	= new AppSettingsReader ();
			//ApplicationLogLevel			= (string)reader.GetValue (LOGGER_LEVEL,	typeof(string));
		}

		/// <summary>
		/// Loga the Exception Into the log.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <param name="ex"></param>
		/// <returns></returns>
		public override bool LogException		(string userID , Severity severityID,string className, string functionName , Exception ex )
		{
			//[2] -- ADD -- START Getting the class name and function name
			StackTrace stackTrace = new StackTrace();
			StackFrame stackFrame = stackTrace.GetFrame(1);
			MethodBase methodBase = stackFrame.GetMethod();
			className=Convert.ToString(methodBase.DeclaringType);
			functionName =methodBase.Name;
			//[2] -- ADD -- END			
			try
			{
				if(!(UpdateTrace(PrepMsg(userID,ex))))
				{
					ILogger altLogger = LoggerFactory.createAlternateLogger();
					return(altLogger.LogException(userID ,  severityID,className, functionName , ex )); 
				}
				return(true);
			}
			catch(Exception exp)
			{
				ClassForProblemResolutionOnly obj = new ClassForProblemResolutionOnly();
				obj.WriteToEventLog(exp);
				return (false);
			}
		}


		/// <summary>
		/// Writes The Information to the Log.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public override bool WriteToInfoLog		(string userID , Severity severityID, string className, string functionName , string message)
		{
			//[2] -- ADD -- START Getting the class name and function name
			StackTrace stackTrace = new StackTrace();
			StackFrame stackFrame = stackTrace.GetFrame(1);
			MethodBase methodBase = stackFrame.GetMethod();
			className=Convert.ToString(methodBase.DeclaringType);
			functionName =methodBase.Name;
			//[2] -- ADD -- END			
			try
			{
				if(!(UpdateTrace(PrepMsg(userID, severityID, message, Level.INFO.ToString(), className, functionName))))
				{
					ILogger altLogger = LoggerFactory.createAlternateLogger();
					return(altLogger.WriteToInfoLog(userID , severityID, className, functionName , message)); 
				}
				return(true);
			}
			catch(Exception ex)
			{
				ClassForProblemResolutionOnly obj = new ClassForProblemResolutionOnly();
				obj.WriteToEventLog(ex);
				return (false);
			}
		}

		/// <summary>
		/// Writes the Error Information into the log.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public override bool WriteToErrorLog	(string userID , Severity severityID,  string className, string functionName , string message)
		{
			//[2] -- ADD -- START Getting the class name and function name
			StackTrace stackTrace = new StackTrace();
			StackFrame stackFrame = stackTrace.GetFrame(1);
			MethodBase methodBase = stackFrame.GetMethod();
			className=Convert.ToString(methodBase.DeclaringType);
			functionName =methodBase.Name;
			//[2] -- ADD -- END			
			try
			{
				if(!(UpdateTrace(PrepMsg(userID,  severityID, message, Level.ERROR.ToString(), className, functionName))))
				{
					ILogger altLogger = LoggerFactory.createAlternateLogger();
					return(altLogger.WriteToErrorLog (userID , severityID, className, functionName , message)); 
				}
				return(true);
			}
			catch(Exception ex)
			{
				ClassForProblemResolutionOnly obj = new ClassForProblemResolutionOnly();
				obj.WriteToEventLog(ex);
				return (false);
			}
		}

		/// <summary>
		/// Writes the warnings to the Log.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public override bool WriteToWarningLog	(string userID , Severity severityID, string className, string functionName , string message)
		{
			//[2] -- ADD -- START Getting the class name and function name
			StackTrace stackTrace = new StackTrace();
			StackFrame stackFrame = stackTrace.GetFrame(1);
			MethodBase methodBase = stackFrame.GetMethod();
			className=Convert.ToString(methodBase.DeclaringType);
			functionName =methodBase.Name;
			//[2] -- ADD -- END			
			try
			{
				if(!(UpdateTrace(PrepMsg(userID, severityID, message, Level.WARNING.ToString(), className, functionName))))
				{
					ILogger altLogger = LoggerFactory.createAlternateLogger();
					return(altLogger.WriteToWarningLog (userID , severityID, className, functionName , message)); 
				}
				return(true);
			}
			catch(Exception ex)
			{
				ClassForProblemResolutionOnly obj = new ClassForProblemResolutionOnly();
				obj.WriteToEventLog(ex);
				return (false);
			}
		}

	
		/// <summary>
		/// Updates The Trace Log.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		private bool UpdateTrace(string message)
		{
			try
			{
				HttpContext.Current.Trace.Write(message,"GoldwynLED");
				return(true);
			}
			catch(Exception ex)
			{
				ClassForProblemResolutionOnly obj = new ClassForProblemResolutionOnly();
				obj.WriteToEventLog(ex);
				return (false);
			}
		}

		
	}
}
