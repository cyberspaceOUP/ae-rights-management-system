/*
	*  Title: LOGGING.
	* Description: This file encapsulates the enumeration of Logger level.
	* Copyright: GoldwynLED
	* Company: VRVirtual.com Pvt. Ltd.
	* Dated: August-05-08
	* @filename Level.cs
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
	/// Level stores the default logging level for the application.
	///		All loggers have to know what logging level the application
	///		is currently defined at.
	/// </summary>
	public enum Level
	{
		INFO,
		WARNING,
		ERROR
	}

	/// <summary>
	/// 
	/// </summary>
	public enum Severity
	{
		AuthenticationFail,
		AuthorizationFail,
		Critical,
		High,
		Information,
		AccessAudit,
		ProcessingError
	}
}
