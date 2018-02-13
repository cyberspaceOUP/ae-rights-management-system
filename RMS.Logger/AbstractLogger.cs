
/*
	* Title: LOGGING.
	* Description: This file encapsulates the Abstract logger class.
	* Copyright: GoldwynLED
	* Company: VRVirtual.com Pvt. Ltd.
	* Dated: Aug-04-08
	* @filename AbstractLogger.cs
	* @author <<a href="mailto:sanjeevk@vrvirtual.com">Sanjeev Kumar</a> > 
	* ****************************  Modification History  *********************************************
	* Sr No:		Date			Modified by	    Tracker No					Description
	* *************************************************************************************************
	***************************************************************************************************
*/


using System;
using System.Text;
using System.Configuration; 
using System.Globalization; 
using System.Diagnostics;
using System.Reflection;

namespace Logger
{

	/// <summary>
	/// The Base Abstract Class class for all types of Audit Loggers.
	/// </summary>
	public abstract class AbstractLogger : ILogger
	{	

		protected static string Msg_Seprator		=	"|";
		protected const  string State_Info			=	"INFO";
		protected const  string State_Warn			=	"WARNING";
		protected const  string State_Eror			=	"ERROR";
		protected const  string State_None			=	"NO";
		protected const  string LOGGER_LEVEL		=	"Logger_Level";
		protected const  string DEFAULT				=	"DEFAULT";
		protected const  int	DEFAULT_SEVERITY	=	5;
		protected static string ApplicationLogLevel	;
		protected static string isLoggingOn			=	"true"	;
		private delegate bool LogExceptionDelegate(Exception ex);
		private delegate bool WriteToLogDelegate		(string userId ,Severity severityId, string className, string functionName , string message, Level level);
		private delegate bool WriteToInfoLogDelegate		(string message);
		private delegate bool WriteToWarningLogDelegate		(string message);
		private delegate bool WriteToErrorLogDelegate		(string message);


		/// <summary>
		/// The Abstract Logger Constructor.
		/// </summary>
		static	AbstractLogger()	
		{
			AppSettingsReader reader		=	new AppSettingsReader ();
			ApplicationLogLevel				=	(string)reader.GetValue (LOGGER_LEVEL,	typeof(string));
			isLoggingOn						=	(string)reader.GetValue ("isLoggingOn",	typeof(string));
		}


		/// <summary>
		/// To Check whether to be logged or not
		/// </summary>
		/// <param name="level"></param>
		/// <returns></returns>
		protected bool isLoggingToBeDone(Level level)
		{
			bool isLog	=	false;
			if (isLoggingOn.Equals("false"))
				return false;
			switch(level.ToString())
			{
				case State_Info:
					if(ApplicationLogLevel == State_Info)
						isLog= true;
					break;
				case State_Warn:
					if((ApplicationLogLevel == State_Info) || (ApplicationLogLevel == State_Warn))
						isLog= true;
					break;
				case State_Eror:
					if((ApplicationLogLevel == State_Info) || (ApplicationLogLevel == State_Warn) || (ApplicationLogLevel == State_Eror))
						isLog= true;
					break;
			}
			return isLog;
		}

		/// <summary>
		/// Logs an Exception details.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <param name="message"></param>
		/// <param name="ex"></param>
		/// <returns></returns>
		public abstract bool LogException		(string userID , Severity severityID,  string className, string functionName , Exception ex );
		/// <summary>
		/// Logs an Exception.
		/// </summary>
		/// <param name="ex"></param>
		/// <returns></returns>
		public bool LogException(Exception ex)
		{

			LogExceptionDelegate logExDelegate = new LogExceptionDelegate(LogExceptionInternal);
			IAsyncResult ar= logExDelegate.BeginInvoke(ex,null,null);
			return true;

		}

		public bool LogExceptionInternal(Exception ex)
		{
			if (isLoggingToBeDone(Level.ERROR))
				return LogException(DEFAULT,Severity.ProcessingError,DEFAULT,DEFAULT,ex);
			else
				return false;
		}


