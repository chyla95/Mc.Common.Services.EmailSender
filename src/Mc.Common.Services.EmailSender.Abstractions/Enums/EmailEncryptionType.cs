namespace Mc.Common.Services.EmailSender.Abstractions.Enums;
public enum EmailEncryptionType
{
    /// <summary>
    /// No connection encryption.
    /// </summary>
    None,

    /// <summary>
    /// If a server is compatible and no errors occur, the secured TLS or SSL connection will be established.
    /// If anything fails in the process, a plain-text transmission will be established.
    /// </summary>
    OptionalTls,

    /// <summary>
    /// If a server is compatible and no errors occur, the secured TLS or SSL connection will be established.
    /// If the server is incompatible or the connection times out, the transmission will be abandoned.
    /// </summary>
    MandatoryTls
}
