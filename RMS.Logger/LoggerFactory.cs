/*
	*  Title: LOGGING.
	* Description: This file encapsulates the functionality of instantiating the required type of Event Logging class.
	* Copyright: GoldwynLED
	* Company: VRVirtual.com Pvt. Ltd.
	* Dated: August-05-08
	* @filename LoggerFactory.cs
	* @author <<a href="mailto:sanjeevk@vrvirtual.com">Sanjeev Kumar</a> > 
	* ****************************  Modification History  *********************************************
	* Sr No:		Date			Modified by	    Tracker No					Description
	* *************************************************************************************************
	***************************************************************************************************
*/

using System;
using System.Configuration; 

namespace Logger
{
	/// <summary>
	/// Summary description for LogFactory.
	/// </summary>
	public class LoggerFactory
	{
		private const string	LOGGER_TYPE			= "Logger_Destination";
		private const string	LOGGER_ALT_TYPE		= "Logger_ALT_Destination";
		private const string	LOGGER_DBLOGGER		= "DB";
		private const string	LOGGER_EVENTLOGGER	= "EV";
		private const string	LOGGER_TRACELOGGER	= "TL";
		private const string	LOGGER_FILELOGGER	= "FS";
		//lOGGER TYPES TO BE USED FOR COMPARISONS.
		private const string	TYPE_DBLOGGER		= "Logger.DBLogger";
		private const string	TYPE_EVENTLOGGER	= "Logger.EventLogger";
		private const string	TYPE_TRACELOGGER	= "Logger.TraceLogger";
		private const string	TYPE_FILELOGGER		= "Logger.FileLogger";
		private static string	logDestination;
		private static string	altLogDestination;

		/// <summary>
		///  Static object to create only  one instance of logger through out the application.
		/// </summary>
		private static ILogger logger=null;

		/// <summary>
		/// The Constructor for the Logger Factory class.
		/// </summary>
		static LoggerFactory()
		{
			AppSettingsReader reader	=	new AppSettingsReader ();
			//Retrieve the Logging type from the config file.
			logDestination				=	(string)reader.GetValue (LOGGER_TYPE, typeof(String));
			altLogDestination			=	(string)reader.GetValue (LOGGER_ALT_TYPE, typeof(String));
		}

		/// <summary>
		/// Returns a new logger of the specified type
		/// </summary>
		/// <returns></returns>
		public static ILogger getLogger ()
		{
			//The various values for logDestination variable are {DB,EV,TL,FS}
			if (logger==null)
			{
				logger		=	createLogger(logDestination);
			}
			else
			{
				//Check if the existing Logger is the type of the one set in the web.config
				switch(logger.GetType().ToString())
				{
					case (TYPE_DBLOGGER):
						if(!(logDestination == LOGGER_DBLOGGER))
							return(createLogger(logDestination));
						else
							return(logger);
					case (TYPE_EVENTLOGGER):
						if(!(logDestination == LOGGER_EVENTLOGGER))
							return(createLogger(logDestination));
						else
							return(logger);
					case (TYPE_TRACELOGGER):
						if(!(logDestination == LOGGER_TRACELOGGER))
							return(createLogger(logDestination));
						else
							return(logger);
					case (TYPE_FILELOGGER):
						if(!(logDestination == LOGGER_FILELOGGER))
							return(createLogger(logDestination));
						else
							return(logger);
					default:
						return(createLogger(logDestination));
				}
			}
			return logger;
		}

		/// <summary>
		/// Returns a new logger of the specified type
		/// </summary>
		/// <param name="loggerType"></param>
		/// <returns></returns>
		private static ILogger createLogger (string loggerType)
		{//{DB,EV,TL,FS}
			//Retrieve the Logging type from the config file.
			ILogger logger;
			switch(loggerType)
			{
				case LOGGER_DBLOGGER:
					logger = new Logger.DBLogger ();
					return (logger);
				case LOGGER_EVENTLOGGER:
					logger = new Logger.EventLogger ();
					return (logger);
				case LOGGER_TRACELOGGER:
					logger = new Logger.TraceLogger ();
					return (logger);
				case LOGGER_FILELOGGER:
					logger = new Logger.FileLogger ();
					return (logger);
				default:
					logger = new Logger.FileLogger ();
					return (logger);
			}
		}
		/// <summary>
		/// Creates a Alternate logger destination  class of  in the event of first ones 
		///	</summary>
		public static ILogger createAlternateLogger ()
		{
			// The Probable values of altLogDestination are {DB,EV,TL,FS}
			return createLogger(altLogDestination);
		}
		/// <summary>
		/// Creates a Alternate logger destination  assign it to the static logger object 
		///	</summary>
		public static void enableAlternateLogger ()
		{//{DB,EV,TL,FS}
			logger	=	createAlternateLogger();
		}
		
	}
}
