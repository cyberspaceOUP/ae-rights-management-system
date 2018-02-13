/*
	*  Title: LOGGING.
	* Description: This file encapsulates the Database logger class, used for logging the event in the Sql Server Database.
	* Copyright: GoldwynLED
	* Company: VRVirtual.com Pvt. Ltd.
	* Dated: August-05-08
	* @filename DBLogger.cs
	* @author <<a href="mailto:sanjeevk@vrvirtual.com">Sanjeev Kumar</a> > 
	* ****************************  Modification History  *********************************************
	* Sr No:		Date			Modified by	    Tracker No					Description
	* *************************************************************************************************
	***************************************************************************************************
*/

using System;
using System.Data;
using System.Data.SqlClient;  
using System.Data.SqlTypes; 
using System.Text; 
using System.Configuration; 
using System.Reflection;
using System.Diagnostics;

namespace Logger
{
	/// <summary>
	/// Logs events to the Database
	/// </summary>
	public class DBLogger: AbstractLogger
	{

		private static string ConnStr							;
		private const  string LOGGER_CONNSTR	=  "Logger_ConnStr";
		private SqlConnection	conn ;
		private SqlCommand		Command ;
		
		/// <summary>
		/// Loads the DBLogger Class in the App Domain.
		/// </summary>
		public DBLogger()
		{
			try
			{
				AppSettingsReader reader	= new AppSettingsReader ();
				ConnStr						= (string)reader.GetValue (LOGGER_CONNSTR, typeof(String));
			}
			catch(Exception ex)
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
		public override bool LogException(string userID ,Severity severityID,  string className, string functionName , Exception ex )
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
				if(PrepCmmdForLog(userID , Severity.ProcessingError,  className, functionName , PrepMsg(userID, ex)))
				{
					if(!(UpdateDB()))
					{
						ILogger altLogger = LoggerFactory.createAlternateLogger();
						return(altLogger.LogException(userID , severityID,  className, functionName , ex )); 
					}
					else
						return(true);
				}
				return (false);
			}
			catch(Exception exp)
			{
				ClassForProblemResolutionOnly obj = new ClassForProblemResolutionOnly();
				obj.WriteToEventLog(exp);
				return (false);
			}
		}

		/// <summary>
		/// Writes a message to the Log.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		/// <summary>
		/// Writes Informations, Warnings, Errors to the Log. 
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public override bool WriteToInfoLog	(string userID , Severity severityID,  string className, string functionName , string message)
		{
			
			try
			{
				if(PrepCmmdForLog(userID , severityID, className, functionName , PrepMsg(userID, severityID, message, State_Info)))
				{
					if(!(UpdateDB()))
					{
						ILogger altLogger = LoggerFactory.createAlternateLogger();
						return(altLogger.WriteToInfoLog(userID , severityID, className, functionName , message));
					}
					else
						return(true);
				}
				return (false);
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
				if(PrepCmmdForLog(userID , severityID, className, functionName , PrepMsg(userID, severityID, message, State_Eror)))
				{
					if(!(UpdateDB()))
					{
						ILogger altLogger = LoggerFactory.createAlternateLogger();
						return(altLogger.WriteToErrorLog(userID , severityID, className, functionName , message));
					}
					else
						return(true);
				}
				return (false);
			}
			catch(Exception ex)
			{
				ClassForProblemResolutionOnly obj = new ClassForProblemResolutionOnly();
				obj.WriteToEventLog(ex);
				return (false);
			}
		}

		/// <summary>
		/// Writes the Warnings and Errors to the Log.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public override bool WriteToWarningLog	(string userID , Severity severityID, string className, string functionName , string message)
		{
			try
			{
				if(PrepCmmdForLog(userID , severityID,  className, functionName , PrepMsg(userID, severityID, message, State_Warn)))
				{
					if(!(UpdateDB()))
					{
						ILogger altLogger = LoggerFactory.createAlternateLogger();
						return(altLogger.WriteToWarningLog(userID , severityID, className, functionName , message));
					}
					else
						return(true);
				}
				return (false);
			}
			catch(Exception ex)
			{
				ClassForProblemResolutionOnly obj = new ClassForProblemResolutionOnly();
				obj.WriteToEventLog(ex);
				return (false);
			}
		}

		/// <summary>
		/// Configures and Prepares the Command object for execution.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="severityID"></param>
		/// <param name="className"></param>
		/// <param name="functionName"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		private bool PrepCmmdForLog(string userID , Severity severityID, string className, string functionName , string message)
		{
			try
			{

				conn				= new SqlConnection(ConnStr);
				Command				= new SqlCommand();
				Command.Connection	= conn ;
				Command.CommandText = @"Insert into AuditLog	
						(UserName,SeverityID,Description,LogingModule,CreatedDate,ClassName,FunctionName) 
														values 
						(@UserName, @SeverityID,@Description,@LogingModule,GetDate(),@ClassName,@FunctionName)";
				Command.Parameters.Clear() ;
				Command.Parameters.Add(new SqlParameter("@UserName", userID));
				Command.Parameters.Add(new SqlParameter("@SeverityID",severityID));
				Command.Parameters.Add(new SqlParameter("@Description",message));
				Command.Parameters.Add(new SqlParameter("@LogingModule",userID));
				//Command.Parameters.Add(new SqlParameter("@CreatedDate",CreatedDate));
				Command.Parameters.Add(new SqlParameter("@ClassName",className));
				Command.Parameters.Add(new SqlParameter("@FunctionName",functionName));
				return(true);
			}
			catch
			{
				return(false);
			}
		}

		/// <summary>
		/// Updates the database.
		/// </summary>
		/// <returns></returns>
		private bool UpdateDB()
		{
			try
			{
				conn.Open();
				return(Command.ExecuteNonQuery() > 0);
			}
			catch(Exception ex)
			{
				ClassForProblemResolutionOnly obj = new ClassForProblemResolutionOnly();
				obj.WriteToEventLog(ex);
				return (false);
			}
			finally
			{
				try
				{
					if	((conn.State == System.Data.ConnectionState.Open)    
						||
						(conn.State == System.Data.ConnectionState.Broken))
					{
						conn.Close(); 
					}
				}
				catch
				{
				}
			}
		}
	}
}
