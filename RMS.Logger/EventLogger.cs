/*
	*  Title: LOGGING.
	* Description: This file encapsulates the Event Viewer logger class, used for logging the event in the Server Event Viewer Metabase.
	* Copyright: GoldwynLED
	* Company: VRVirtual.com Pvt. Ltd.
	* Dated: August-05-08
	* @filename EventViewerLogger.cs
	* @author <<a href="mailto:sanjeevk@vrvirtual.com">Sanjeev Kumar</a> > 
	* ****************************  Modification History  *********************************************
	* Sr No:		Date			Modified by	    Tracker No					Description
	* *************************************************************************************************
	***************************************************************************************************
*/


using System;
using System.Configuration; 
using System.Diagnostics;
using System.Text; 
using System.Reflection;

namespace Logger
{
	/// <summary>
	/// Logs events to the System Event Log
	/// </summary>
	public class EventLogger : AbstractLogger 
	{

		//private static string ApplicationLogLevel	;
		private static string EventSrc				;
		private static string EventMachine			;
		private static string EventLogName			;
		private const  string Src			=	"Logger_EventSrc";
		private const  string Machine		=	"Logger_EventMachine";
		private const  string LogName		=	"Logger_EventLogName";


		/// <summary>
		/// Loads this class in the App Domain.
		/// </summary>
		public EventLogger()
		{
			try
			{
				//Getting all the Details from the Configurations.
				AppSettingsReader reader = new AppSettingsReader ();
				EventSrc			= (string)reader.GetValue (Src,				typeof(string));
				EventMachine		= (string)reader.GetValue (Machine,			typeof(string));
				EventLogName		= (string)reader.GetValue (LogName,			typeof(string));
			}
			catch(Exception ex)
			{
				ClassForProblemResolutionOnly obj = new ClassForProblemResolutionOnly();
				obj.WriteToEventLog(ex);
			}
		}

		/// <summary>
		/// Loga the details of an Exception into the Event Log.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <param name="message"></param>
		/// <param name="ex"></param>
		/// <returns></returns>
		public override bool LogException(string userID ,Severity severityID, string className, string functionName , Exception ex )
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
				if(CreateEventSource())
					if(UpdateLog(userID, Severity.ProcessingError, PrepMsg(userID, ex), State_Eror, EventLogEntryType.Information))
						return(true);
				ILogger altLogger = LoggerFactory.createAlternateLogger();
				return(altLogger.LogException(userID , severityID, className, functionName , ex ));
			}
			catch(Exception exp)
			{
				ClassForProblemResolutionOnly obj = new ClassForProblemResolutionOnly();
				obj.WriteToEventLog(exp);
				return (false);
			}
		}

		/// <summary>
		/// Writes Informations, Warnings, Errors to the Log.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public override bool WriteToInfoLog		(string userID , Severity severityID, string className, string functionName , string message)
		{
			try
			{
				if(CreateEventSource())
					if(UpdateLog(userID, severityID, PrepMsg(userID, severityID, message, State_Info, className, functionName), State_Info, EventLogEntryType.Information))
						return(true);
				ILogger altLogger = LoggerFactory.createAlternateLogger();
				return(altLogger.WriteToInfoLog(userID , severityID, className, functionName , message));
			}
			catch(Exception ex)
			{
				ClassForProblemResolutionOnly obj = new ClassForProblemResolutionOnly();
				obj.WriteToEventLog(ex);
				return (false);
			}
		}

		/// <summary>
		/// Writes Error Information to the Log.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public override bool WriteToErrorLog	(string userID , Severity severityID,  string className, string functionName , string message)
		{
	
			try
			{
				if(CreateEventSource())
					if(UpdateLog(userID, severityID, PrepMsg(userID, severityID, message, State_Eror, className, functionName), State_Eror, EventLogEntryType.Error))
						return(true);
				ILogger altLogger = LoggerFactory.createAlternateLogger();
				return(altLogger.WriteToErrorLog(userID , severityID, className, functionName , message));
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
		public override bool WriteToWarningLog	(string userID , Severity severityID,  string className, string functionName , string message)
		{
			try
			{
				if(CreateEventSource())
					if(UpdateLog(userID, severityID, PrepMsg(userID, severityID, message, State_Warn, className, functionName), State_Warn, EventLogEntryType.Warning))
						return(true);
				ILogger altLogger = LoggerFactory.createAlternateLogger();
				return(altLogger.WriteToWarningLog(userID , severityID,  className, functionName , message));
			}
			catch(Exception ex)
			{
				ClassForProblemResolutionOnly obj = new ClassForProblemResolutionOnly();
				obj.WriteToEventLog(ex);
				return (false);
			}
		}

		/// <summary>
		/// Creates the Event Source, if not already created.
		/// </summary>
		/// <returns></returns>
		private bool CreateEventSource()
		{
			try
			{
				if(EventLog.Exists(EventLogName,EventMachine))
					if(EventLog.SourceExists(EventSrc,EventMachine))  
						return(true);
					else
					{
                        EventSourceCreationData mySourceData = new EventSourceCreationData(EventSrc, EventLogName);
                        mySourceData.MachineName = EventMachine;
                        EventLog.CreateEventSource(mySourceData);
						return(true);
					}
				return(false);
			}
			catch(Exception ex)
			{
				ClassForProblemResolutionOnly obj = new ClassForProblemResolutionOnly();
				obj.WriteToEventLog(ex);
				return (false);
			}
		}

		/// <summary>
		/// Updates the Event Log.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="message"></param>
		/// <param name="level"></param>
		/// <param name="entryType"></param>
		/// <returns></returns>
		private bool UpdateLog(string userID, Severity severityID, string message, string level,EventLogEntryType entryType)
		{
			try
			{
				EventLog.WriteEntry (EventSrc,message,entryType);    
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
