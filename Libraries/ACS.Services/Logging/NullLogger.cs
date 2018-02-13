using System;
using System.Collections.Generic;
using ACS.Core;
//using ACS.Core.Domain.Customers;
using ACS.Core.Domain.Logging;
using ACS.Core.Data;
using ACS.Data;
using ACS.Core.Domain.Common;

namespace ACS.Services.Logging
{
    /// <summary>
    /// Null logger
    /// </summary>
    public partial class NullLogger : ILogger
    {

        #region Fields

        private readonly IRepository<Log> _logRepository;
        private readonly IWebHelper _webHelper;
        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;
        private readonly CommonSettings _commonSettings;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logRepository">Log repository</param>
        /// <param name="webHelper">Web helper</param>
        /// <param name="dbContext">DB context</param>
        /// <param name="dataProvider">WeData provider</param>
        /// <param name="commonSettings">Common settings</param>
        public NullLogger(
            IRepository<Log> logRepository,
            IWebHelper webHelper,
            IDbContext dbContext,
            IDataProvider dataProvider,
            CommonSettings commonSettings)
        {
            this._logRepository = logRepository;
            this._webHelper = webHelper;
            this._dbContext = dbContext;
            this._dataProvider = dataProvider;
            this._commonSettings = commonSettings;
        }

        #endregion
        /// <summary>
        /// Determines whether a log level is enabled
        /// </summary>
        /// <param name="level">Log level</param>
        /// <returns>Result</returns>
        public virtual bool IsEnabled(LogLevel level)
        {
            return false;
        }

        /// <summary>
        /// Deletes a log item
        /// </summary>
        /// <param name="log">Log item</param>
        public virtual void DeleteLog(Log log)
        {
        }

        /// <summary>
        /// Clears a log
        /// </summary>
        public virtual void ClearLog()
        {
        }

        ///// <summary>
        ///// Gets all log items
        ///// </summary>
        ///// <param name="fromUtc">Log item creation from; null to load all records</param>
        ///// <param name="toUtc">Log item creation to; null to load all records</param>
        ///// <param name="message">Message</param>
        ///// <param name="logLevel">Log level; null to load all records</param>
        ///// <param name="pageIndex">Page index</param>
        ///// <param name="pageSize">Page size</param>
        ///// <returns>Log item collection</returns>
        //public virtual IPagedList<Log> GetAllLogs(DateTime? fromUtc, DateTime? toUtc,
        //    string message, LogLevel? logLevel, int pageIndex, int pageSize)
        //{
        //    return new PagedList<Log>(new List<Log>(), pageIndex, pageSize);
        //}

        /// <summary>
        /// Gets a log item
        /// </summary>
        /// <param name="logId">Log item identifier</param>
        /// <returns>Log item</returns>
        public virtual Log GetLogById(int logId)
        {
            return null;
        }

        /// <summary>
        /// Get log items by identifiers
        /// </summary>
        /// <param name="logIds">Log item identifiers</param>
        /// <returns>Log items</returns>
        public virtual IList<Log> GetLogByIds(int[] logIds)
        {
            return new List<Log>();
        }

        /// <summary>
        /// Inserts a log item
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="shortMessage">The short message</param>
        /// <param name="fullMessage">The full message</param>
        /// <param name="customer">The customer to associate log record with</param>
        /// <returns>A log item</returns>
        /// <summary>
        /// Inserts a log item
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="shortMessage">The short message</param>
        /// <param name="fullMessage">The full message</param>
        /// <param name="customer">The customer to associate log record with</param>
        /// <returns>A log item</returns>
        public virtual Log InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "")
        {
            var log = new Log()
            {
                LogLevel = logLevel,
                ShortMessage = shortMessage,
                FullMessage = fullMessage,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                PageUrl = _webHelper.GetThisPageUrl(true),
                ReferrerUrl = _webHelper.GetUrlReferrer(),
                CreatedOnUtc = DateTime.Now,
                EntryDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            _logRepository.Insert(log);

            return log;
        }
    }
}
