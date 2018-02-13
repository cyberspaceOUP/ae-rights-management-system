/*
	*  Title: LOGGING.
	* Description: This file encapsulates the File logger class, used for logging the event in a flat file.
	* Copyright: GoldwynLED
	* Company: VRVirtual.com Pvt. Ltd.
	* Dated: August-05-08
	* @filename FileLogger.cs
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
using System.Globalization; 
using System.Threading;
using System.Reflection;
using System.Diagnostics;

namespace Logger
{
	/// <summary>
	/// Logs events to the Application Trace
	/// </summary>
	public class FileLogger : AbstractLogger
	{
        //private static string ApplicationLogLevel	;
        private static string LoggerFile;
        private const string LOGGER_PATH = "Logger_PathName";
        private const string LOGGER_FILE = "Logger_FileName";
        static ReaderWriterLock rwl = new ReaderWriterLock();
        private static int _dayofYear;

        /// <summary>
        /// Loads the Class into the App Domain.
        /// </summary>
        public FileLogger()
        {
            try
            {
                AppSettingsReader reader = new AppSettingsReader();
                StringBuilder builder = new StringBuilder();
                LoggerFile = System.Web.HttpContext.Current.Server.MapPath((string)reader.GetValue(LOGGER_PATH, typeof(string))) + (string)reader.GetValue(LOGGER_FILE, typeof(string));
                builder.Append(LoggerFile);
                builder.Append(DateTime.Now.ToString("MM_dd_yyyy", DateTimeFormatInfo.InvariantInfo));
                builder.Append(".txt");
                _dayofYear = DateTime.Now.DayOfYear;
                LoggerFile = builder.ToString();
            }
            catch (Exception ex)
            {
                ClassForProblemResolutionOnly obj = new ClassForProblemResolutionOnly();
                obj.WriteToEventLog(ex);
            }
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
		public override bool LogException	(string userID , Severity severityID, string className, string functionName , Exception ex )
		{
			try
			{
				if(!(UpdateFile(PrepMsg( userID, ex))))
				{
					ILogger altLogger = LoggerFactory.createAlternateLogger();
					return(altLogger.LogException(userID , severityID, className, functionName , ex));  
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
				if(!(UpdateFile(PrepMsg( userID, severityID,  message, Level.INFO.ToString(), className, functionName))))
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
		/// Writes Error Information to the Log.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public override bool WriteToErrorLog	(string userID , Severity severityID, string className, string functionName , string message)
		{
			try
			{
				if(!(UpdateFile(PrepMsg( userID, severityID,  message, Level.ERROR.ToString(), className, functionName))))
				{
					ILogger altLogger = LoggerFactory.createAlternateLogger();
					return(altLogger.WriteToErrorLog(userID , severityID, className, functionName , message)); 
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
		public override bool WriteToWarningLog	(string userID , Severity severityID,  string className, string functionName , string message)
		{
			try
			{
				if(!(UpdateFile(PrepMsg( userID, severityID,  message, Level.WARNING.ToString(), className, functionName))))
				{
					ILogger altLogger = LoggerFactory.createAlternateLogger();
					return(altLogger.WriteToWarningLog(userID , severityID, className, functionName , message));
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
		/// Writes a custom message to the Log.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		private void AdjustLoggerFileNamewithDate()
		{
			try
			{
				int dayofYear;
				dayofYear = DateTime.Now.DayOfYear;
				if (_dayofYear != dayofYear)
				{
					AppSettingsReader reader = new AppSettingsReader ();
					StringBuilder builder = new StringBuilder(); 
					LoggerFile	= (string)reader.GetValue (LOGGER_FILE,		typeof(string));
					builder.Append(LoggerFile);
					builder.Append(DateTime.Now.ToString("MM_dd_yyyy",DateTimeFormatInfo.InvariantInfo));
                    builder.Append(".txt");
					LoggerFile = builder.ToString(); 
					_dayofYear = dayofYear;
				}
			}
			catch(Exception ex)
			{
				ClassForProblemResolutionOnly obj = new ClassForProblemResolutionOnly();
				obj.WriteToEventLog(ex);
			}

		}
		/// <summary>
		/// Updates the File with the message.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		private bool UpdateFile(string message)
		{
			FileStream			fileStream		;
			StreamWriter		streamWriter	;
			try
			{
			lock(this)//Afzal
				{
				rwl.AcquireWriterLock(Timeout.Infinite);
				/*10/11/04: Day change should create new log file */
				AdjustLoggerFileNamewithDate();

				if(File.Exists(LoggerFile))
				{
					streamWriter = File.AppendText(LoggerFile);
				}
				else
				{
					fileStream		=	File.Open(LoggerFile,FileMode.OpenOrCreate,FileAccess.Write);
					streamWriter	=	new StreamWriter(fileStream);
				}
				streamWriter.Write(message);  
				streamWriter.Close(); 
				}
				return(true);
			}
			catch(Exception ex)
			{
				ClassForProblemResolutionOnly obj = new ClassForProblemResolutionOnly();
				obj.WriteToEventLog(ex);
				return (false);
			}
			finally
			{
				rwl.ReleaseWriterLock();
				fileStream		=	null;
				streamWriter	=	null;
			}
		}
	}
}
