// --------------------------------------------------------------------------
// <copyright file="IMailService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface
{
    using System.Collections.Generic;
    using Fanex.MailDeliver.Model;

    /// <summary>
    /// Mail service interface.
    /// </summary>
    public interface IMailService
    {
        List<MailConfig> GetListConfig(string ip = "");

        IMailService WithApp(int appId);

        IMailService WithStatus(int status);

        IMailService FromDate(string fromDate);

        IMailService ToDate(string toDate);

        IMailService WithPagination(int page, int counter);

        List<MailRecord> GetListMail();

        List<MailRecord> GetListMailByDate(ref int totalRows);

        MailRecord GetMail(int mailId);

        MailSetting GetSetting(int settingId);

        MailConfig GetConfig(int configId);

        MailFunction GetFunction(int functionId);

        List<MailSetting> GetListSetting();

        List<MailFunction> GetListFunction();

        bool SendMail(long mailID);

        bool StoreSetting(MailSetting setting);

        bool StoreConfig(MailConfig config);

        bool StoreFunction(MailFunction func);

        List<MailError> GetError(int mailId);
    }
}