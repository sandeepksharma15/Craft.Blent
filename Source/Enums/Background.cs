using Craft.Blent.Utilities;

namespace Craft.Blent.Enums;

public record Background : Enumeration<Background>
{
    public Background( string name ) : base( name ) { }

    public static implicit operator Background(string name)
        => new(name);

    public static readonly Background Default = new( (string)null );
    public static readonly Background Primary = new( "primary" );
    public static readonly Background Secondary = new( "secondary" );
    public static readonly Background Success = new( "success" );
    public static readonly Background Danger = new( "danger" );
    public static readonly Background Warning = new( "warning" );
    public static readonly Background Info = new( "info" );
    public static readonly Background Light = new( "light" );
    public static readonly Background Dark = new( "dark" );
    public static readonly Background White = new( "white" );
    public static readonly Background Transparent = new( "transparent" );
    public static readonly Background Body = new( "body" );
}
