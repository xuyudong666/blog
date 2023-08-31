namespace SwiftCode.BBS.Model.Models;

public class Article
{
    public int Id { get; set; }
    public string Submitter { get; set; }
    public string Title { get; set; }

    public string Category { get; set; }

    public string Content { get; set; }

    public int Traffic { get; set; }

    public int CommentNum { get; set; }
    public DateTime UpdateTime { get; set; }

    public DateTime CreateTime { get; set; }

    public string Remark { get; set; }

    public bool? IsDeleted { get; set; }
}