		/// <summary>
		/// Writes a message to the Log.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		//protected abstract bool writeToLog(string message);

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
		public bool WriteToLog		(string userID ,Severity severityID, string className, string functionName , string message, Level level)
		{
			//[2] -- ADD -- START Getting the class name and function name
			StackTrace stackTrace = new StackTrace();
			StackFrame stackFrame = stackTrace.GetFrame(1);
			MethodBase methodBase = stackFrame.GetMethod();
			className=Convert.ToString(methodBase.DeclaringType);
			functionName =methodBase.Name;

			WriteToLogDelegate logDelegate = new WriteToLogDelegate(WriteToLogInternal);
			IAsyncResult ar= logDelegate.BeginInvoke(userID ,severityID,  className, functionName , message,level,null,null);
			return true;
		}

		public bool WriteToLogInternal		(string userID ,Severity severityID,  string className, string functionName , string message, Level level)
		{
			//[2] -- ADD -- START Getting the class name and function name
			/*StackTrace stackTrace = new StackTrace();
			StackFrame stackFrame = stackTrace.GetFrame(1);
			MethodBase methodBase = stackFrame.GetMethod();
			className=Convert.ToString(methodBase.DeclaringType);
			functionName =methodBase.Name;*/
			//[2] -- ADD -- END
			if(!isLoggingToBeDone(level))
				return false;
			switch(level.ToString())
			{
				case State_Info:
					return	(WriteToInfoLog		(userID ,severityID,  className, functionName , message));
					
				case State_Warn:
					return	(WriteToWarningLog	(userID , severityID, className, functionName , message));
					
				case State_Eror:
					return	(WriteToErrorLog	(userID , severityID, className, functionName , message));
			}
			return (false);
		}


