// --------------------------------------------------------------------------
// <copyright file="MailFunctionViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.12.6">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.MailService
{
    using System.ComponentModel.DataAnnotations;
    using Fanex.MailDeliver.Model;
    using Fanex.Maintenance.Core;

    /// <summary>
    /// Mail function view model.
    /// </summary>
    public class MailFunctionViewModel
    {
        public MailFunctionViewModel()
        {
        }

        public MailFunctionViewModel(MailFunction mailFunction)
        {
            this.ConfigId = mailFunction.Config.AppID;
            this.SettingId = mailFunction.Setting.SettingID;
            this.FunctionId = mailFunction.FuncID;
            this.FunctionName = mailFunction.FuncName;
        }

        public int FunctionId { get; set; }

        [Required]
        [Display(Name = "Setting", ResourceType = typeof(ViewResource))]
        public int SettingId { get; set; }

        [Required]
        [Display(Name = "Config", ResourceType = typeof(ViewResource))]
        public int ConfigId { get; set; }

        [Required]
        [Display(Name = "Function_Name", ResourceType = typeof(ViewResource))]
        [StringLength(50, MinimumLength = 2,
            ErrorMessageResourceName = "InputLengthValidationMsg", ErrorMessageResourceType = typeof(ViewResource))]
        [RegularExpression(@"^[\w\s]{1,}$",
            ErrorMessageResourceName = "InvalidNameValidationMsg", ErrorMessageResourceType = typeof(ViewResource))]
        public string FunctionName { get; set; }

        /// <summary>
        /// Get mail function.
        /// </summary>
        /// <returns>Mail function.</returns>
        public MailFunction GetMailFunction()
        {
            var mailFunction = new MailFunction
            {
                Config = new MailConfig
                {
                    AppID = this.ConfigId
                },
                Setting = new MailSetting
                {
                    SettingID = this.SettingId
                },
                FuncName = this.FunctionName,
                FuncID = this.FunctionId
            };

            return mailFunction;
        }
    }
}
