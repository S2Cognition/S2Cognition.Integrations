using S2Cognition.Integrations.Monday.Core.Models.Requests;

namespace S2Cognition.Integrations.Monday.Core.Models.Options;

internal interface IUserOptions : IBaseOptions
{
    bool IncludeName { get; set; }
    bool IncludeEmail { get; set; }
    bool IncludeUrl { get; set; }
    bool IncludePhoto { get; set; }
    bool IncludeTitle { get; set; }
    bool IncludeBirthday { get; set; }
    bool IncludeCountryCode { get; set; }
    bool IncludeLocation { get; set; }
    bool IncludeTimeZoneIdentifier { get; set; }
    bool IncludePhone { get; set; }
    bool IncludeMobilePhone { get; set; }
    bool IncludeIsGuest { get; set; }
    bool IncludeIsPending { get; set; }
    bool IncludeIsEnabled { get; set; }
    bool IncludeCreatedAt { get; set; }
}

internal class UserOptions : BaseOptions, IUserOptions
{
    public bool IncludeName { get; set; }
    public bool IncludeEmail { get; set; }
    public bool IncludeUrl { get; set; }
    public bool IncludePhoto { get; set; }
    public bool IncludeTitle { get; set; }
    public bool IncludeBirthday { get; set; }
    public bool IncludeCountryCode { get; set; }
    public bool IncludeLocation { get; set; }
    public bool IncludeTimeZoneIdentifier { get; set; }
    public bool IncludePhone { get; set; }
    public bool IncludeMobilePhone { get; set; }
    public bool IncludeIsGuest { get; set; }
    public bool IncludeIsPending { get; set; }
    public bool IncludeIsEnabled { get; set; }
    public bool IncludeCreatedAt { get; set; }

    internal UserOptions()
        : this(RequestMode.Default)
    {
    }

    internal UserOptions(RequestMode mode)
        : base("user")
    {
        switch (mode)
        {
            case RequestMode.Minimum:
                IncludeName = false;
                IncludeEmail = false;
                IncludeUrl = false;
                IncludePhoto = false;
                IncludeTitle = false;
                IncludeBirthday = false;
                IncludeCountryCode = false;
                IncludeLocation = false;
                IncludeTimeZoneIdentifier = false;
                IncludePhone = false;
                IncludeMobilePhone = false;
                IncludeIsGuest = false;
                IncludeIsPending = false;
                IncludeIsEnabled = false;
                IncludeCreatedAt = false;
                break;

            case RequestMode.Maximum:
            case RequestMode.MaximumChild:
                IncludeName = true;
                IncludeEmail = true;
                IncludeUrl = true;
                IncludePhoto = true;
                IncludeTitle = true;
                IncludeBirthday = true;
                IncludeCountryCode = true;
                IncludeLocation = true;
                IncludeTimeZoneIdentifier = true;
                IncludePhone = true;
                IncludeMobilePhone = true;
                IncludeIsGuest = true;
                IncludeIsPending = true;
                IncludeIsEnabled = true;
                IncludeCreatedAt = true;
                break;

            default:
                IncludeName = true;
                IncludeEmail = true;
                IncludeUrl = true;
                IncludePhoto = true;
                IncludeTitle = true;
                IncludeBirthday = true;
                IncludeCountryCode = true;
                IncludeLocation = true;
                IncludeTimeZoneIdentifier = true;
                IncludePhone = true;
                IncludeMobilePhone = true;
                IncludeIsGuest = true;
                IncludeIsPending = true;
                IncludeIsEnabled = true;
                IncludeCreatedAt = true;
                break;
        }
    }

    internal override string Build(OptionBuilderMode mode, (string key, object val)[]? attrs = null)
    {
        var modelName = GetModelName(mode);
        var modelAttributes = GetModelAttributes(attrs);

        var name = GetField(IncludeName, "name");
        var email = GetField(IncludeEmail, "email");
        var url = GetField(IncludeUrl, "url");
        var photo = GetField(IncludePhoto, "photo_original");
        var title = GetField(IncludeTitle, "title");
        var birthday = GetField(IncludeBirthday, "birthday");
        var countryCode = GetField(IncludeCountryCode, "country_code");
        var location = GetField(IncludeLocation, "location");
        var timeZoneIdentifier = GetField(IncludeTimeZoneIdentifier, "time_zone_identifier");
        var phone = GetField(IncludePhone, "phone");
        var mobilePhone = GetField(IncludeMobilePhone, "mobile_phone");
        var isGuest = GetField(IncludeIsGuest, "is_guest");
        var isPending = GetField(IncludeIsPending, "is_pending");
        var enabled = GetField(IncludeIsEnabled, "enabled");
        var createdAt = GetField(IncludeCreatedAt, "created_at");

        return $@"
{modelName}{modelAttributes} {{
    id {name} {email} {url} {photo} {title} {birthday} {countryCode} {location}
    {timeZoneIdentifier} {phone} {mobilePhone} {isGuest} {isPending} {enabled} {createdAt}
}}";
    }
}

internal class OwnerOptions : UserOptions
{
    internal OwnerOptions()
        : this(RequestMode.Default)
    {
    }

    internal OwnerOptions(RequestMode mode)
        : base(mode)
    {
        NameSingular = "owner";
        NamePlural = "owners";
    }
}

internal class CreatorOptions : UserOptions
{
    internal CreatorOptions()
        : this(RequestMode.Default)
    {
    }

    internal CreatorOptions(RequestMode mode)
        : base(mode)
    {
        NameSingular = "creator";
        NamePlural = "creators";
    }
}

internal class SubscriberOptions : UserOptions
{
    internal SubscriberOptions()
        : this(RequestMode.Default)
    {
    }

    internal SubscriberOptions(RequestMode mode)
        : base(mode)
    {
        NameSingular = "subscriber";
        NamePlural = "subscribers";
    }
}
