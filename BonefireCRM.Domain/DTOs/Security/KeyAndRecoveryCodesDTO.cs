namespace BonefireCRM.Domain.DTOs.Security
{
    public class KeyAndRecoveryCodesDTO
    {
        public string Key { get; set; } = string.Empty;

        public string[] RecoveryCodes { get; set; } = [];

        public int RecoveryCodesLeft { get; set; }

        public bool IsTwoFactorEnabled { get; set; }

        public KeyValuePair<string, string> ValidationError { get; set; }
    }
}
