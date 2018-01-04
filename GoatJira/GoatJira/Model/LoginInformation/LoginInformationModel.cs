using Newtonsoft.Json;
using System;
using System.Text;
using System.Security.Cryptography;
using GalaSoft.MvvmLight;

namespace GoatJira.Model.LoginInformation
{
    /// <summary>
    /// Parameters used for connecting to JIRA server
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    class LoginInformationModel : ObservableObject
    {
        public LoginInformationModel()
        {
            URI = Username = Password = "";
            SavePassword = false;
        }

        public LoginInformationModel(string URI, string Username, string Password, bool SavePassword = false)
        {
            this.URI = URI;
            this.Username = Username;
            this.Password = Password;
            this.SavePassword = SavePassword;
        }

        private string uri;
        private string username;
        private string password;
        private bool savePassword;
        /// <summary>
        /// URI of a JIRA server
        /// </summary>
        [JsonProperty]
        public string URI
        {
            get => uri;
            set => Set(nameof(URI), ref uri, value);
        }
        /// <summary>
        /// Username (or email) of the JIRA account
        /// </summary>
        [JsonProperty]
        public string Username
        {
            get => username;
            set => Set(nameof(Username), ref username, value);
        }
        /// <summary>
        /// Password of the JIRA account
        /// </summary>
        public string Password
        {
            get => password;
            set => Set(nameof(Password), ref password, value);
        }
        /// <summary>
        /// true if it is requested to save the password, false otherwise
        /// </summary>
        [JsonProperty]
        public bool SavePassword
        {
            get => savePassword;
            set => Set(nameof(SavePassword), ref savePassword, value);
        }
        /// <summary>
        /// Encrypted form of a password
        /// </summary>
        [JsonProperty("Password")]
        private string PasswordForSave
        {
            get => SavePassword ? EncodePassword(Password) : null;
            set => Password = DecodePassword(value);
        }

        /// <summary>
        /// Encode a password
        /// </summary>
        /// <param name="s">string to be encoded</param>
        /// <returns></returns>
        private string EncodePassword(string s)
        {
            // Data to protect
            byte[] plaintext = Encoding.UTF8.GetBytes(s);

            // Generate additional entropy (will be used as the Initialization vector)
            byte[] entropy = new byte[20];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(entropy);
            }

            byte[] ciphertext = ProtectedData.Protect(plaintext, entropy, DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(ciphertext) + "^" + Convert.ToBase64String(entropy);
        }

        /// <summary>
        /// Decode a password
        /// </summary>
        /// <param name="s">Password to be decoded</param>
        /// <returns></returns>
        private string DecodePassword(string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;
            try
            {
                byte[] pwd = Convert.FromBase64String(s.Substring(0, s.IndexOf('^')));
                byte[] entropy = Convert.FromBase64String(s.Substring(s.IndexOf('^') + 1));
                return Encoding.UTF8.GetString(ProtectedData.Unprotect(pwd, entropy, DataProtectionScope.CurrentUser));
            }
            catch (Exception)
            {
                return "";
            }
        }

    }
}
