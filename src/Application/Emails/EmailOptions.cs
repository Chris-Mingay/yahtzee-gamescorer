namespace Application.Emails;

public class EmailOptions
{
    public string RecipientAddress { get; set; }
    public string RecipientName { get; set; }
    public string FromAddress { get; set; }
    public string FromName { get; set; }
    public string Subject { get; set; }
    public string HtmlBody { get; set; }
    public string PlainTextBody { get; set; }
}