using Microsoft.Extensions.Localization;

namespace StsServerIdentity.Resources
{
    public class LocService
    {
        private readonly IStringLocalizer _localizer;

        public LocService(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            return !string.IsNullOrEmpty(key) ? _localizer[key] : new LocalizedString(key, string.Empty);
        }

        public LocalizedString GetLocalizedHtmlStringAllowNull(string key)
        {
            return !string.IsNullOrWhiteSpace(key) ? _localizer[key] : new LocalizedString(key, string.Empty);
        }

        public LocalizedString GetLocalizedHtmlString(string key, string parameter)
        {
            return !string.IsNullOrEmpty(key) ? _localizer[key, parameter] : new LocalizedString(key, string.Empty);
        }
    }
}