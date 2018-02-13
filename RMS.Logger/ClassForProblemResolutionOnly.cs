using System;
using System.Configuration; 
using System.Diagnostics;
using System.Text; 

namespace Logger
{
	/// <summary>
	/// Summary description for ClassForProblemResolutionOnly.
	/// </summary>
	public class ClassForProblemResolutionOnly
	{
		private static string EventSrc				;
		private static string EventMachine			;
		private static string EventLogName			;
		private const  string Src			=	"Logger_EventSrc";
		private const  string Machine		=	"Logger_EventMachine";
		private const  string LogName		=	"Logger_EventLogName";

		public ClassForProblemResolutionOnly()
		{
			try
			{
				//Getting all the Details from the Configurations.
				AppSettingsReader reader = new AppSettingsReader ();
				EventSrc			= (string)reader.GetValue (Src,				typeof(string));
				EventMachine		= (string)reader.GetValue (Machine,			typeof(string));
				EventLogName		= (string)reader.GetValue (LogName,			typeof(string));

				if(EventLog.Exists(EventLogName,EventMachine))
					if(EventLog.SourceExists(EventSrc,EventMachine))  
					{
					}
					else
					{
                        EventSourceCreationData mySourceData = new EventSourceCreationData(EventSrc, EventLogName);
                        mySourceData.MachineName = EventMachine;
                        EventLog.CreateEventSource(mySourceData);
					}
			}
			catch
			{
			}
		}


		public bool WriteToEventLog(Exception e)
		{
			try
			{

				StringBuilder strBldr = new StringBuilder(); 
				strBldr.Append("MESSAGE      :");
				strBldr.Append(e.Message);
				strBldr.Append("\n");
				strBldr.Append("STACK TRACE  :");
				strBldr.Append(e.StackTrace);
				strBldr.Append("\n");
				strBldr.Append("SOURCE       :");
				strBldr.Append(e.Source);
				strBldr.Append("\n");
				strBldr.Append("--------------------------------------------------");
				strBldr.Append("\n");
				strBldr.Append("\n");
				if(e.InnerException == null) 
				{
				}
				else
				{
					strBldr.Append("INNER EXCEPTION MESSAGE       :");
					strBldr.Append(e.InnerException.Message);
					strBldr.Append("\n");
					strBldr.Append("INNER EXCEPTION STACK TRACE   :");
					strBldr.Append(e.InnerException.StackTrace);
					strBldr.Append("\n");
					strBldr.Append("INNER EXCEPTION SOURCE        :");
					strBldr.Append(e.InnerException.Source);
					strBldr.Append("\n");
					strBldr.Append("--------------------------------------------------");
					strBldr.Append("\n");
					strBldr.Append("\n");
					strBldr.Append("\n");
				}
				EventLog.WriteEntry (EventSrc,strBldr.ToString(),EventLogEntryType.Error);
				return(true);
			}
			catch
			{
				return(false);
			}
		}
	}
}
