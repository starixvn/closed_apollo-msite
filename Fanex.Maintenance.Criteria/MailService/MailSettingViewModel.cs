// --------------------------------------------------------------------------
// <copyright file="MailSettingViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.12.6">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.MailService
{
    using System.ComponentModel.DataAnnotations;
    using Fanex.Maintenance.Core;
    using MailDeliver.Model;

    /// <summary>
    /// Mail setting view model.
    /// </summary>
    public class MailSettingViewModel
    {
        public MailSettingViewModel()
        {
        }

        public MailSettingViewModel(MailSetting mailSetting)
        {
            this.SettingName = mailSetting.SettingName;
            this.SettingId = mailSetting.SettingID;
            this.Server = mailSetting.Server;
            this.Port = mailSetting.Port;
            this.Username = mailSetting.Username;
            this.Password = mailSetting.Password;
            this.IsAuth = mailSetting.IsAuth;
        }

        public int SettingId { get; set; }

        [Display(Name = "Setting_Name", ResourceType = typeof(ViewResource))]
        [StringLength(50, MinimumLength = 2,
            ErrorMessageResourceName = "InputLengthValidationMsg", ErrorMessageResourceType = typeof(ViewResource))]
        public string SettingName { get; set; }

        [Required]
        [Display(Name = "Server", ResourceType = typeof(ViewResource))]
        [StringLength(50, MinimumLength = 2,
            ErrorMessageResourceName = "InputLengthValidationMsg", ErrorMessageResourceType = typeof(ViewResource))]
        public string Server { get; set; }

        [Required]
        [Display(Name = "Port", ResourceType = typeof(ViewResource))]
        public int Port { get; set; }

        [Required]
        [Display(Name = "Username", ResourceType = typeof(ViewResource))]
        [StringLength(50, MinimumLength = 2,
            ErrorMessageResourceName = "InputLengthValidationMsg", ErrorMessageResourceType = typeof(ViewResource))]
        public string Username { get; set; }

        [Display(Name = "Password", ResourceType = typeof(ViewResource))]
        public string Password { get; set; }

        [Display(Name = "IsAuth", ResourceType = typeof(ViewResource))]
        public bool IsAuth { get; set; }

        /// <summary>
        /// Get mail setting.
        /// </summary>
        /// <returns>Mail setting.</returns>
        public MailSetting GetMailSetting()
        {
            var mailSetting = new MailSetting
            {
                IsAuth = this.IsAuth,
                Password = this.Password,
                Port = this.Port,
                Server = this.Server,
                SettingID = this.SettingId,
                SettingName = this.SettingName,
                Username = this.Username
            };

            return mailSetting;
        }
    }
}
