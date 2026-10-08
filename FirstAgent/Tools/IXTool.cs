using System.ComponentModel;

namespace FirstAgent.Tools;

public interface IXTool
{ 
    [Description("")]
    string GetY([Description("")] String Z);

    [Description("")]
    string GetZ([Description("")] String Z);

}