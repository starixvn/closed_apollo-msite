// --------------------------------------------------------------------------
// <copyright file="MailService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.MailDeliver.MailClient.API;
    using Fanex.MailDeliver.Model;
    using Fanex.Maintenance.Service.Interface;

    /// <summary>
    /// Mail service.
    /// </summary>
    public class MailService : IMailService
    {
        private int _appId;

        private string _fromDate;

        private string _toDate;

        private int _page;

        private int _counter;

        private MailStatus _mailStatus;

        private MailService()
        {
        }

        /// <summary>
        /// Create mail service.
        /// </summary>
        /// <returns>Mail service.</returns>
        public static MailService Create()
        {
            return new MailService();
        }

        /// <summary>
        /// Get list mail config filter by server IP.
        /// </summary>
        /// <param name="ip">IP address.</param>
        /// <returns>List of mail config.</returns>
        public List<MailConfig> GetListConfig(string ip = "")
        {
            var configs = new List<MailConfig>();

            using (var client = new MailDeliver())
            {
                var data = client.GetListConfig(ip);
                if (data != null)
                {
                    configs = data.OrderBy(x => x.AppName).ToList();
                }
            }

            return configs;
        }

        /// <summary>
        /// By application id.
        /// </summary>
        /// <param name="appId">Application ID.</param>
        /// <returns>Mail service.</returns>
        public IMailService WithApp(int appId)
        {
            this._appId = appId;

            return this;
        }

        /// <summary>
        /// Update from date.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <returns>Mail service.</returns>
        public IMailService FromDate(string fromDate)
        {
            this._fromDate = fromDate;

            return this;
        }

        /// <summary>
        /// Update to date.
        /// </summary>
        /// <param name="toDate">To date string.</param>
        /// <returns>Mail service.</returns>
        public IMailService ToDate(string toDate)
        {
            this._toDate = toDate;

            return this;
        }

        /// <summary>
        /// Pagination items.
        /// </summary>
        /// <param name="page">Page index.</param>
        /// <param name="counter">Counter value.</param>
        /// <returns>Mail service.</returns>
        public IMailService WithPagination(int page, int counter)
        {
            this._page = page;
            this._counter = counter;

            return this;
        }

        /// <summary>
        /// Get list mail.
        /// </summary>
        /// <returns>List of mail record.</returns>
        public List<MailRecord> GetListMail()
        {
            var mails = new List<MailRecord>();

            using (var client = new MailDeliver())
            {
                var dateTime = DateTime.Parse(_fromDate);
                var data = client.GetListMail(_appId, dateTime, _page, _counter);
                if (data != null)
                {
                    mails = data.ToList();
                }
            }

            return mails;
        }

        /// <summary>
        /// Get list mail from date to date.
        /// </summary>
        /// <param name="totalRows">Number of rows.</param>
        /// <returns>List of mail record.</returns>
        public List<MailRecord> GetListMailByDate(ref int totalRows)
        {
            var mails = new List<MailRecord>();

            using (var client = new MailDeliver())
            {
                var dateTime = DateTime.Parse(_fromDate);
                var dateTimeTo = DateTime.Parse(_toDate);
                var data = client.GetListMailByDate(_appId, dateTime, dateTimeTo, ref totalRows, _page, _counter, _mailStatus);

                if (data != null)
                {
                    mails = data.Select(mailRecord =>
                    {
                        mailRecord.MailTo = mailRecord.MailTo.Replace(";", "<br />");
                        mailRecord.MailBcc = mailRecord.MailBcc.Replace(";", "<br />");
                        mailRecord.MailCc = mailRecord.MailCc.Replace(";", "<br />");
                        return mailRecord;
                    }).ToList();
                }
            }

            return mails;
        }

        /// <summary>
        /// Get detail mail by mail id.
        /// </summary>
        /// <param name="mailID">Mail object ID.</param>
        /// <returns>Mail record.</returns>
        public MailRecord GetMail(int mailID)
        {
            MailRecord mail = null;

            using (var client = new MailDeliver())
            {
                mail = client.GetMail(mailID);
            }

            return mail;
        }

        /// <summary>
        /// Get list mail setting.
        /// </summary>
        /// <returns>List of mail setting.</returns>
        public List<MailSetting> GetListSetting()
        {
            var settings = new List<MailSetting>();

            using (var client = new MailDeliver())
            {
                var data = client.GetListSetting();
                if (data != null)
                {
                    settings = data.OrderBy(x => x.Server).ToList();
                }
            }

            return settings;
        }

        /// <summary>
        /// Get list mail function.
        /// </summary>
        /// <returns>List of mail function.</returns>
        public List<MailFunction> GetListFunction()
        {
            var functions = new List<MailFunction>();

            using (var client = new MailDeliver())
            {
                var data = client.GetListFuntion();
                if (data != null)
                {
                    functions = data.ToList();
                }
            }

            return functions;
        }

        /// <summary>
        /// ReSend mail.
        /// </summary>
        /// <param name="mailID">Mail object ID.</param>
        /// <returns>Send result.</returns>
        public bool SendMail(long mailID)
        {
            try
            {
                using (var client = new MailDeliver())
                {
                    client.ReSendMail(mailID);
                }

                return true;
            }
            catch
            {
            }

            return false;
        }

        /// <summary>
        /// Insert new mail setting.
        /// </summary>
        /// <param name="setting">Mail setting.</param>
        /// <returns>Success or not.</returns>
        public bool StoreSetting(MailSetting setting)
        {
            bool result = false;

            if (setting != null)
            {
                using (var client = new MailDeliver())
                {
                    client.StoreSetting(setting);
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Insert new mail config.
        /// </summary>
        /// <param name="config">Mail config.</param>
        /// <returns>Success or not.</returns>
        public bool StoreConfig(MailConfig config)
        {
            bool result = false;

            if (config != null)
            {
                using (var client = new MailDeliver())
                {
                    client.StoreConfig(config);
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Insert new mail function.
        /// </summary>
        /// <param name="func">Mail function.</param>
        /// <returns>Success or not.</returns>
        public bool StoreFunction(MailFunction func)
        {
            bool result = false;

            if (func != null)
            {
                using (var client = new MailDeliver())
                {
                    client.StoreFunction(func);
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Get mail setting by setting id.
        /// </summary>
        /// <param name="settingId">Setting ID.</param>
        /// <returns>Mail setting.</returns>
        public MailSetting GetSetting(int settingId)
        {
            MailSetting mail = null;

            using (var client = new MailDeliver())
            {
                mail = client.GetSetting(settingId);
            }

            return mail;
        }

        /// <summary>
        /// Get mail config by config id.
        /// </summary>
        /// <param name="configId">Configuration ID.</param>
        /// <returns>Mail config.</returns>
        public MailConfig GetConfig(int configId)
        {
            MailConfig mail = null;

            using (var client = new MailDeliver())
            {
                mail = client.GetConfig(configId);
            }

            return mail;
        }

        /// <summary>
        /// Get mail function by function id.
        /// </summary>
        /// <param name="functionId">Function ID.</param>
        /// <returns>Mail function.</returns>
        public MailFunction GetFunction(int functionId)
        {
            MailFunction mail = null;

            using (var client = new MailDeliver())
            {
                mail = client.GetFunction(functionId);
            }

            return mail;
        }

        /// <summary>
        /// By Status mail.
        /// </summary>
        /// <param name="status">Mail status.</param>
        /// <returns>Mail service.</returns>
        public IMailService WithStatus(int status)
        {
            if (status == 0)
            {
                this._mailStatus = MailStatus.All;
            }

            if (status == -1)
            {
                this._mailStatus = MailStatus.Fail;
            }

            if (status == 1)
            {
                this._mailStatus = MailStatus.Success;
            }

            return this;
        }

        /// <summary>
        /// Get error.
        /// </summary>
        /// <param name="mailId">Mail object ID.</param>
        /// <returns>List of mail error.</returns>
        public List<MailError> GetError(int mailId)
        {
            List<MailError> mailList = new List<MailError>();

            using (var client = new MailDeliver())
            {
                mailList = client.GetListError(mailId).ToList();
            }

            return mailList;
        }
    }
}