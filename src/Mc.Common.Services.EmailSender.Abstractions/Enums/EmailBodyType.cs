namespace Mc.Common.Services.EmailSender.Abstractions.Enums;
public enum EmailBodyType
{
    /// <summary>
    /// Represents the plain text format for the body of an email message. 
    /// This type typically contains simple and unformatted textual content.
    /// </summary>
    Text,
    /// <summary>
    /// Represents the HTML format for the body of an email message. 
    /// This type allows for richer and formatted content, including the use of HTML tags for styling and layout.
    /// </summary>
    Html
}