		/// <summary>
		/// Writes a message and its level to the log.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="level"></param>
		/// <returns></returns>
		public bool WriteToLog		(string message, Level level)
		{
			return (WriteToLog(DEFAULT,Severity.Information,DEFAULT,DEFAULT,message,level));
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
		public abstract bool WriteToInfoLog	(string userID , Severity severityID, string className, string functionName , string message);
		/// <summary>
		///  Writes Informations, Warnings, Errors to the Log.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public bool WriteToInfoLog		(string message)
		{
//			return (WriteToInfoLog(DEFAULT,DEFAULT_SEVERITY,DEFAULT,DEFAULT,DEFAULT,message));
			WriteToInfoLogDelegate logInfoDelegate = new WriteToInfoLogDelegate(WriteToInfoLogInternal);
			IAsyncResult ar= logInfoDelegate.BeginInvoke(message,null,null);
			return true;
		}
		public bool WriteToInfoLogInternal		(string message)
		{
			return (WriteToInfoLog(DEFAULT,Severity.Information,DEFAULT,DEFAULT,message));
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
		public abstract bool WriteToErrorLog	(string userID , Severity severityID, string className, string functionName , string message);
		/// <summary>
		/// Writes an Errors to the Log.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public bool WriteToErrorLog	(string message)
		{
//			return (WriteToErrorLog(DEFAULT,DEFAULT_SEVERITY,DEFAULT,DEFAULT,DEFAULT,message));
			WriteToErrorLogDelegate logErrDelegate = new WriteToErrorLogDelegate(WriteToErrorLogInternal);
			IAsyncResult ar= logErrDelegate.BeginInvoke(message,null,null);
			return true;
		}
		public bool WriteToErrorLogInternal	(string message)
		{
			return (WriteToErrorLog(DEFAULT,Severity.Information,DEFAULT,DEFAULT,message));
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
		public abstract bool WriteToWarningLog	(string userID , Severity severityID,  string className, string functionName , string message);
		/// <summary>
		/// Writes Warnings, Errors to the Log.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public bool WriteToWarningLog	(string message)
		{
			WriteToWarningLogDelegate logWarningDelegate = new WriteToWarningLogDelegate(WriteToWarningLogInternal);
			IAsyncResult ar= logWarningDelegate.BeginInvoke(message,null,null);
			return true;
		}

		public bool WriteToWarningLogInternal	(string message)
		{
			return (WriteToWarningLog(DEFAULT,Severity.Information,DEFAULT,DEFAULT,message));
		}		
		/// <summary>
		/// Prepares the Message text.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="message"></param>
		/// <param name="level"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <returns></returns>
		protected string PrepMsg(string userID, Severity severityID, string message, string level, string className, string functionName)
		{
			StringBuilder builder = new StringBuilder(); 
			builder.Append("\r\n");
			builder.Append(DateTime.Now.ToString("u",DateTimeFormatInfo.InvariantInfo));
			builder.Append(Msg_Seprator);
			builder.Append("User -");
			builder.Append(userID);
			builder.Append(Msg_Seprator);
			builder.Append("Level -");
			builder.Append(level);
			builder.Append(Msg_Seprator);
			builder.Append("Severity -");
			builder.Append(severityID);
			builder.Append(Msg_Seprator);
			builder.Append("Class -");
			builder.Append(className);
			builder.Append(Msg_Seprator);
			builder.Append("Function -");
			builder.Append(functionName);
			builder.Append(Msg_Seprator);
			builder.Append(message);
			if(severityID == Severity.ProcessingError)
			{
				builder.Append(Msg_Seprator);
				StackTrace st = new StackTrace();
				builder.Append(st.ToString());
			}
			builder.Append("\r\n");
			return(builder.ToString());
		} 

		/// <summary>
		/// Prepares the Message Text.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="message"></param>
		/// <param name="level"></param>
		/// <returns></returns>
		protected string PrepMsg(string userID, Severity severityID,string message, string level)
		{
			StringBuilder builder = new StringBuilder(); 
			builder.Append("\r\n");
			builder.Append(DateTime.Now.ToString("u",DateTimeFormatInfo.InvariantInfo));
			builder.Append(Msg_Seprator);
			builder.Append("User -");
			builder.Append(userID);
			builder.Append(Msg_Seprator);
			builder.Append("Level -");
			builder.Append(level);
			builder.Append(Msg_Seprator);
			builder.Append("Severity -");
			builder.Append(severityID);
			builder.Append(Msg_Seprator);
			builder.Append(message);
			if(severityID == Severity.ProcessingError)
			{
				builder.Append(Msg_Seprator);
				StackTrace st = new StackTrace();
				builder.Append(st.ToString());
			}
			builder.Append("\r\n");
			return(builder.ToString());
		}

		/// <summary>
		/// Prepares the Message Text for the Exception.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="ex"></param>
		/// <returns></returns>
		protected string PrepMsg(string userID, Exception ex)
		{
			StringBuilder builder = new StringBuilder(); 
			builder.Append("\r\n");
			builder.Append(DateTime.Now.ToString("u",DateTimeFormatInfo.InvariantInfo));
			builder.Append(Msg_Seprator);
			builder.Append("User -");
			builder.Append(userID);
			builder.Append(Msg_Seprator);
			builder.Append("Source -");
			builder.Append(ex.Source);
			builder.Append(Msg_Seprator);
			builder.Append("Message -");
			builder.Append(ex.Message);
			builder.Append(Msg_Seprator);
			builder.Append("StackTrace -");
			builder.Append(ex.StackTrace);
			builder.Append("\r\n");
			return(builder.ToString());
		}

		/// <summary>
		/// Prepares the message from the exception.
		/// </summary>
		/// <param name="ex"></param>
		/// <returns></returns>
		protected string PrepMsg(Exception ex)
		{
			StringBuilder builder = new StringBuilder(); 
			builder.Append("\r\n\r\n");
			builder.Append(DateTime.Now.ToString("u",DateTimeFormatInfo.InvariantInfo));
			builder.Append(Msg_Seprator);
			builder.Append("Message -");
			builder.Append(ex.Message);
			builder.Append(Msg_Seprator);
			builder.Append("StackTrace -");
			builder.Append(ex.StackTrace);
			builder.Append("\r\n");
			return(builder.ToString());
		}
		
	}
}
