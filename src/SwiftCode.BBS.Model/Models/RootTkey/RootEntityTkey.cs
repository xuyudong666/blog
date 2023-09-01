using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCode.BBS.Model.Models.RootTkey;

public class RootEntityTkey<Tkey> where Tkey : IEquatable<Tkey>
{
    public Tkey Id { get; set; }
}
