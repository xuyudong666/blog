using SwiftCode.BBS.Model.Models.RootTkey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCode.BBS.Model.Models;

public class UserCollectionArticle : RootEntityTkey<int>
{
    public int UserId { get; set; }

    public int ArticleId { get; set; }
}
